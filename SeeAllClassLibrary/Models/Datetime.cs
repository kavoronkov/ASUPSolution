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
    public class Datetime
    {
        // [Display(Name = "Идентификатор времени", ResourceType = typeof(Datetime))]
        [Key] // primary key
        [Required] // is not null
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DatetimeId { get; set; }

        // [Display(Name = "Значение времени", ResourceType = typeof(Datetime))]
        [Required] // is not null
        public DateTime DatetimeValue { get; set; }

        [Required] // is not null
        public int PointId { get; set; }

        [ForeignKey("PointId")] // foreign key
        public virtual Point Point { get; set; }
    }
}
