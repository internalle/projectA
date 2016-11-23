using ProjectA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Framework.Messaging
{
    public class Result<T> where T : BaseResponse
    {
        public T Response { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public Exception Exception { get; set; }
    }
}
