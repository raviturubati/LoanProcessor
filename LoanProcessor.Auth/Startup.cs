using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using LoanProcessor.Auth.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using LoanProcessor.Auth.Repositories;
using Microsoft.OpenApi.Models;

namespace LoanProcessor.Auth
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

            //Authentication Token
            var secretKay = Configuration.GetSection("JwtSettings").GetSection("Secret").Value;
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
                ).AddJwtBearer(b =>
                {
                    b.RequireHttpsMetadata = false;
                    b.SaveToken = true;
                    b.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(secretKay)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }

                 );
            services.AddSingleton<IUserRepository>(new UserRepository(secretKay));

            //services.AddDbContext<LoanProcessorAuthContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("LoanProcessorAuthContext")));

            //services.AddSwaggerDocumentation();
            services.AddSwaggerGen(s =>
            s.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication API", Version = "v1" })
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseSwaggerDocumentation();
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("v1/swagger.json", "Authentication  V1");
            });
        }
    }
}
