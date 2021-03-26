using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part01.Middlewares
{
	public class ShortCircuitMiddleware
	{
		private readonly RequestDelegate _next;
		public ShortCircuitMiddleware(RequestDelegate next)
		{
			_next = next;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			if (httpContext.Request.Query.ContainsKey("w") && httpContext.Request.Query["w"] == 1)
			{
				await _next(httpContext);
			}
			else
			{
				await httpContext.Response.WriteAsync("You can not use this application . \n");
			}
		}
	}
}
