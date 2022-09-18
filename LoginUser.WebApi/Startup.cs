using FluentValidation;
using FluentValidation.AspNetCore;
using LoginUser.WebApi.Authentication;
using LoginUser.WebApi.Context;
using LoginUser.WebApi.Entities;
using LoginUser.WebApi.InterFaces;
using LoginUser.WebApi.Middleware;
using LoginUser.WebApi.Models;
using LoginUser.WebApi.Services;
using LoginUser.WebApi.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LoginUser.WebApi
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

            var authenticationSettings = new AuthenticationSettings();

            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);

            //services.AddAuthentication(option =>
            //{
            //    option.DefaultAuthenticateScheme = "Bearer";
            //    option.DefaultScheme = "Bearer";
            //    option.DefaultChallengeScheme = "Bearer";
            //}).AddJwtBearer(cfg =>
            //{
            //    cfg.RequireHttpsMetadata = false;
            //    cfg.SaveToken = true;
            //    cfg.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidIssuer = authenticationSettings.JwtIssuer,
            //        ValidAudience = authenticationSettings.JwtIssuer,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
            //    };
            //});

            services.AddControllers().AddFluentValidation();// aby zadzialal IValidator trzeba to dodac! paczka FluentValidation.AspNetCore

            services.AddDbContext<ApplicationDbContext>();//
            services.AddScoped<UserSeeder>();//
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<IUserService, UserService>();//
            services.AddScoped<IAccountService, AccountService>();//
            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();//uzyjemy tego do hashowania hasel
            services.AddScoped<IValidator<RegisterUserDto>, ValidationRegisterUserDto>();//TODO validator https://youtu.be/exKLvxaPI6Y?t=2512 , https://youtu.be/exKLvxaPI6Y?t=2571

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoginUser.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserSeeder seeder,
            ApplicationDbContext context) //TODO wstzykujemy seedera
        {
            context?.Database.Migrate();
            seeder.Seed();//

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoginUser.WebApi v1"));//TODO nie odplaca stronki z /swagger!!!
                //app.UseSwaggerUI(c => c.RoutePrefix = string.Empty);       
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
