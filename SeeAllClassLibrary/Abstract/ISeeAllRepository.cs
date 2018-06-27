using SeeAllClassLibrary.Directories;
using SeeAllClassLibrary.Models;
using SeeAllClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Abstract
{
    public interface ISeeAllRepository
    {
        IEnumerable<Datetime> Datetimes { get; }
        IEnumerable<Downtime> Downtimes { get; }

        IEnumerable<Plant> Plants { get; }
        IEnumerable<Workshop> Workshops { get; }
        IEnumerable<Department> Departments { get; }
        IEnumerable<Point> Points { get; }

        IEnumerable<PLCSettings> SettingsPLC { get; }
        IEnumerable<SeeAllSettings> SettingsSeeAll { get; }
    }
}
