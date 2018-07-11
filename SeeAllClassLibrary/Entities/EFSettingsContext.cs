using SeeAllClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Entities
{
    public class EFSettingsContext : DbContext
    {
        public DbSet<PLCSettings> SettingsPLC { get; set; }
        public DbSet<PointSeeAllSettings> SettingsPointSeeAll { get; set; }
    }
}
