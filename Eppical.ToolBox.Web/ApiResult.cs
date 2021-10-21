using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace Eppical.ToolBox.Web
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

        //public ApiResult(string error)
        //{
        //    AddError(error);
        //}

        public ApiResult(Exception ex) // : this("Server error.")
        {
            AddError(ex.Message);

            //HttpCode = (int)HttpStatusCode.InternalServerError;
            //Code = 500;
        }





        public void AddError(string Field, string Message)
        {
            if (Error == null)
                Error = new ApiError();

            Error.FieldErrors.Add(new FieldError() { Field = Field, Error = Message });
        }









    }

    public class ApiError
    {

        public ApiError()
        {
            FieldErrors = new List<FieldError>();
        }
        public string GeneralMessage { get; set; }
        public List<FieldError> FieldErrors { get; set; }
    }

    public class FieldError
    {
        public string Field { get; set; }
        public string Error { get; set; }
    }
}
