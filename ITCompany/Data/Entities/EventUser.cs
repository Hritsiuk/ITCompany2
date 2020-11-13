using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITCompany.Data.Entities
{
    public class EventUser
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public Guid EventId { get; set; }
    }
}
