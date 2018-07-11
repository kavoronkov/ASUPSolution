using SeeAllClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Abstract
{
    public interface ISettingsRepository
    {
        IEnumerable<PLCSettings> SettingsPLC { get; }
        IEnumerable<PointSeeAllSettings> SettingsPointSeeAll { get; }
    }
}
