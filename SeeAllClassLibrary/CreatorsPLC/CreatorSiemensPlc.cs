using SeeAllClassLibrary.Abstract;
using SeeAllClassLibrary.PLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.CreatorsPLC
{
    class CreatorSiemensPlc : AbstractCreatorPlc
    {
        public override AbstractPlc CreatePlc()
        {
            return new SiemensPlc();
        }
    }
}
