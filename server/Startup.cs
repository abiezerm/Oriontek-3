using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using server.Data;
using server.GraphQl;

namespace server
{
  public class Startup
  {
      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      public void ConfigureServices(IServiceCollection services)
      {
        services.AddCors(options =>{
          options.AddPolicy(name: "ClientPermission", policy=>{
            policy 
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
          });
        });

        services.AddDbContext<AppDbContext>(opt => opt
          .UseNpgsql("Host=127.0.0.1;Database=OriontekTestDb;Username=postgres;Password=hola1234"));
        
        services
          .AddGraphQLServer()
          .AddQueryType<Query>()
          .AddMutationType<Mutation>();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
        if (env.IsDevelopment())
        {
          app.UseDeveloperExceptionPage();
        }
     

        app.UseRouting();
        app.UseCors("ClientPermission");

        app.UseEndpoints(endpoints =>
        {
          endpoints.MapGraphQL();
        });
      }
  }
}
