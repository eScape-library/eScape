using eScape.Entities;
using eScape.UseCase.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.UseCase.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<bool> UpdateRefreshToken(RefreshToken refreshToken, string action);
        Task<string?> RetrieveUserNameByRefreshToken(string refreshToken);
    }
}
