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
    public class SeeAllSettings
    {
        // [Display(Name = "Идентификатор автоматизированной системы", ResourceType = typeof(SeeAllSettings))]
        [Key] // primary key
        [Required] // is not null
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeeAllId { get; set; }

        // [Display(Name = "Время перехода смен", ResourceType = typeof(SeeAllSettings))]
        [Required] // is not null
        [StringLength(32)] // [MaxLength(32)]
        public string Transitions { get; set; }

        // [Display(Name = "Величина / значение микропростоя", ResourceType = typeof(SeeAllSettings))]
        [Required] // is not null
        public int MicroDowntime { get; set; }

        // [Display(Name = "Время цикла", ResourceType = typeof(SeeAllSettings))]
        [Required] // is not null
        public int Cycle { get; set; }

        // [Display(Name = "Работа / простой", ResourceType = typeof(SeeAllSettings))]
        [Required] // is not null
        public bool Work { get; set; }

        [Required] // is not null
        public int PointId { get; set; }

        [ForeignKey("PointId")] // foreign key
        public virtual Point Point { get; set; }
    }
}
