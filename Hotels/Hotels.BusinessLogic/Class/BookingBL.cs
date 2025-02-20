using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotels.BusinessLogic.Interface;
using Hotels.DataAccess.Class;
using Hotels.DataAccess.Interface;
using Hotels.Entity.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hotels.BusinessLogic.Class
{
    public class BookingBL : IBooking, IDisposable
    {
        private readonly IUnitOfWork UnitOfWork;
        private Repository<Booking> BookingRepository;
        private Repository<Room> RoomRepository;

        public BookingBL(IUnitOfWork UoW)
        {
            this.UnitOfWork = UoW;
            BookingRepository = UnitOfWork.Repository<Booking>();
            RoomRepository = UnitOfWork.Repository<Room>();
        }

        public Booking Create(Booking booking)
        {
            var room = RoomRepository.FindByEntity(r => r.Id == booking.RoomId && r.IsEnabled, new string[] { "Hotel" });

            if (room == null)
                throw new Exception("La habitación no está disponible.");

            using (IDbContextTransaction transaction = UnitOfWork.GetContext().Database.BeginTransaction())
            {
                try
                {
                    booking.RoomId = booking.RoomId;

                    BookingRepository.Create(booking);
                    UnitOfWork.SaveChanges();

                    transaction.Commit();
                    return booking;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IEnumerable<Booking> FindAllByAgent(int agentId)
        {
            return BookingRepository.FindAll(new string[]
            {
                "Room",
                "Room.Hotel",
                "Room.Hotel.Agent",
                "User"
            }).Where(b => b.Room.Hotel.Agent.Id == agentId && b.Room.Hotel.Agent.Role.Name == "Agency");
        }

        public Booking FindById(int Id)
        {
            return BookingRepository.FindById(Id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
