using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Services
{
    public abstract class BaseValidator<TResponse, TRequest>  : AbstractValidator<TRequest>
        where TRequest : BaseRequest<TResponse> 
        where TResponse : BaseResponse
    {
    }
}
