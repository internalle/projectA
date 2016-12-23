using ProjectA.Core.Features.Measurements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Services.Database
{
    public class InitializeStaticData
    {

        public class Request : BaseRequest<EmptyResponse>
        {
        }
        
        public class Handler : BaseHandler<EmptyResponse, Request>
        {
            public override EmptyResponse Handle(Request request)
            {
                MeasurementUnit.Init();

                return new EmptyResponse();
            }
        }

    }
}
