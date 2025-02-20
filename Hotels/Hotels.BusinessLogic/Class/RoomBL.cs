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
    public class RoomBL : IRoom, IDisposable
    {
        private readonly IUnitOfWork UnitOfWork;
        private Repository<Room> RoomRepository;

        public RoomBL(IUnitOfWork UoW)
        {
            this.UnitOfWork = UoW;
            RoomRepository = UnitOfWork.Repository<Room>();
        }

        public Room ChangeStatus(int Id)
        {
            Room entityDB = new Room();
            using (IDbContextTransaction transaction = UnitOfWork.GetContext().Database.BeginTransaction())
            {
                try
                {
                    entityDB = RoomRepository.FindById(Id);
                    if (entityDB != null)
                    {
                        entityDB.IsEnabled = entityDB.IsEnabled ? false : true;
                        RoomRepository.Update(entityDB);
                        UnitOfWork.SaveChanges();
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return entityDB;
        }

        public Room Create(Room entity)
        {
            using (IDbContextTransaction transaction = UnitOfWork.GetContext().Database.BeginTransaction())
            {
                try
                {
                    RoomRepository.Create(entity);
                    UnitOfWork.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return entity;
        }

        public Room Update(Room entity)
        {
            using (IDbContextTransaction transaction = UnitOfWork.GetContext().Database.BeginTransaction())
            {
                try
                {
                    Room entityDB = RoomRepository.FindById(entity.Id);
                    if (entityDB != null)
                    {
                        entity.Id = entityDB.Id;
                        RoomRepository.Update(entity);
                        UnitOfWork.SaveChanges();
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return entity;
        }

        public async Task<List<Room>> SearchRooms(DateTime checkIn, DateTime checkOut, int guests, string city)
        {
            string[] includes = { "Hotel", "Bookings" };

            return await RoomRepository.Find(
                r => r.Hotel.City == city &&    
                     r.IsEnabled &&             
                     !r.Bookings.Any(b =>       
                        (b.CheckInDate < checkOut && b.CheckOutDate > checkIn)
                     ),
                includes
            );
        }

        public async Task<List<Room>> GetRoomsByHotel(int hotelId)
        {
            string[] includes = { "Hotel", "Bookings" };

            return await RoomRepository.Find(
                r => r.Hotel.Id == hotelId && r.IsEnabled,
                includes
            );
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
