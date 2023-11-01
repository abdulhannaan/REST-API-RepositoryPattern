using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Model.Dtos
{
    public class ApiResponseModel
    {
        public ApiResponseModel()
        {
            this.Message = "Request Failed.";
            this.Status = HttpStatusCode.InternalServerError;
        }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }

        public static ApiResponseModel GetResponse(string message, HttpStatusCode statusCode, object data = null)
        {
            ApiResponseModel response = new ApiResponseModel
            {
                Status = statusCode,
                Message = message,
                Response = data
            };
            return response;
        }
    }
}
