using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCompany.Models
{
    public class CreateEventViewModel
    {
        public Guid Id;
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public List<string> Users { get; set; }
    }
}
