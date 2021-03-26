using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part01.Middlewares
{
	public class TerminatorMiddleware
	{
		private readonly Microsoft.AspNetCore.Http.RequestDelegate _next;
		public TerminatorMiddleware(Microsoft.AspNetCore.Http.RequestDelegate next)
		{
			_next = next;
		}
		//Not Called _next
		public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext httpContext)
		{
			await httpContext.Response.WriteAsync("This is Terminator");
		}
	}
}
