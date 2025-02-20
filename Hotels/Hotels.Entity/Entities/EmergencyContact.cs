using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Entity.Entities
{
    public class EmergencyContact
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public string FullName { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
    }
}
