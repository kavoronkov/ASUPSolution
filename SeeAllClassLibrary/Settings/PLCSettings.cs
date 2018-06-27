using SeeAllClassLibrary.Directories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Settings
{
    public class PLCSettings
    {
        // [Display(Name = "Идентификатор контроллера", ResourceType = typeof(PLCSettings))]
        [Key] // primary key
        [Required] // is not null
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PLCId { get; set; }

        // [Display(Name = "IP контроллера", ResourceType = typeof(PLCSettings))]
        [Required] // is not null
        [StringLength(32)] // [MaxLength(32)]
        public string PLCIp { get; set; }

        // [Display(Name = "Номер стойки CPU", ResourceType = typeof(PLCSettings))]
        [Required] // is not null
        public int RackCPU { get; set; }

        // [Display(Name = "Номер слота CPU", ResourceType = typeof(PLCSettings))]
        [Required] // is not null
        public int SlotCPU { get; set; }

        // [Display(Name = "Номер блока данных с min / max", ResourceType = typeof(PLCSettings))]
        [Required] // is not null
        public int DataBlockLimit { get; set; }

        // [Display(Name = "Номер блока данных с датой и временем", ResourceType = typeof(PLCSettings))]
        [Required] // is not null
        public int DataBlockDatetime { get; set; }

        [Required] // is not null
        public int PointId { get; set; }

        [ForeignKey("PointId")] // foreign key
        public virtual Point Point { get; set; }
    }
}
