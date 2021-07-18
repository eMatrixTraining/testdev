using System;
using TestDev.Data.Enums;

namespace TestDev.Models
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            PayLoad = null;
        }

        public CustomHttpResponseStatus Status { get; set; }
        public string Message { get; set; }
        public Object PayLoad { get; set; }
    }
}
