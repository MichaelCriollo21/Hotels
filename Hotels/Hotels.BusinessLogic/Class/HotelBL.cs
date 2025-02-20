using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotels.BusinessLogic.Interface;
using Hotels.DataAccess.Class;
using Hotels.DataAccess.Interface;
using Hotels.Entity.DTO;
using Hotels.Entity.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hotels.BusinessLogic.Class
{
    public class HotelBL : IHotel, IDisposable
    {
        private readonly IUnitOfWork UnitOfWork;
        private Repository<Hotel> HotelRepository;
        private Repository<Room> RoomRepository;
        public HotelBL(IUnitOfWork UoW)
        {
            this.UnitOfWork = UoW;
            HotelRepository = UnitOfWork.Repository<Hotel>();
            RoomRepository = UnitOfWork.Repository<Room>();
        }

        public Hotel Create(Hotel entity)
        {
            using (IDbContextTransaction transaction = UnitOfWork.GetContext().Database.BeginTransaction())
            {
                try
                {
                    HotelRepository.Create(entity);
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Hotel> Find(Hotel entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hotel> FindAll(string[] IncludeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Hotel FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public Hotel Update(Hotel entity)
        {
            using (IDbContextTransaction transaction = UnitOfWork.GetContext().Database.BeginTransaction())
            {
                try
                {
                    Hotel entityDB = HotelRepository.FindById(entity.Id);
                    if (entityDB != null)
                    {
                        entity.Id = entityDB.Id;
                        HotelRepository.Update(entity);
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
    }
}
