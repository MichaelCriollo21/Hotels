using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Entity.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalCost { get; set; }
        public bool IsConfirmed { get; set; }

        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
        public EmergencyContact? EmergencyContact { get; set; }
    }
}
