using SeeAllClassLibrary.Directories;
using SeeAllClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Initializers
{
    public class SeeAllClassLibraryDbInitializer : DropCreateDatabaseAlways<EFDirectoriesContext>
    {
        protected override void Seed(EFDirectoriesContext context)
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
        }
    }
}
