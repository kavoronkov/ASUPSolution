using SeeAllClassLibrary.Directories;
using SeeAllClassLibrary.Initializers;
using SeeAllClassLibrary.Models;
using SeeAllClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Entities
{
    public class EFSeeAllContext : DbContext
    {
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Point> Points { get; set; }

        public DbSet<Datetime> Datetimes { get; set; }
        public DbSet<Downtime> Downtimes { get; set; }
        public DbSet<WorkCenter> WorkCenters { get; set; }

        public DbSet<PLCSettings> SettingsPLC { get; set; }
        public DbSet<PointSeeAllSettings> SettingsPointSeeAll { get; set; }
                
        //static EFSeeAllContext()
        //{
        //    Database.SetInitializer<EFSeeAllContext>(new SeeAllClassLibraryDbInitializer());
        //}

        //public EFSeeAllContext() : base("SeeAllClassLibrary") { }
    }
}

