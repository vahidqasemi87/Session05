using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part01
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = CreateHostBuilder02(args);
			var host = builder.Build();
			host.Run();

			//CreateHostBuilder(args).Build().Run();
		}

		//public static IHostBuilder CreateHostBuilder(string[] args) =>
		//	Host.CreateDefaultBuilder(args)
		//		.ConfigureWebHostDefaults(webBuilder =>
		//		{
		//			webBuilder.UseStartup<Startup>();
		//		});
		public static IHostBuilder CreateHostBuilder02(string[] arg)
		{
			var host = Host.CreateDefaultBuilder(arg);
			var configure = host.ConfigureWebHostDefaults(c=>c.UseStartup<Startup>());
			return configure;
		}
	}
}
