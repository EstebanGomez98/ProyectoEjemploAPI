using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProyectoEjemploAPI.Context;
using System;
using Microsoft.OpenApi.Models;
using ProyectoEjemploAPI.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace ProyectoEjemploAPI
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
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Conexion")));
            services.Configure<TokenManagement>(Configuration.GetSection("JsonWebTokenKeys"));
            var tokenConfigurations = Configuration.GetSection("JsonWebTokenKeys").Get<TokenManagement>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = tokenConfigurations.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenConfigurations.IssuerSigningKey)),
                    ValidateIssuer = tokenConfigurations.ValidateIssuer,
                    ValidIssuer = tokenConfigurations.ValidIssuer,
                    ValidateAudience = tokenConfigurations.ValidateAudience,
                    ValidAudience = tokenConfigurations.ValidAudience,
                    RequireExpirationTime = tokenConfigurations.RequireExpirationTime,
                    ValidateLifetime = tokenConfigurations.RequireExpirationTime,
                    ClockSkew = TimeSpan.Zero,
                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired-Time", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"ESTACION METEOROLOGICA {groupName}",
                    Version = groupName,
                    Description = "ESTACION METEOROLOGICA",
                    Contact = new OpenApiContact
                    {
                        Name = "ESTACION METEOROLOGICA",
                        Email = string.Empty,
                        Url = new Uri("https://ingenieriasoftware2.com/"),
                    }
                });
            });
            services.AddScoped<IAuthenticationService, TokenAuthenticationService>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()){app.UseDeveloperExceptionPage();}
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c=>{c.SwaggerEndpoint("/swagger/v1/swagger.json", "ESTACION METEOROLOGICA API V1");});
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());// global cors policy - .WithOrigins("")
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints=>{endpoints.MapControllers();});
        }
    }
}
