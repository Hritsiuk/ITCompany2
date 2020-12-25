using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITCompany.Data.Entities
{
    public class UserInformation
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid Id_user { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public double Hours { get; set; }
    }
}
