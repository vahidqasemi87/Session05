using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Part01.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part01
{
	public class Startup
	{
		public Startup()
		{

		}
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			//خطا دست کاربر نره
			app.Use(async (httpcontext, next) =>
			{
				try
				{
					await next();
				}
				catch (Exception ex)
				{
					Console.WriteLine("".PadRight(100, '-'));
					Console.WriteLine($"{ex.Message}\n");
					Console.WriteLine("".PadRight(100, '-'));
					await httpcontext.Response.WriteAsync("Error is running ...");
				}
			});
			app.Use(async (httpcontext, next) =>
			{
				if (httpcontext.Request.Query.ContainsKey("ex"))
				{
					throw new Exception("Bichare shodim  ... ");
				}
				await next();
			});
			//Middleware
			//app.Use(async (httpcontext, next) =>
			//{
			//	if (httpcontext.Request.Query.ContainsKey("a"))
			//	{
			//		await httpcontext.Response.WriteAsync("There is a key in quert string\n");
			//	}
			//	await next();
			//});

			app.Use(async (httpcontext, next) =>
			{
				System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
				stopwatch.Start();
				await next();
				stopwatch.Stop();
				Console.WriteLine("".PadRight(100, '-'));
				Console.WriteLine($"Total time : {stopwatch.ElapsedMilliseconds}\n");
				Console.WriteLine("".PadRight(100, '-'));
			});

			app.Use(async (httpcontext, next) =>
			{
				await httpcontext.Response.WriteAsync("First middleware before\n");
				await next();
				await httpcontext.Response.WriteAsync("First middleware after\n");
			});

			app.Use(async (httpcontext, next) =>
			{
				await httpcontext.Response.WriteAsync("second middleware \n");
				await next();
			});
			app.Use(async (httpcontext, next) =>
			{
				if (httpcontext.Request.Query.ContainsKey("x"))
					System.Threading.Thread.Sleep(2000);
				await next();
			});

			app.UseRouting();
			app.UseMiddleware<LoggerMidleware>();
			app.UseMiddleware<ShortCircuitMiddleware>();
			//
			//app.UseMiddleware<TerminatorMiddleware>();
			//convention for Terminator Middleware
			app.Run(async httpContext =>
			{
				await httpContext.Response.WriteAsync("This is Terminator");
			});
			//


			app.Map("/admin", c =>
			 {
				//c --> Application Builder
				c.Use(async (httpcontext, next) =>
				 {
					 await httpcontext.Response.WriteAsync("Admin\n");
				 });
				 c.Use(async (httpcontext, next) =>
				 {
					 await httpcontext.Response.WriteAsync("Admin 02\n");
				 });
			 });

			//MapWhen
			app.MapWhen(context =>true,app2=> 
			{

			});
			//app.UseWhen
			app.UseWhen(context=>true,app2=> 
			{

			});
			//
			app.Use(async (context, next) =>
			{
				await context.Response.WriteAsync("User\n");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello World!\n");
				});
			});
		}
	}
}
