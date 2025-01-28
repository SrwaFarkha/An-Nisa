using SharedModels.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        Task<string?> GetToken(LoginDto login);
    }
}
