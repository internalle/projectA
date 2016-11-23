using Autofac;
using ProjectA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace ProjectA.Framework.Messaging
{
    public class Dispatcher : IDispatcher
    {
        private static Type[] _handlerTypes { get; } = typeof(BaseResponse).Assembly.GetHandlerTypes();
        private static Type[] _validatorTypes { get; } = typeof(BaseResponse).Assembly.GetValidatorTypes();
        private static Type[] _requestTypes { get; } = typeof(BaseResponse).Assembly.GetRequestTypes();

        private IComponentContext _container;

        public Dispatcher(IComponentContext container)
        {
            _container = container;
        }

        public Result<T> Dispatch<T>(BaseRequest<T> request) where T : BaseResponse
        {
            var responseType = typeof(T);
            var requestType = request.GetType();
            var handlerType = GetTypeInHaystack(_handlerTypes, responseType, requestType);
            var validatorType = GetTypeInHaystack(_validatorTypes, responseType, requestType);
            
            var result = new Result<T>();
            try
            {
                var handler = _container.Resolve(handlerType.BaseType);
                Validate(validatorType, request);
                                
                result.Response = Execute(handler, request);
                result.Exception = null;
                result.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                result.Response = null;
                result.Exception = ex;
                result.StatusCode = System.Net.HttpStatusCode.Conflict;
            }
            return result;
        }

        public void Validate<T>(Type validatorType, BaseRequest<T> request) where T : BaseResponse
        {
            if (validatorType != null)
            {
                var validator = _container.Resolve(validatorType.BaseType);
                var results = validator?.GetType().GetMethod("Validate", new Type[] { request.GetType() }).Invoke(validator, new object[] { request }) as ValidationResult;
                if (!results.IsValid)
                {
                    throw new AggregateException(results.Errors.Select(x =>
                    {
                        var exception = new Exception(x.ErrorMessage);
                        exception.Source = "FluentValidator";
                        exception.Data["Property"] = x.PropertyName;
                        return exception;
                    }));
                }
            }
        }

        public T Execute<T>(object handler, BaseRequest<T> request) where T : BaseResponse
        {
            return handler.GetType().GetMethod("Handle").Invoke(handler, new object[] { request }) as T;
        }

        private Type GetTypeInHaystack(Type[] searchTypes, Type responseType, Type requestType)
        {
            return searchTypes
                .Where(x => x.BaseType.GetGenericArguments().Any(y => y == responseType || y == requestType))
                .SingleOrDefault();
        }
    }
}
