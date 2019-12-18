using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.ApplicationServices;
using BilProjekt3Semester.Core.ApplicationServices.Services;
using BilProjekt3Semester.Core.DomainServices;
using BilProjekt3Semester.Infrastructure.SQL;
using BilProjekt3Semester.Infrastructure.SQL.Helper;
using BilProjekt3Semester.Infrastructure.SQL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BilProjekt3Semester.RestApi
{
    public class Startup
    {
        private Timer timer;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            services.AddSingleton<IAuthHelper>(new AuthHelper(secretBytes));
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarService, CarService>();
            services.AddTransient<IDbInitializer, DbInitializer>();

            services.AddCors(options => options.AddPolicy("AllowSpecificOrigin",
                builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            // Add JWT based authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidAudience = "",
                    ValidateIssuer = false,
                    //ValidIssuer = "",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(1) //5 minute tolerance for the expiration date
                };
            });


            if (Environment.IsDevelopment())
            {
                services.AddDbContext<CarShopContext>(opt => opt.UseSqlite("Data source=CarShop.db"));
            }
            else
            {
                services.AddDbContext<CarShopContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }

            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.MaxDepth = 3;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<CarShopContext>();
                    var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();

                    // It is done here instead of in the initializer so that it does not interfere with the production environment
                    context.Database.EnsureDeleted();
                    dbInitializer.SeedDb(context);

                    InitDeleteCarThread(app);
                }

                app.UseDeveloperExceptionPage();
            }
            else
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<CarShopContext>();
                    var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                    //dbInitializer.SeedDb(context);  //only run once 
                    //context.Database.EnsureCreated();   //needs to be run once for entity core to create the database tables

                    InitDeleteCarThread(app); // this is used for checking and deleting old cars.
                }

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");
            app.UseMvc();
        }

        // Uses a thread to continuously run the CheckAndDeleteOldCars method
        // based on the startTimeSpan and periodTimeSpan variables
        public void InitDeleteCarThread(IApplicationBuilder app)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromDays(1);

            timer = new Timer((e) =>
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var carService = scope.ServiceProvider.GetService<ICarService>();
                    carService.CheckAndDeleteOldCars();
                }
            }, null, startTimeSpan, periodTimeSpan);
        }
    }
}