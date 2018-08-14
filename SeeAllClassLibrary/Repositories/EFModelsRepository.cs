using SeeAllClassLibrary.Abstract;
using SeeAllClassLibrary.Entities;
using SeeAllClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Repositories
{
    public class EFModelsRepository : IModelsRepository
    {
        EFModelsContext context = new EFModelsContext();

        public IEnumerable<Datetime> Datetimes
        {
            get { return context.Datetimes; }
        }

        public IEnumerable<Downtime> Downtimes
        {
            get { return context.Downtimes; }
        }

        public IEnumerable<WorkCenter> WorkCenter
        {
            get { return context.WorkCenters; }
        }
    }
}
