using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Micro_Project.MyDbClass
{
    public class Client
    {
        [Key]
        public int idClient { get; set; }
        [Required]
        public string name { get; set; }

        public string surname { get; set; }

        public string lastname { get; set; }

        public string login { get; set; }

        public string password { get; set; }

        public string seriaPassport { get; set; }

        public string numberPassport { get; set; }

        public string deschotel { get; set; }
        [Column(TypeName = "date")]
        public DateTime dataOfBirth { get; set; }

        public int? countDaysLive { get; set; }

        public virtual ICollection<RegHotel> RegHotel { get; set; }

    }
}
