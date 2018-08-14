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
    class WorkingPLC
    {
        public bool StatusConnectionSql { get; set; }
        private int lastIndexRead = 0;
        private int numberItems = 10;  // Number of items from the controller

        public WorkingPLC(WorkCenter itemWorkCenter)
        {
            StatusConnectionSql = false;

            LoadingPLC loadPLC = new LoadingPLC(itemWorkCenter);

            while (true)
            {
                Thread.Sleep(60000);
                WorkCycle(loadPLC, itemWorkCenter.PointId);                
            }

            //var a(int a, string b, object c) = ( 5, "aaa", null );
        }

        private void WorkCycle(LoadingPLC loadPLC, int pointId)
        {
            List<Datetime> readAllFromCpu = null;

            // 1. Read from CPU
            if (lastIndexRead == 0)
            {
                // first step
                readAllFromCpu = loadPLC.ReadAllFromCpu(0, "ALL");
            }
            else
            {
                readAllFromCpu = loadPLC.ReadAllFromCpu(lastIndexRead, lastIndexRead + numberItems);
                lastIndexRead += numberItems;
            }
            if (readAllFromCpu == null)
            {
                lastIndexRead = 0;
                return;
            }
                

            //2. find last of not duplicate
            lastIndexRead = CheckLastIndexDatetime(readAllFromCpu, pointId);

            // 3. Write to SQL
            WriteToSql(readAllFromCpu);
        }

        private void WriteToSql(List<Datetime> datetimeList)
        {
            try
            {
                SaveDateTimeSql(checkDatetimeList(datetimeList));
            }
            catch (Exception ex)
            {
                SaveDateTimeSql(checkDatetimeList(datetimeList));
            }
        }

        private int CheckLastIndexDatetime(List<Datetime> datetimeList, int pointId)
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

        private List<Datetime> checkDatetimeList(List<Datetime> datetimeList)
        {
            List<Datetime> newDatetimeList = new List<Datetime>();
            using (var context = new EFSeeAllContext())
            {
                StatusConnectionSql = true;
                try
                {
                    foreach (var itemDt in datetimeList)
                    {
                        // check id for duplicates
                        if (context.Datetimes.FirstOrDefault(p => p.DatetimeId == itemDt.DatetimeId) == null)
                            newDatetimeList.Add(itemDt);
                    }
                }
                catch (Exception ex)
                {
                    //TODO need a logger
                    StatusConnectionSql = false;
                }
            }
            return newDatetimeList;
        }

        private void SaveDateTimeSql(List<Datetime> datetimeList)
        {
            using (var context = new EFSeeAllContext())
            {
                StatusConnectionSql = true;
                try
                {
                    // check id for duplicates
                    //Datetime model_DateTime = context.Datetimes.FirstOrDefault(p => p.DatetimeId == modelDateTime.DatetimeId);

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
    }
}
