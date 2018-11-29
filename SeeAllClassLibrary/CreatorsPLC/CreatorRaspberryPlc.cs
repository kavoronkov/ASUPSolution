using SeeAllClassLibrary.Abstract;
using SeeAllClassLibrary.PLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.CreatorsPLC
{
    public class CreatorRaspberryPlc : AbstractCreatorPlc
    {
        public override AbstractPlc CreatePlc()
        {
            return new RaspberryPlc();
        }
    }
}
