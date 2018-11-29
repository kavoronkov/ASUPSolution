using SeeAllClassLibrary.Abstract;
using SeeAllClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.PLC
{
    public class RaspberryPlc : AbstractPlc
    {
        public override List<Datetime> ReadAllCpu(Dictionary<string, string> dictionary)
        {
            throw new NotImplementedException();
        }
    }
}
