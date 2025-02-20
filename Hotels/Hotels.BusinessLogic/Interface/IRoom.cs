using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotels.Entity.Entities;

namespace Hotels.BusinessLogic.Interface
{
    public interface IRoom 
    {
        Room Create(Room entity);
        Room Update(Room entity);
        Room ChangeStatus(int Id);
        Task<List<Room>> SearchRooms(DateTime checkIn, DateTime checkOut, int guests, string city);
        Task<List<Room>> GetRoomsByHotel(int hotelId);
    }
}
