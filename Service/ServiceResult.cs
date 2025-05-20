using System.Net;
using System.Text.Json.Serialization;

namespace App.Services
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? ErrorMessages{ get; set; }

        [JsonIgnore] public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;

        [JsonIgnore] public bool IsFail => !IsSuccess;

        [JsonIgnore] public HttpStatusCode Status { get; set; }

        [JsonIgnore] public string? UrlAsCreated { get; set; }
        public static ServiceResult<T> Success(T data, HttpStatusCode statusCode=HttpStatusCode.OK)
        {
            return new ServiceResult<T>()
            {
                Data = data,
                Status = statusCode
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T data,string urlAsCreated)
        {
            return new ServiceResult<T>()
            {
                Data = data,
                Status = HttpStatusCode.Created,
                UrlAsCreated = urlAsCreated
            };
        }
        //static factory method ile newlemeyi kontrol altına alıyoruz

        public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessages = new List<string> { errorMessage },
                Status = statusCode
            };
        }
        public static ServiceResult<T> Fail(List<string> errorMessages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>() 
            {
                ErrorMessages = errorMessages,
                Status = statusCode
            };
        }


    }
    public class ServiceResult
    {
        
        public List<string>? ErrorMessages { get; set; }
        [JsonIgnore]
        public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }

        public static ServiceResult Success( HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult()
            {
             
                Status = statusCode
            };
        }
        //static factory method ile newlemeyi kontrol altına alıyoruz

        public static ServiceResult Fail(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessages = new List<string> { errorMessage },
                Status = statusCode
            };
        }
        public static ServiceResult Fail(List<string> errorMessages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessages = errorMessages,
                Status = statusCode
            };
        }


    }
}
