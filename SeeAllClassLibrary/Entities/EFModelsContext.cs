using SeeAllClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Entities
{
    public class EFModelsContext : DbContext
    {
        public DbSet<Datetime> Datetimes { get; set; }
        public DbSet<Downtime> Downtimes { get; set; }
    }
}
