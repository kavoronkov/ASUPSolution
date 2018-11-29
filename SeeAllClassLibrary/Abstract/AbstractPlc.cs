using SeeAllClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Abstract
{
    public abstract class AbstractPlc
    {
        public abstract List<Datetime> ReadAllCpu(Dictionary<string, string> dictionary);
    }
}
