using SeeAllClassLibrary.Abstract;
using SeeAllClassLibrary.Directories;
using SeeAllClassLibrary.Entities;
using SeeAllClassLibrary.Models;
using SeeAllClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Repositories
{
    public class EFSeeAllRepository : ISeeAllRepository
    {
        EFSeeAllContext context = new EFSeeAllContext();

        public IEnumerable<Datetime> Datetimes
        {
            get { return context.Datetimes; }
        }

        public IEnumerable<Downtime> Downtimes
        {
            get { return context.Downtimes; }
        }

        public IEnumerable<Plant> Plants
        {
            get { return context.Plants; }
        }

        public IEnumerable<Workshop> Workshops
        {
            get { return context.Workshops; }
        }

        public IEnumerable<Department> Departments
        {
            get { return context.Departments; }
        }

        public IEnumerable<Point> Points
        {
            get { return context.Points; }
        }

        public IEnumerable<PLCSettings> SettingsPLC
        {
            get { return context.SettingsPLC; }
        }

        public IEnumerable<SeeAllSettings> SettingsSeeAll
        {
            get { return context.SettingsSeeAll; }
        }
    }
}
