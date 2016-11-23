using ProjectA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Framework.Messaging
{
    public interface IDispatcher
    {
        Result<T> Dispatch<T>(BaseRequest<T> request) where T : BaseResponse;
    }
}
