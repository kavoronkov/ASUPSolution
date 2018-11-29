using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Models
{
    class SelectCpuTypeConnect
    {
        public CpuType GetCpuType(int index)
        {
            switch (index)
            {
                case 1: return CpuType.S71200;
                case 2: return CpuType.S71500;
                case 3: return CpuType.S7200;
                case 4: return CpuType.S7300;
                case 5: return CpuType.S7400;
                default:
                    return CpuType.S71200;
            }
        }

        // public string GetStrCpuType(int index)
        // {
        //     switch (index)
        //     {
        //         case 1: return "S71200";
        //         case 2: return "S71500";
        //         case 3: return "S7200";
        //         case 4: return "S7300";
        //         case 5: return "S7400";
        //         default:
        //             return "S71200";
        //     }
        // }

        // public int GetIntCpuType(string indexStr)
        // {
        //     switch (indexStr)
        //     {
        //         case "S71200": return 1;
        //         case "S71500": return 2;
        //         case "S7200": return 3;
        //         case "S7300": return 4;
        //         case "S7400": return 5;
        //         default:
        //             return 1;
        //     }
        // }
    }
}
