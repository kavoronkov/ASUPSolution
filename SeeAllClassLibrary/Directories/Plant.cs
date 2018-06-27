using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeAllClassLibrary.Directories
{
    public class Plant
    {
        // [Display(Name = "Идентификатор актива / завода / предприятия", ResourceType = typeof(Plant))]
        [Key] // primary key
        [Required] // is not null
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlantId { get; set; }

        // [Display(Name = "Наименование актива / завода / предприятия", ResourceType = typeof(Plant))]
        [Required] // is not null
        [StringLength(64)] // [MaxLength(64)]
        public string PlantName { get; set; }


        public virtual ICollection<Workshop> Workshops { get; set; }

        public Plant()
        {
            Workshops = new List<Workshop>();
        }
    }
}
