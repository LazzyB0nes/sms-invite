using System.Net.Http;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;

using AutoMapper;
using AutoMapper.Configuration;

using sms_invite.Mappers;
using sms_invite.Interfaces;
using sms_invite.Servcie;
using sms_invite.Http;
namespace sms_invite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "sms_invite", Version = "v1" });
            });

            _ = services.AddAutoMapper(typeof(Startup));
            _ = services.AddTransient<IInviteService, SmsInviteService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "sms_invite v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseExceptionHandler(c => c.Run(async context => {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;

                (var statusCode, string error) = MapExceptionToHttpStatus.Map(exception.GetType().Name);
                context.Response.StatusCode = (int)statusCode;
                
                await context.Response.WriteAsJsonAsync(new HttpErrorResponse() { Error = error, Message = exception.Message });
            }));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            MapperConfiguration config = new(cfg => cfg.AddProfile<InviteProfile>());
        }
    }
}
