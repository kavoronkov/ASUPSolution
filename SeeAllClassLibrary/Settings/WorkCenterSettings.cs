using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Settings
{
    public class WorkCenterSettings
    {
        public string PlcName { get; set; }
        public string PlcIp { get; set; }
        public int ThreadSleepTime { get; set; }
        public int NumberAttemptsConnection { get; set; }
        public int ConnectionSleepTime { get; set; }

        public int TypeCpu { get; set; }
        public int RackCpu { get; set; }
        public int SlotCpu { get; set; }
        public int DataBlockDatetime { get; set; }

        public int ByteStepDB { get; set; }
        public long ExNumberOverflowDB { get; set; }

        public string Transitions { get; set; }
        public int MicroDowntime { get; set; }
        public int Cycle { get; set; }
        public bool Work { get; set; }

        public WorkCenterSettings(string plcName, string plcIp, int threadSleepTime, int numberAttemptsConnection, int connectionSleepTime,
            int typeCpu, int rackCpu, int slotCpu, int dataBlockDatetime, int byteStepDB, long exNumberOverflowDB,
            string transitions, int microDowntime, int cycle, bool work)
        {
            PlcName = plcName;
            PlcIp = plcIp;
            ThreadSleepTime = threadSleepTime;
            NumberAttemptsConnection = numberAttemptsConnection;
            ConnectionSleepTime = connectionSleepTime;

            TypeCpu = typeCpu;
            RackCpu = rackCpu;
            SlotCpu = slotCpu;
            DataBlockDatetime = dataBlockDatetime;

            ByteStepDB = byteStepDB;
            ExNumberOverflowDB = exNumberOverflowDB;

            Transitions = transitions;
            MicroDowntime = microDowntime;
            Cycle = cycle;
            Work = work;
        }

        public CpuType GetTypeCPU()
        {
            switch (TypeCpu)
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

        // [Display(Name = "Идентификатор контроллера", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public string PlcName { get; set; }

        // [Display(Name = "IP контроллера", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // [StringLength(32)] // [MaxLength(32)]
        // public string PlcIp { get; set; }

        // [Display(Name = "Thread sleep time for open connection to PLC", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public int ThreadSleepTime { get; set; }

        // [Display(Name = "Number attempts for connection to PLC", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public int NumberAttemptsConnection { get; set; }

        // [Display(Name = "Thread sleep time for loading datetime of PLC", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public int ConnectionSleepTime { get; set; }

        // [Display(Name = "Тип CPU S7.Net", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public int TypeCpu { get; set; }

        // [Display(Name = "Номер стойки CPU", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public int RackCpu { get; set; }

        // [Display(Name = "Номер слота CPU", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public int SlotCpu { get; set; }

        // [Display(Name = "Номер блока данных с датой и временем", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public int DataBlockDatetime { get; set; }

        // [Display(Name = "Шаг блока данных в байтах ", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public int ByteStepDB { get; set; }

        // [Display(Name = "Код ошибки при обращении к блоку данных", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public int ExNumberOverflowDB { get; set; }

        // [Display(Name = "Время перехода смен", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // [StringLength(32)] // [MaxLength(32)]
        // public string Transitions { get; set; }

        // [Display(Name = "Величина / значение микропростоя", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public int MicroDowntime { get; set; }

        // [Display(Name = "Время цикла", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public int Cycle { get; set; }

        // [Display(Name = "Работа / простой", ResourceType = typeof(WorkCenterSettings))]
        // [Required] // is not null
        // public bool Work { get; set; }
    }
}
