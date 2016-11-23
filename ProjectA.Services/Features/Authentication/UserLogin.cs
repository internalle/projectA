using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Services.Features.Authentication
{
    public class UserLogin
    {
        public class Request : BaseRequest<Response>
        {
        }

        public class Response : BaseResponse
        {
        }

        public class Validator : BaseValidator<Response, Request>
        {
            public Validator()
            {
            }
        }

        public class Handler : BaseHandler<Response, Request>
        {
            public override Response Handle(Request request)
            {
                return new Response();
            }
        }
    }
}