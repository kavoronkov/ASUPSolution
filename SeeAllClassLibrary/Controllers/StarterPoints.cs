using SeeAllClassLibrary.Entities;
using SeeAllClassLibrary.Models;
using SeeAllClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Controllers
{
    class StarterPoints
    {
        public StarterPoints()
        {
            // 1.
            init();
            // 2. 
            var listWorkCenter = ReadPointsSettings();
            // 3.
            CreateObjectWorkPoints(listWorkCenter);
        }

        private void init()
        {

        }

        private List<WorkCenter> ReadPointsSettings()
        {
            List<WorkCenter> listWorkCenter = null;
            using (var context = new EFSeeAllContext())
            {
                try
                {
                    // select all table WorkCenter            
                    listWorkCenter = (from r in context.WorkCenters select r).ToList();
                }
                catch (Exception ex)
                {
                }
            }
            return listWorkCenter;
        }

        private void CreateObjectWorkPoints(List<WorkCenter> listWorkCenter)
        {
            Task task = new Task(() => new WorkingPLC(listWorkCenter[0]));
            task.Start();

            //new WorkingPLC(listWorkCenter[0]);
            //foreach (WorkCenter itemWorkCenter in listWorkCenter)
            //{
            //    new WorkingPLC(itemWorkCenter);
            //}
        }
    }
}
