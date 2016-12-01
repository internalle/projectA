using ProjectA.Core.Infrastructure;
using ProjectA.Framework.Logging;
using ProjectA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Framework.Messaging.Decorators
{
    public class ExceptionLogger : IDispatcher
    {
        private IDispatcher _inner;
        private ILogger _logger;

        public ExceptionLogger(IDispatcher inner,
            ILogger logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public Result<T> Dispatch<T>(BaseRequest<T> request) where T : BaseResponse
        {
            var result = _inner.Dispatch(request);
            if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                _logger.Error(result.Exception);
            }
            return result;
        }
    }
}
