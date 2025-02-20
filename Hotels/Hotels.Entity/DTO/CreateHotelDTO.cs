using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Entity.DTO
{
    public class CreateHotelDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
    }
}
