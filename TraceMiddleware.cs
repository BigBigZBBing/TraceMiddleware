using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
        public void Write(string message)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.txt");
            if (!File.Exists(path)) File.Create(path);
            using (var fs = File.Open(path, FileMode.Append))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(message);
                }
            }
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
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
            catch (System.Exception ex)
            {
                Write(ex.Message);
            }
        }
    }
}
