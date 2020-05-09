using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Cw4.Middlerwares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            using (StreamWriter w = File.AppendText("requestsLog.txt"))
            {
                Log(httpContext, w);
            }
            await _next(httpContext);
        }
        public static void Log(HttpContext httpContext, TextWriter w)
        {
            w.Write("\r\nLog : ");
            w.Write(httpContext.Request.Method + " , ");
            w.Write(httpContext.Request.Path + " , ");
            w.Write(httpContext.Request.QueryString.Value);
        }
    }
}
