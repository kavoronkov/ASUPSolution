using SeeAllClassLibrary.Directories;
using SeeAllClassLibrary.Initializers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Entities
{
    public class EFDirectoriesContext : DbContext
    {
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Point> Points { get; set; }

        //static EFDirectoriesContext()
        //{
        //    Database.SetInitializer<EFDirectoriesContext>(new SeeAllClassLibraryDbInitializer());
        //}

        //public EFDirectoriesContext() : base("SeeAllClassLibrary") { }
    }
}
