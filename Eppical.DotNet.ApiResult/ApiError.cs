using System.Collections.Generic;

namespace Eppical.DotNet.ApiResult
{
    public class ApiError
    {

        public ApiError()
        {
            FieldErrors = new List<FieldError>();
        }
        public string GeneralMessage { get; set; }
        public List<FieldError> FieldErrors { get; set; }
    }

}
