using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITCompany.Data.Entities
{
    public class EventItem
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Название события")]
        public string Name { get; set; }

        [DataType(DataType.Time)]
        public DateTime DateStart { get; set; }

        [DataType(DataType.Time)]
        public DateTime DateEnd { get; set; }
    }
}
