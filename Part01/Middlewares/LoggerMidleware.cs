using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part01.Middlewares
{
	public class LoggerMidleware //: Microsoft.AspNetCore.Http.IMiddleware
	{
		private readonly RequestDelegate _next;
		public LoggerMidleware(RequestDelegate loggerMidleware)
		{
			_next = loggerMidleware;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();
			await _next(context);
			stopwatch.Stop();
			Console.WriteLine("".PadRight(100, '-'));
			Console.WriteLine($"Total time is : {stopwatch.ElapsedMilliseconds}");
			Console.WriteLine("".PadRight(100, '-'));
		}
	}
}
