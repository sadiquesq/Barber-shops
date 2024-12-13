using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Barber_shops.ApiRespones
{
    public class ApiRespones<T>
    {

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public string Error { get; set; }

        public ApiRespones(int statuscode, string message, T data = default(T), string error = null)
        {
            StatusCode = statuscode;
            Message = message;
            Data = data;
            Error = error;
        }

    }
}
