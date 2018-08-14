using S7.Net;
using SeeAllClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Controllers
{
    class LoadingPLC
    {
        public int byteStep { get; set; }
        public string dbPLC { get; set; }
        public string dbwPLC { get; set; }
        public int waitingTime { get; set; }            // waiting time for open connection PLC
        public int numberAttempts { get; set; }         // number of connection attempts
        public bool statusConnection { get; set; }      // status of connection to PLC
        public bool equalityCheck { get; set; }         // equality check of write and read position PLC
        public short prefixForAsp { get; }
        private int sleepTimeConnCpu = 100;            // mc

        private WorkCenter itemWorkCenter;
        private CpuType cpuType;

        public LoadingPLC(WorkCenter itemWorkCenter)
        {
            this.itemWorkCenter = itemWorkCenter;
            byteStep = 12;
            dbPLC = "DB";
            dbwPLC = ".DBW0"; // aspPrefix = 2bayt then need -2b
            waitingTime = 100;
            numberAttempts = 10;
            prefixForAsp = 2;
            statusConnection = false;
            equalityCheck = false;
            cpuType = new SelectCpuTypeConnect().GetCpuType(itemWorkCenter.CpuType);


        }

        // All index
        public List<Datetime> ReadAllFromCpu(int startIndex, string finishIndex)
        {
            int maxIndex = 10000;
            return ReadAllFromCpu(startIndex, maxIndex);
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

        // index from to
        public List<Datetime> ReadAllFromCpu(int startIndex, int finishIndex)
        {
            List<Datetime> dtList = new List<Datetime>();
            try
            {
                using (var plc = new Plc(
                    cpuType,
                    itemWorkCenter.PLCIp,
                    (short)itemWorkCenter.RackCPU,
                    (short)itemWorkCenter.SlotCPU))   //"172.17.132.200"       "127.0.0.1"
                    {
                        Thread.Sleep(waitingTime);
                        plc.Open();
                        for (int index = startIndex; index < finishIndex; index++)
                        {
                            Datetime readDt = ReadDateTime(plc, index);
                            
                            if (CheckConnectionFailed(readDt))
                                return null;

                            if (readDt.DatetimeId == -2146233088)
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
            catch (Exception ex)
            {
                //string e = ex.Message;
                return null;
            }
            return dtList;
        }

        // loading Datetime from the CPU
        public Datetime ReadDateTime(Plc plc, int startByteAdress)
        {
            for (int i = 0; i < numberAttempts; i++)  // counter
            {
                Datetime datetime = ReadDatetimeLogics(plc, startByteAdress);                
                if (datetime != null)
                {
                    statusConnection = true;
                    return datetime;
                }
                Thread.Sleep(sleepTimeConnCpu);     //msec
            }
            statusConnection = false;
            return null;   // There aren't data
        }

        private Datetime ReadDatetimeLogics(Plc plc, int startByteAdress)
        {
            int dataBlock = itemWorkCenter.DataBlockDatetime;
            int[] dateTimeArray = new int[6];      
            int addStepBytes = startByteAdress * byteStep;

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
                    modelDateTime.PointId = itemWorkCenter.PointId;
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

        public LimitsCpu ReadLimits()
        {
            for (int i = 0; i < numberAttempts; i++)      // counter
            {
                LimitsCpu limitsCpu = ReadLimitsLogics();
                if (limitsCpu != null)
                {
                    statusConnection = true;
                    CheckWriteReadEqually(limitsCpu);      // for check
                    return limitsCpu;                      // There are data
                }
                Thread.Sleep(sleepTimeConnCpu);     //msec
            }
            statusConnection = false;
            //TODO need a logger
            return null;   // There aren't data
        }

        private LimitsCpu ReadLimitsLogics()
        {
            LimitsCpu limitsCpu = null;
            try
            {
                using (var plc = new Plc(
                    cpuType,
                    itemWorkCenter.PLCIp,
                    (short)itemWorkCenter.RackCPU,
                    (short)itemWorkCenter.SlotCPU))   //"172.17.132.200"
                {
                    Thread.Sleep(waitingTime);
                    plc.Open();
                    if (plc.IsConnected)
                    {
                        int dBLimit = itemWorkCenter.DataBlockLimit;
                        limitsCpu = new LimitsCpu();
                        Thread.Sleep(waitingTime);
                        limitsCpu.PositionWrite = GetPlcRead(plc, dBLimit, 0);
                        limitsCpu.PositionRead = GetPlcRead(plc, dBLimit, 2);
                        limitsCpu.PositionMin = GetPlcRead(plc, dBLimit, 4);
                        limitsCpu.PositionMax = GetPlcRead(plc, dBLimit, 6);

                        if (limitsCpu.PositionWrite < limitsCpu.PositionMin)
                        {
                            // was "0"
                            // When PositionWrite < PositionMin
                            return null;
                        }

                        return limitsCpu;
                    }
                    else
                    {
                        return null;
                        //TODO need a logger
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                //TODO need a logger
            }
        }

        private int GetPlcRead(Plc plc, int dataBlock, int startByteAdress)
        {
            return Convert.ToInt32(plc.Read(DataType.DataBlock, dataBlock, startByteAdress, VarType.Int, 1));
        }

        public void WritePositionLimitsCpu(int newPositionRead)
        {
            try
            {
                using (var plc = new Plc(
                    cpuType,
                    itemWorkCenter.PLCIp,
                    (short)itemWorkCenter.RackCPU,
                    (short)itemWorkCenter.SlotCPU))   //"172.17.132.200"       "127.0.0.1"
                {
                    Thread.Sleep(waitingTime);
                    plc.Open();
                    if (plc.IsConnected)
                    {
                        statusConnection = true;
                        plc.Write(dbPLC + itemWorkCenter.DataBlockLimit + dbwPLC, newPositionRead);
                    }
                    else
                    {
                        statusConnection = false;
                        //TODO need a logger
                    }
                }
            }
            catch (Exception ex)
            {
                statusConnection = false;
                //TODO need a logger
            }
        }

        public void WritePositionLimitsCpu1(int nDb, int newPositionRead)
        {
            try
            {
                using (var plc = new Plc(
                    cpuType,
                    itemWorkCenter.PLCIp,
                    (short)itemWorkCenter.RackCPU,
                    (short)itemWorkCenter.SlotCPU))   //"172.17.132.200"       "127.0.0.1"
                {
                    Thread.Sleep(waitingTime);
                    plc.Open();
                    if (plc.IsConnected)
                    {
                        statusConnection = true;
                        //plc.Write("DB3.DBW2", 44);
                        plc.Write(DataType.DataBlock, 3, 0, newPositionRead);
                        //plc.Write(dbPLC + itemWorkCenter.DataBlockLimit + ".DBW" + nDb, newPositionRead);
                    }
                    else
                    {
                        statusConnection = false;
                        //TODO need a logger
                    }
                }
            }
            catch (Exception ex)
            {
                statusConnection = false;
                //TODO need a logger
            }
        }

        // IF value = 0..9 THERE value = "0" + value
        public string NormalIntToString(int value)
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

        /// <summary>
        /// if(PositionRead == PositionWrite) checkWriteReadEqually = true;
        /// </summary>
        private void CheckWriteReadEqually(LimitsCpu limitsCpu)
        {
            if (limitsCpu.PositionRead == limitsCpu.PositionWrite)
            {
                equalityCheck = true;
            }
            else
            {
                equalityCheck = false;
            }
        }
    }
}
