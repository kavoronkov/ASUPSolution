using SeeAllClassLibrary.Directories;
using SeeAllClassLibrary.Models;
using SeeAllClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeeAllClassLibrary.Controllers;

namespace SeeAllClassLibrary.Initializers
{
    public class SeeAllClassLibraryDbInitializer : DropCreateDatabaseAlways<EFSeeAllContext>
    {
        protected override void Seed(EFSeeAllContext context)
        {
            context.Plants.Add(new Plant { PlantName = "Interpipe_NTRP" });
            context.SaveChanges();

            var tempPlantId = context.Plants.FirstOrDefault(x => x.PlantName == "Interpipe_NTRP").PlantId;
            context.Workshops.Add(new Workshop { WorkshopName = "KPC_Interpipe_NTRP", PlantId = tempPlantId });
            context.Workshops.Add(new Workshop { WorkshopName = "TPC3_Interpipe_NTRP", PlantId = tempPlantId });
            context.Workshops.Add(new Workshop { WorkshopName = "TPC4_Interpipe_NTRP", PlantId = tempPlantId });
            context.Workshops.Add(new Workshop { WorkshopName = "TPC5_Interpipe_NTRP", PlantId = tempPlantId });
            context.SaveChanges();

            // var tempWorkshopId = context.Workshops.FirstOrDefault(x => x.WorkshopName == "KPC_Interpipe_NTRP").WorkshopId;
            // context.Departments.Add(new Department { DepartmentName = "ZagotovitelnyyUchastok_KPC_Interpipe_NTRP", WorkshopId = tempWorkshopId });
            // context.Departments.Add(new Department { DepartmentName = "UchastokProkata_KPC_Interpipe_NTRP", WorkshopId = tempWorkshopId });
            // context.Departments.Add(new Department { DepartmentName = "MehObrabotka1_KPC_Interpipe_NTRP", WorkshopId = tempWorkshopId });
            // context.Departments.Add(new Department { DepartmentName = "TermoUchastok_KPC_Interpipe_NTRP", WorkshopId = tempWorkshopId });
            // context.Departments.Add(new Department { DepartmentName = "MehObrabotka2_KPC_Interpipe_NTRP", WorkshopId = tempWorkshopId });
            // context.Departments.Add(new Department { DepartmentName = "EksportnyyUchastok_KPC_Interpipe_NTRP", WorkshopId = tempWorkshopId });
            // context.Departments.Add(new Department { DepartmentName = "UFOK_KPC_Interpipe_NTRP", WorkshopId = tempWorkshopId });
            // -------------------------------------------------------------------------------------------------------
            context.Departments.Add(new Department
            {
                DepartmentName = "UchastokProkata_KPC_Interpipe_NTRP",
                WorkshopId = context.Workshops.FirstOrDefault(x => x.WorkshopName == "KPC_Interpipe_NTRP").WorkshopId
            });
            context.Departments.Add(new Department {
                DepartmentName = "UchastokProkata_TPC3_Interpipe_NTRP",
                WorkshopId = context.Workshops.FirstOrDefault(x => x.WorkshopName == "TPC3_Interpipe_NTRP").WorkshopId
            });
            context.Departments.Add(new Department {
                DepartmentName = "UchastokProkata_TPC4_Interpipe_NTRP",
                WorkshopId = context.Workshops.FirstOrDefault(x => x.WorkshopName == "TPC4_Interpipe_NTRP").WorkshopId
            });
            context.Departments.Add(new Department {
                DepartmentName = "UchastokProkata_TPC5_Interpipe_NTRP",
                WorkshopId = context.Workshops.FirstOrDefault(x => x.WorkshopName == "TPC5_Interpipe_NTRP").WorkshopId
            });
            context.SaveChanges();
            // -------------------------------------------------------------------------------------------------------
            context.Points.Add(new Point {
                PointName = "Prokat_UchastokProkata_KPC_Interpipe_NTRP",
                DepartmentId = context.Departments.FirstOrDefault(x => x.DepartmentName == "UchastokProkata_KPC_Interpipe_NTRP").DepartmentId
            });                                                                             
            context.Points.Add(new Point                                                    
            {                                                                               
                PointName = "Prokat_UchastokProkata_TPC3_Interpipe_NTRP",                   
                DepartmentId = context.Departments.FirstOrDefault(x => x.DepartmentName == "UchastokProkata_TPC3_Interpipe_NTRP").DepartmentId
            });                                                                             
            context.Points.Add(new Point                                                    
            {                                                                               
                PointName = "Prokat_UchastokProkata_TPC4_Interpipe_NTRP",                   
                DepartmentId = context.Departments.FirstOrDefault(x => x.DepartmentName == "UchastokProkata_TPC4_Interpipe_NTRP").DepartmentId
            });                                                                             
            context.Points.Add(new Point                                                    
            {                                                                               
                PointName = "Prokat_UchastokProkata_TPC5_Interpipe_NTRP",                   
                DepartmentId = context.Departments.FirstOrDefault(x => x.DepartmentName == "UchastokProkata_TPC5_Interpipe_NTRP").DepartmentId
            });
            context.SaveChanges();
            // -------------------------------------------------------------------------------------------------------

            WorkCenter workCenter = new WorkCenter();
            workCenter.DictionaryWorkCenterSettings = new Dictionary<string, string>
            {
                ["PlcName"] = "Siemens",
                ["PlcIp"] = "192.168.1.200",
                ["ThreadSleepTime"] = "100",
                ["NumberAttemptsConnection"] = "10",
                ["ConnectionSleepTime"] = "100",
                ["TypeCpu"] = "30",
                ["RackCpu"] = "0",
                ["SlotCpu"] = "1",
                ["DataBlockDatetime"] = "2",
                ["ByteStepDB"] = "12",
                ["ExNumberOverflowDB"] = "-2146233088",
                ["Transitions"] = "7,15,23",
                ["MicroDowntime"] = "60",
                ["Cycle"]= "120",
                ["Work"] = "false"
            };
            workCenter.DictionarySerializeJSON();
            workCenter.PointId = context.Points.FirstOrDefault(x => x.PointName == "Prokat_UchastokProkata_TPC5_Interpipe_NTRP").PointId;

            context.WorkCenters.Add(workCenter);
            context.SaveChanges();

            // context.WorkCenters.Add(new WorkCenter
            // {
            //     PLCIp = "192.168.1.200",
            //     CpuType = 30,
            //     RackCPU = 0,
            //     SlotCPU = 1,
            //     DataBlockDatetime = 2,
            //     // DataBlockLimit = 3,                
            //     MicroDowntime = 60,
            //     Cycle = 120,
            //     Work = false,
            //     Transitions = "7,15,23",
            //     SettingsPLC = "ThreadSleepTime:100, ",
            //     PointId = context.Points.FirstOrDefault(x => x.PointName == "Prokat_UchastokProkata_TPC5_Interpipe_NTRP").PointId
            // });

            // context.WorkCenters.Add(new WorkCenter
            // {
            //     PLCIp = "192.168.0.201",
            //     CpuType = 4,
            //     RackCPU = 0,
            //     SlotCPU = 2,
            //     // DataBlockLimit = 41,
            //     DataBlockDatetime = 35,
            //     MicroDowntime = 60,
            //     Cycle = 120,
            //     Work = false,
            //     Transitions = "7,15,23",
            //     PointId = context.Points.FirstOrDefault(x => x.PointName == "Prokat_UchastokProkata_TPC5_Interpipe_NTRP").PointId
            // });
            // context.SaveChanges();
            // -------------------------------------------------------------------------------------------------------

            new StarterPoints();
        }
    }
}
