using SeeAllClassLibrary.Abstract;
using SeeAllClassLibrary.Entities;
using SeeAllClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Repositories
{
    public class EFSettingsRepository : ISettingsRepository
    {
        EFSettingsContext context = new EFSettingsContext();

        public IEnumerable<PLCSettings> SettingsPLC
        {
            get { return context.SettingsPLC; }
        }

        public IEnumerable<PointSeeAllSettings> SettingsPointSeeAll
        {
            get { return context.SettingsPointSeeAll; }
        }
    }
}
