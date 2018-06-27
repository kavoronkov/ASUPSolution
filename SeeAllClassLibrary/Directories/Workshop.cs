using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Directories
{
    public class Workshop
    {
        // [Display(Name = "Идентификатор цеха", ResourceType = typeof(Workshop))]
        [Key] // primary key
        [Required] // is not null
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkshopId { get; set; }

        // [Display(Name = "Наименование цеха", ResourceType = typeof(Workshop))]
        [Required] // is not null
        [StringLength(64)] // [MaxLength(64)]
        public string WorkshopName { get; set; }

        [Required] // is not null
        public int PlantId { get; set; }

        [ForeignKey("PlantId")] // foreign key
        public virtual Plant Plant { get; set; }

        public virtual ICollection<Department> Departments { get; set; }

        public Workshop()
        {
            Departments = new List<Department>();
        }
    }
}
