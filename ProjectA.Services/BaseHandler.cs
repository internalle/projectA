using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Services
{
    public abstract class BaseHandler<TResponse, TRequest> 
        where TResponse : BaseResponse 
        where TRequest: BaseRequest<TResponse> 
    {
        public abstract TResponse Handle(TRequest request);
    }
}
