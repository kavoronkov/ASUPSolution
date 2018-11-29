using SeeAllClassLibrary.Entities;
using SeeAllClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Controllers
{
    public class WorkingPLC
    {
        public bool StatusConnectionSql { get; set; }

        public WorkingPLC(WorkCenter itemWorkCenter)
        {
            StatusConnectionSql = false;

            LoadingPLC loadPLC = new LoadingPLC(itemWorkCenter);

            while (true)
            {
                //TODO Thread.Sleep(5000);
                WorkCycle(loadPLC, itemWorkCenter.PointId);
            }
        }

        private void WorkCycle(LoadingPLC loadPLC, int pointId)
        {
            List<Datetime> allCpu = null;

            // 1. Read from CPU
            allCpu = loadPLC.ReadAllCpu();
            if (allCpu == null)
                return;

            long maxIdSql = GetMaxIdSql(allCpu, pointId);

            //2. 
            List<Datetime> allCpuNew = SelectMoreMaxIdSql(allCpu, maxIdSql);

            // 3. Write to SQL
            WriteToSql(allCpuNew);
        }

        private List<Datetime> SelectMoreMaxIdSql(List<Datetime> allCpu, long maxIdSql)
        {
            var sortedAllCpu = allCpu.OrderByDescending(u => u.DatetimeId).ToList();
            int indexStart = sortedAllCpu.FindIndex(u => u.DatetimeId == maxIdSql);
            if (indexStart < sortedAllCpu.Count)
                indexStart++;
            return allCpu.GetRange(indexStart, allCpu.Count);
        }

        private void WriteToSql(List<Datetime> datetimeList)
        {
            using (var context = new EFSeeAllContext())
            {
                StatusConnectionSql = true;
                try
                {
                    if (datetimeList != null)
                    {
                        // add to BD SQL -------------------------WRITE-----<-----<-----<
                        context.Datetimes.AddRange(datetimeList);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    //TODO need a logger
                    StatusConnectionSql = false;
                }
            }
        }

        private int GetMaxIdSql(List<Datetime> datetimeList, int pointId)
        {
            using (var context = new EFSeeAllContext())
            {
                StatusConnectionSql = true;
                try
                {
                    // find MAX ID in the SQL
                    long idMaxSql = context.Datetimes
                                    .Where(c => c.PointId == pointId)
                                    .Select(p => p.DatetimeId)
                                    .DefaultIfEmpty(0)
                                    .Max();

                    foreach (var itemDt in datetimeList)
                    {
                        if (itemDt.DatetimeId > idMaxSql)
                        {
                            return datetimeList.IndexOf(itemDt);
                        }
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    //TODO need a logger
                    StatusConnectionSql = false;
                    return 0;
                }
            }
        }
    }
}