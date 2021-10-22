using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Eppical.DotNet.ApiResult
{
    [Serializable]
    public class BusinessException : Exception
    {
        public HttpStatusCode? HttpStatusCode { get; set; }
        public string FieldsError { get; set; }
        public int? StatusCode { get; set; }

        public BusinessException(string message, HttpStatusCode? httpStatus = null, int? statusCode = null, Exception inner = null) : base(message, inner)
        {
            HttpStatusCode = httpStatus;
            StatusCode = statusCode;
            FieldsError = null;
        }

        public BusinessException(Dictionary<string, string> fields, HttpStatusCode? httpStatus = null, Exception inner = null) : base("", inner)
        {
            FieldsError = string.Empty;
            foreach (var item in fields)
            {
                FieldsError += $"{item.Key}: {item.Value}. ";
            }
            HttpStatusCode = httpStatus;
        }

        protected BusinessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
