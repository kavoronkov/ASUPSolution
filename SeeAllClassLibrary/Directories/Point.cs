using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Directories
{
    public class Point
    {
        // [Display(Name = "Идентификатор оборудования / установки", ResourceType = typeof(Point))]
        [Key] // primary key
        [Required] // is not null
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PointId { get; set; }

        // [Display(Name = "Наименование оборудования / установки", ResourceType = typeof(Point))]
        [Required] // is not null
        [StringLength(64)] // [MaxLength(64)]
        public string PointName { get; set; }

        [Required] // is not null
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")] // foreign key
        public virtual Department Department { get; set; }
    }
}
