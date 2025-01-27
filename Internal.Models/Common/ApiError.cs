using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedModels.Enums.Enums;

namespace Internal.Models.Common
{
    public class ApiError
    {
        public int ErrorCode { get; set; }
        public string ErrorName { get; set; }
        public string ErrorMessage { get; set; }
        public ApiError() { }

        public ApiError(ApiErrorCode code, string msg)
        {
            ErrorCode = (int)code;
            ErrorName = code.ToString();
            ErrorMessage = msg;
        }
    }
}
