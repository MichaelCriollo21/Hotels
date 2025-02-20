using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Entity.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;

        public string Type { get; set; } = string.Empty;
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
        public int MaxCapacity { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
