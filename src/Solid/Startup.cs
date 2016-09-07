using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Solid.Core;

namespace Solid
{
    public class Startup
    {
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

		public Startup(IHostingEnvironment env)
		{
			ConfigurationBuilder builder = new ConfigurationBuilder();
			builder.SetBasePath(env.ContentRootPath);
			builder.AddJsonFile("appsettings.json");
			var config = builder.Build();
			AppSettings.ConnectionString = config["Data:DefaultConnection:ConnectionString"];
			AppSettings.Title = config["AppSettings:Title"];
		}
		public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc();
			services.AddEntityFrameworkSqlServer();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			app.UseStaticFiles();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
    }
}
