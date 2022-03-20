using LibraryApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi
{
    public class Startup
    {
        public readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        //*** :This method is used for adding dependencies to IoC container.
        public void ConfigureServices(IServiceCollection services)
        {
            //*** :extenstion method added.
            services.ConfigureIoC(_config);
        }

        //*** :This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //*** :extenstion method added.
            app.ConfigureMiddleWare(env);
        }
    }
}
