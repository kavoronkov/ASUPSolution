using Newtonsoft.Json;
using S7.Net;
using SeeAllClassLibrary.Abstract;
using SeeAllClassLibrary.Directories;
using SeeAllClassLibrary.Settings;
using SeeAllClassLibrary.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace SeeAllClassLibrary.Models
{
    public class WorkCenter
    {
        // [Display(Name = "Идентификатор автоматизированной системы", ResourceType = typeof(WorkCenter))]
        [Key] // primary key
        [Required] // is not null
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkCenterId { get; set; }

        // [Display(Name = "Настройки рабочего центра", ResourceType = typeof(WorkCenter))]
        [Required] // is not null
        public string WorkCenterSettings { get; set; }

        [Required] // is not null
        public int PointId { get; set; }

        [ForeignKey("PointId")] // foreign key
        public virtual Point Point { get; set; }

        [NotMapped]
        public Dictionary<string, string> DictionaryWorkCenterSettings { get; set; }

        public void DictionarySerializeJSON()
        {
            WorkCenterSettings = JsonConvert.SerializeObject(DictionaryWorkCenterSettings, Formatting.Indented);
        }

        public void JSONDeserializeDictionary()
        {
            DictionaryWorkCenterSettings = JsonConvert.DeserializeObject<Dictionary<string, string>>(WorkCenterSettings);
        }

        public WorkCenter()
        {
            
        }

        public WorkCenter(Dictionary<string, string> dictionary, int pointId)
        {
            DictionaryWorkCenterSettings = dictionary;
            DictionarySerializeJSON();
            PointId = pointId;
        }
    }

    // [Display(Name = "IP контроллера", ResourceType = typeof(WorkCenter))]
    // [Required] // is not null
    // [StringLength(32)] // [MaxLength(32)]
    // public string PlcIp { get; set; }

    // [Display(Name = "Тип CPU", ResourceType = typeof(WorkCenter))]
    // [Required] // is not null
    // public int CpuType { get; set; }

    // [Display(Name = "Номер стойки CPU", ResourceType = typeof(WorkCenter))]
    // [Required] // is not null
    // public int CpuRack { get; set; }

    // [Display(Name = "Номер слота CPU", ResourceType = typeof(WorkCenter))]
    // [Required] // is not null
    // public int CpuSlot { get; set; }

    // [Display(Name = "Номер блока данных с min / max", ResourceType = typeof(WorkCenter))]
    // [Required] // is not null
    // public int DataBlockLimit { get; set; }

    // [Display(Name = "Номер блока данных с датой и временем", ResourceType = typeof(WorkCenter))]
    // [Required] // is not null
    // public int DataBlockDatetime { get; set; }

    // [Display(Name = "Величина / значение микропростоя", ResourceType = typeof(WorkCenter))]
    // [Required] // is not null
    // public int MicroDowntime { get; set; }

    // [Display(Name = "Время цикла", ResourceType = typeof(WorkCenter))]
    // [Required] // is not null
    // public int Cycle { get; set; }

    // [Display(Name = "Работа / простой", ResourceType = typeof(WorkCenter))]
    // [Required] // is not null
    // public bool Work { get; set; }

    // [Display(Name = "Время перехода смен", ResourceType = typeof(WorkCenter))]
    // [Required] // is not null
    // [StringLength(32)] // [MaxLength(32)]
    // public string Transitions { get; set; }

    // [NotMapped]
    // public WorkCenterSettings _WorkCenterSettings
    // {
    //     get { return WorkCenterSettings == null ? null : JsonConvert.DeserializeObject<WorkCenterSettings>(WorkCenterSettings); }
    //     set { WorkCenterSettings = JsonConvert.SerializeObject(value); }
    // }

    // [NotMapped]
    // public Dictionary<string, string> _WorkCenterSettings
    // {
    //     get { return DictionaryWorkCenterSettings == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(this.WorkCenterSettings); }
    //     set { WorkCenterSettings = JsonConvert.SerializeObject(DictionaryWorkCenterSettings, Formatting.Indented); }
    // }
}

