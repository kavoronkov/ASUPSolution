using S7.Net;
using SeeAllClassLibrary.Abstract;
using SeeAllClassLibrary.CreatorsPLC;
using SeeAllClassLibrary.Models;
using SeeAllClassLibrary.PLC;
using SeeAllClassLibrary.Settings;
using SeeAllClassLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Controllers
{
    public class LoadingPLC
    {
        public bool StatusConnectionPLC { get; set; }              // status of connection to PLC

        // private const int BYTE_STEP_DB = 12;
        // private const long EX_NUMBER_OVERFLOW_DB = -2146233088;

        private WorkCenter workCenter { get; set; }
        private AbstractPlc abstractPlc { get; set; }
        private AbstractCreatorPlc abstractCreatorPlc { get; set; }

        public LoadingPLC(WorkCenter objectWorkCenter)
        {
            StatusConnectionPLC = false;
            workCenter = objectWorkCenter;
            workCenter.JSONDeserializeDictionary();
            GetTypePLC();
        }

        private void GetTypePLC()
        {
            try
            {
                string nameCpu = GetNameCpu(workCenter.DictionaryWorkCenterSettings);
                if (nameCpu != "")
                {
                    switch (nameCpu)
                    {
                        case "Arduino": { abstractCreatorPlc = new CreatorArduinoPlc(); break; }
                        case "Raspberry": { abstractCreatorPlc = new CreatorRaspberryPlc(); break; }
                        case "Siemens": { abstractCreatorPlc = new CreatorSiemensPlc(); break; }

                        default: { abstractCreatorPlc = new CreatorSiemensPlc(); break; }
                    }
                    abstractPlc = abstractCreatorPlc.CreatePlc();
                }
            }
            catch (ArgumentNullException argumentNullException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        // index from to
        public List<Datetime> ReadAllCpu()
        {
            string nameCpu = GetNameCpu(workCenter.DictionaryWorkCenterSettings);
            if (nameCpu != "")
            {
                if (nameCpu == "Siemens")
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
                        typeCpu = UtilityClass.StringToInt32FromDictionary(workCenter.DictionaryWorkCenterSettings, "TypeCpu");
                        plcIp = UtilityClass.StringToStringFromDictionary(workCenter.DictionaryWorkCenterSettings, "PlcIp");
                        rackCpu = UtilityClass.StringToInt32FromDictionary(workCenter.DictionaryWorkCenterSettings, "RackCpu");
                        slotCpu = UtilityClass.StringToInt32FromDictionary(workCenter.DictionaryWorkCenterSettings, "SlotCpu");
                        threadSleepTime = UtilityClass.StringToInt32FromDictionary(workCenter.DictionaryWorkCenterSettings, "ThreadSleepTime");
                        exNumberOverflowDB = UtilityClass.StringToInt64FromDictionary(workCenter.DictionaryWorkCenterSettings, "ExNumberOverflowDB");

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
                    catch (Exception ex)
                    {
                        dtList = null;
                    }

                    return dtList;
                }
                else return null;
            }
            else return null;
        }


        // loading Datetime from the CPU
        private Datetime ReadDateTime(Plc plc, int startByteAdress)
        {
            for (int i = 0; i < workCenterSettings.NumberAttemptsConnection; i++)  // counter
            {
                Datetime datetime = ReadDatetimeLogics(plc, startByteAdress);
                if (datetime != null)
                {
                    StatusConnectionPLC = true;
                    return datetime;
                }
                Thread.Sleep(workCenterSettings.ConnectionSleepTime);     //msec
            }
            StatusConnectionPLC = false;
            return null;   // There aren't data
        }

        // If in a row Index = null THEN finish this connection and start new connection
        private int numberOfAttempts = 2;
        private int countNull = 0;
        private bool CheckConnectionFailed(Datetime readDt)
        {
            if (readDt == null)
            {
                countNull++;
                if (countNull >= numberOfAttempts)
                {
                    // noConnection
                    return true;
                }
            }
            else
                countNull = 0;
            return false;
        }

        private Datetime ReadDatetimeLogics(Plc plc, int startByteAdress)
        {
            int dataBlock = workCenterSettings.DataBlockDatetime;
            int[] dateTimeArray = new int[6];
            int addStepBytes = startByteAdress * workCenterSettings.ByteStepDB;

            Datetime modelDateTime = new Datetime();
            try
            {
                if (plc.IsConnected)
                {
                    for (int i = 0; i < dateTimeArray.Length; i++)
                    {
                        dateTimeArray[i] = GetPlcRead(plc, dataBlock, i * 2 + addStepBytes); // every two bytes
                    }
                    modelDateTime.DatetimeId = Convert.ToInt64(getIdDateTimeForReadDatetime(dateTimeArray).ToString());
                    modelDateTime.DatetimeValue = getDateTimeForReadDatetime(dateTimeArray);
                    modelDateTime.PointId = workCenter.PointId;
                    return modelDateTime;
                }
                else
                {
                    return null;
                    //TODO need a logger
                }
            }
            catch (Exception ex)
            {
                modelDateTime.DatetimeId = ex.HResult;
                return modelDateTime;
                //TODO need a logger
            }

        }

        private long getIdDateTimeForReadDatetime(int[] dateTimeArray)
        {
            // IF year, month, day = 0 THERE Id_DateTime = -1;
            if ((dateTimeArray[0] == 0) || (dateTimeArray[1] == 0) || (dateTimeArray[2] == 0))
            {
                return -1;
            }

            string stringIdDateTime = "";
            foreach (var item in dateTimeArray)
            {
                stringIdDateTime += NormalIntToString(item);
            }
            return Convert.ToInt64(stringIdDateTime);
        }

        private DateTime getDateTimeForReadDatetime(int[] dateTimeArray)
        {
            return new DateTime(dateTimeArray[0], dateTimeArray[1], dateTimeArray[2], dateTimeArray[3], dateTimeArray[4], dateTimeArray[5]);
        }

        private int GetPlcRead(Plc plc, int dataBlock, int startByteAdress)
        {
            return Convert.ToInt32(plc.Read(DataType.DataBlock, dataBlock, startByteAdress, VarType.Int, 1));
        }

        // IF value = 0..9 THERE value = "0" + value
        private string NormalIntToString(int value)
        {
            if (value <= 9)
            {
                return "0" + value;
            }
            else
            {
                return value.ToString();
            }
        }

        public string GetNameCpu(Dictionary<string, string> dictionary)
        {
            string nameCpu = "";

            try
            {
                nameCpu = UtilityClass.StringFromDictionary(dictionary, "PlcName");
                return nameCpu;
            }

            catch (ArgumentNullException argumentNullException)
            {
                throw;
            }

            catch (Exception exception)
            {
                throw;
            }
        }
    }
}