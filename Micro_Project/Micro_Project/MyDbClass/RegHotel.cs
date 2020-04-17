using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Micro_Project.MyDbClass
{
    public class RegHotel
    {
        [Key]
        public int idLive { get; set; }

        [ForeignKey("Client")]
        public int idClient { get; set; }
        public Client Client { get; set; }

        [ForeignKey("HotelRoom")]
        public int idFlat { get; set; }
        public HotelRoom HotelRoom { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime startLive { get; set; }
        [Column(TypeName = "date")]
        public DateTime endLive { get; set; }
        public string desclive { get; set; }

    }
}
