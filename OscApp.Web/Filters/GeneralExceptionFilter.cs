using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using OscApp.Exceptions;

namespace OscApp.Web.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        private Dictionary<Type, HttpStatusCode> _exceptionStatusCodeMapping;

        public GeneralExceptionFilter()
        {
            _exceptionStatusCodeMapping = new Dictionary<Type, HttpStatusCode>
            {
                {typeof (NotFoundException), HttpStatusCode.NotFound}
            };
        }

        public override void OnException(ExceptionContext context)
        {
            //notimplemented --> Method Not Allowed
            if (context.Exception.GetType() == typeof(NotImplementedException))
            {
                context.Result = new ContentResult() { Content = "Method not supported", StatusCode = 405 };
            }

            //if the exception isn't mapped, catch exception & fire back 500 ISE with generic error message
            //assumption: if this filter is invoked, context.Exception != null
            if (!_exceptionStatusCodeMapping.ContainsKey(context.Exception.GetType()))
            {
                context.Result = new ContentResult() { Content = context.Exception.Message, StatusCode = 500 };
                return;
            }

            //exception is mapped, set status code & message on response
            context.Result= new ContentResult() { Content = context.Exception.Message, StatusCode = (int)_exceptionStatusCodeMapping[context.Exception.GetType()]};
        }

        public override Task OnExceptionAsync(ExceptionContext actionExecutedContext)
        {
            OnException(actionExecutedContext);
            return base.OnExceptionAsync(actionExecutedContext);
        }
    }
}