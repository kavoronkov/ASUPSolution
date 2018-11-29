using S7.Net;
using SeeAllClassLibrary.Abstract;
using SeeAllClassLibrary.Models;
using SeeAllClassLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.PLC
{
    public class SiemensPlc : AbstractPlc
    {
        public override List<Datetime> ReadAllCpu(Dictionary<string, string> dictionary)
        {
            int typeCpu = -1;
            string plcIp = "";
            int rackCpu = -1;
            int slotCpu = -1;
            int threadSleepTime = -1;
            long exNumberOverflowDB = -1;

            int startIndex = 0;
            List<Datetime> dtList = new List<Datetime>();

            try
            {
                typeCpu = UtilityClass.StringToInt32FromDictionary(dictionary, "TypeCpu");
                plcIp = UtilityClass.StringToStringFromDictionary(dictionary, "PlcIp");
                rackCpu = UtilityClass.StringToInt32FromDictionary(dictionary, "RackCpu");
                slotCpu = UtilityClass.StringToInt32FromDictionary(dictionary, "SlotCpu");
                threadSleepTime = UtilityClass.StringToInt32FromDictionary(dictionary, "ThreadSleepTime");
                exNumberOverflowDB = UtilityClass.StringToInt64FromDictionary(dictionary, "ExNumberOverflowDB");

                if (typeCpu >= 0 && plcIp != "" && rackCpu >= 0 && slotCpu >= 0 && threadSleepTime >= 0 && exNumberOverflowDB >= 0)
                {
                    using (var plc = new Plc(GetTypeCpu(typeCpu), plcIp, (short)rackCpu, (short)slotCpu))  //"172.17.132.200"       "127.0.0.1"
                    {
                        Thread.Sleep(threadSleepTime);
                        plc.Open();

                        for (int index = startIndex; ; index++)
                        {
                            Datetime readDt = ReadDateTime(plc, index);

                            if (CheckConnectionFailed(readDt))
                                dtList = null;

                            if (readDt.DatetimeId == exNumberOverflowDB)
                            {
                                return dtList;
                            }

                            if (readDt != null)
                            {
                                dtList.Add(readDt);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dtList;
        }

        public CpuType GetTypeCpu(int intTypeCpu)
        {
            switch (intTypeCpu)
            {
                case 0: return CpuType.S7200;
                case 10: return CpuType.S7300;
                case 20: return CpuType.S7400;
                case 30: return CpuType.S71200;
                case 40: return CpuType.S71500;

                default:
                    return CpuType.S71200;
            }
        }
        //public List<Datetime> ReadAllCpu(Dictionary<string, string> dictionary)
        //{
        //    int typeCpu = -1;
        //    string plcIp = "";
        //    int rackCpu = -1;
        //    int slotCpu = -1;
        //    int threadSleepTime = -1;
        //    long exNumberOverflowDB = -1;

        //    int startIndex = 0;
        //    List<Datetime> dtList = new List<Datetime>();

        //    try
        //    {
        //        typeCpu = UtilityClass.StringToInt32FromDictionary(dictionary, "TypeCpu");
        //        plcIp = UtilityClass.StringToStringFromDictionary(dictionary, "PlcIp");
        //        rackCpu = UtilityClass.StringToInt32FromDictionary(dictionary, "RackCpu");
        //        slotCpu = UtilityClass.StringToInt32FromDictionary(dictionary, "SlotCpu");
        //        threadSleepTime = UtilityClass.StringToInt32FromDictionary(dictionary, "ThreadSleepTime");
        //        exNumberOverflowDB = UtilityClass.StringToInt64FromDictionary(dictionary, "ExNumberOverflowDB");

        //        if (typeCpu >= 0 && plcIp != "" && rackCpu >= 0 && slotCpu >= 0 && threadSleepTime >= 0 && exNumberOverflowDB >= 0)
        //        {
        //            using (var plc = new Plc(GetTypeCpu(typeCpu), plcIp, (short)rackCpu, (short)slotCpu))  //"172.17.132.200"       "127.0.0.1"
        //            {
        //                Thread.Sleep(threadSleepTime);
        //                plc.Open();

        //                for (int index = startIndex; ; index++)
        //                {
        //                    Datetime readDt = ReadDateTime(plc, index);

        //                    if (CheckConnectionFailed(readDt))
        //                        dtList = null;

        //                    if (readDt.DatetimeId == exNumberOverflowDB)
        //                    {
        //                        return dtList;
        //                    }

        //                    if (readDt != null)
        //                    {
        //                        dtList.Add(readDt);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        dtList = null;
        //    }

        //    return dtList;
        //}
    }
}
