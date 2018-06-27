using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Directories
{
    public class Department
    {
        // [Display(Name = "Идентификатор отдела / участка", ResourceType = typeof(Department))]
        [Key] // primary key
        [Required] // is not null
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        // [Display(Name = "Наименование отдела / участка", ResourceType = typeof(Department))]
        [Required] // is not null
        [StringLength(64)] // [MaxLength(64)]
        public string DepartmentName { get; set; }

        [Required] // is not null
        public int WorkshopId { get; set; }

        [ForeignKey("WorkshopId")] // foreign key
        public virtual Workshop Workshop { get; set; }

        public virtual ICollection<Point> Points { get; set; }

        public Department()
        {
            Points = new List<Point>();
        }
    }
}
