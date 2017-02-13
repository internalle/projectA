using ProjectA.Core;
using ProjectA.Core.Features.Measurements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Services.Features.Measurements
{
    public class ListAllUnits
    {
        public class Request : BaseRequest<Response>
        {
        }

        public class Response : BaseResponse
        {
            public IEnumerable<MeasurementUnit> Units { get; set; }
        }
        
        public class Handler : BaseHandler<Response, Request>
        {
            public override Response Handle(Request request)
            {                
                return new Response
                {
                    Units = MeasurementUnit.Query()
                };
            }
        }

    }
}
