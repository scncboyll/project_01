using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TaskSystem
{
    public class Startup
    {
        // 1.添加路径
        private static string WebRootPath;
        public static string ApplicationBasePath;
        public void ConfigureServices(IServiceCollection services)
        {
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // 5.添加配置中间件
            app.UseStaticFiles();

            WebRootPath = env.WebRootPath;
            ApplicationBasePath = env.ContentRootPath;
            app.Use(async (context, next) =>
            {
                try
                {
                     await next.Invoke();
                }
                catch (System.Exception ex)
                {
                    context.Response.WriteAsync(ex.ToString());
                }
            });

            // app.Use((context, next) =>
            // {
            //     context.Request.EnableBuffering();
            //     return next();
            // });
            // app.UseRouting();

            app.Map("/Home", HandleMapHome);
            app.Map("/API", HandleMapAPI);
            app.Map("", ProcessRequest);
        }

        // 2.添加公用路径过滤
        private void ProcessRequest(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                string url = "https://" + context.Request.Host + "/Home";
                context.Response.StatusCode = 307;
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Location", url);
            });
        }

        // 3.添加API中间件
        private static void HandleMapAPI(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await BC_APICommand.ProcessAPIResult(context, app);
            });
        }

        // 4.添加Home中间件
        private static void HandleMapHome(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await BC_Home.ProcessRequest(context, app);
            });
        }
    }
}
