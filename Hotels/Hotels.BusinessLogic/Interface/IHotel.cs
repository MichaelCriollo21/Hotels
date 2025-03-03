﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotels.Entity.DTO;
using Hotels.Entity.Entities;

namespace Hotels.BusinessLogic.Interface
{
    public interface IHotel 
    {
        Hotel Create(Hotel entity);
        Hotel Update(Hotel entity);
        Hotel ChangeStatus(int Id);
    }
}
