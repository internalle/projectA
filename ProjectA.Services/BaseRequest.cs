using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Services
{
    public abstract class BaseRequest<T> where T : BaseResponse
    {
    }
}
