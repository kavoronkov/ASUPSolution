using SeeAllClassLibrary.Directories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Models
{
    public class WorkCenter
    {
        // [Display(Name = "Идентификатор автоматизированной системы", ResourceType = typeof(WorkCenter))]
        [Key] // primary key
        [Required] // is not null
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkCenterId { get; set; }

        // [Display(Name = "IP контроллера", ResourceType = typeof(WorkCenter))]
        [Required] // is not null
        [StringLength(32)] // [MaxLength(32)]
        public string PLCIp { get; set; }

        // [Display(Name = "Номер стойки CPU", ResourceType = typeof(WorkCenter))]
        [Required] // is not null
        public int RackCPU { get; set; }

        // [Display(Name = "Номер слота CPU", ResourceType = typeof(WorkCenter))]
        [Required] // is not null
        public int SlotCPU { get; set; }

        // [Display(Name = "Номер блока данных с min / max", ResourceType = typeof(WorkCenter))]
        [Required] // is not null
        public int DataBlockLimit { get; set; }

        // [Display(Name = "Номер блока данных с датой и временем", ResourceType = typeof(WorkCenter))]
        [Required] // is not null
        public int DataBlockDatetime { get; set; }

        // [Display(Name = "Величина / значение микропростоя", ResourceType = typeof(WorkCenter))]
        [Required] // is not null
        public int MicroDowntime { get; set; }

        // [Display(Name = "Время цикла", ResourceType = typeof(WorkCenter))]
        [Required] // is not null
        public int Cycle { get; set; }

        // [Display(Name = "Работа / простой", ResourceType = typeof(SeeAllSettings))]
        [Required] // is not null
        public bool Work { get; set; }

        // [Display(Name = "Время перехода смен", ResourceType = typeof(WorkCenter))]
        [Required] // is not null
        [StringLength(32)] // [MaxLength(32)]
        public string Transitions { get; set; }

        [Required] // is not null
        public int PointId { get; set; }

        [ForeignKey("PointId")] // foreign key
        public virtual Point Point { get; set; }
    }
}
