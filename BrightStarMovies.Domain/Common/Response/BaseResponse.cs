using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightStarMovies.Domain.Common.Response
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }

        public BaseResponse(string message, bool status, T data)
        {
            Message = message;
            Status = status;
            Data = data;
        }

        public BaseResponse(string message, bool status, object error)
        {
            Message = message;
            Status = status;
            Error = error;
        }
    }
}
