using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotels.BusinessLogic.Class;
using Hotels.Entity.Entities;

namespace Hotels.BusinessLogic.Interface
{
    public interface IBooking
    {
        Booking Create(Booking booking);
        IEnumerable<Booking> FindAllByAgent(int agentId);
        Booking FindById(int Id);
    }
}
