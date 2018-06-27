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
    public class Downtime
    {
        // [Display(Name = "Идентификатор простоя", ResourceType = typeof(Downtime))]
        [Key] // primary key
        [Required] // is not null
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DowntimeId { get; set; }

        // [Display(Name = "Идентификатор времени", ResourceType = typeof(Datetime))]
        // [Key] // primary key        
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required] // is not null
        public long DatetimeId { get; set; }

        // [Display(Name = "Время цикла", ResourceType = typeof(Downtime))]
        [Required] // is not null
        [StringLength(64)] // [MaxLength(64)]
        public string CycleTime { get; set; }

        // [Display(Name = "Величина / значение простоя", ResourceType = typeof(Downtime))]
        [Required] // is not null
        [StringLength(64)] // [MaxLength(64)]
        public string DowntimeValue { get; set; }

        // [Display(Name = "Микропростой", ResourceType = typeof(Downtime))]
        public bool? MicroDowntime { get; set; }

        // [Display(Name = "Смена", ResourceType = typeof(Downtime))]
        public bool? Transition { get; set; }

        [Required] // is not null
        public int PointId { get; set; }

        [ForeignKey("PointId")] // foreign key
        public virtual Point Point { get; set; }
    }
}
