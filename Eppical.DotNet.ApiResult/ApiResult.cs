using System;
using System.Net;
using System.Text.Json.Serialization;

namespace Eppical.DotNet.ApiResult
{
    public class ApiResult<T> where T : class
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public int Code { get; set; }
        public ApiError Error { get; set; }
        [JsonIgnore]
        public int HttpCode { get; set; }

        public void AddError(string message)
        {
            HttpCode = (int)HttpStatusCode.BadRequest;
            Message = message;
            Code = 400;
        }

        public ApiResult()
        {
            HttpCode = (int)HttpStatusCode.BadRequest;
            Code = 400;
            Data = null;
        }

        public ApiResult(T data)
        {
            HttpCode = (int)HttpStatusCode.OK;
            Code = 0;
            Data = data;
        }

        public ApiResult(Exception ex)
        {
            AddError(ex.Message);
        }

        public void AddError(string Field, string Message)
        {
            if (Error == null)
                Error = new ApiError();

            Error.FieldErrors.Add(new FieldError() { Field = Field, Error = Message });
        }

    }

}
