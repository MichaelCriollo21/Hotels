using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotels.Entity.DTO;

namespace Hotels.BusinessLogic.Interface
{
    public interface IAuthentication
    {
        Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);
    }
}
