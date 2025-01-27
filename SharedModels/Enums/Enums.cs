using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Enums
{
    public class Enums
    {

        public enum ApiErrorCode
        {
            NotFound,
            ValidationFailed,
            EmailAlreadyRegistered,
            NullHttpBody,
            IncorrectPassword,
            TokenAlreadyUsed,
            LoginFailed,
            IconNotFound,
            UnauthorizedRole,
        }
    }
}
