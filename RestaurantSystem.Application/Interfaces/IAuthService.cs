using RestaurantSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDTO registerDTO);
        Task<ResponseLoginDTO> LoginAsync(LoginDTO loginDTO);

    }
}
