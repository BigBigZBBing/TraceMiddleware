using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TraceMiddleware
{
    public static class UseTrace
    {
        public static void UseTraceMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TraceMiddleware>();
        }
    }

    public class TraceMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var log = new TraceLog();
            log.Url = context.Request.Path;
            log.CreateTime = DateTime.Now.ToString();
            var time = Stopwatch.StartNew();
            try
            {
                await next?.Invoke(context);
            }
            catch (Exception ex)
            {
                log.ErrorMessage = ex.Message;
            }
            time.Stop();
            log.RunTime = time.ElapsedMilliseconds.ToString();
            TraceService.InsertTraceLog(log);
        }
    }
}
