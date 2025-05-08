using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaExchange.Domain.SeedWork
{
    public class BaseException : Exception
    {
        public string ErrorCode { get; }
        public string ErrorMessage { get; }

        public BaseException(string errorCode, string errorMessage)
            : base(errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public BaseException()
        {
        }
    }
}
