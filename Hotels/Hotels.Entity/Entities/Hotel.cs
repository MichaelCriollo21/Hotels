using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Entity.Entities
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        [ForeignKey("Agent")]
        public int AgentId { get; set; }
        public User Agent { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }

}
