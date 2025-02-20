using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Entity.DTO
{
    public record LoginResponse(bool Success, string? Message, string? Token)
    {                           
    }
}
