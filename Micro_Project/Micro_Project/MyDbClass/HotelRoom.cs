using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Micro_Project.MyDbClass
{
    public class HotelRoom
    {
        [Key]
        public int idFlat { get; set; }
        [Required]
        public string nomer { get; set; }
        public int countPeople { get; set; }
        public string category { get; set; }
        public int payment { get; set; }
        public virtual ICollection<RegHotel> RegHotel { get; set; }
    }
}
