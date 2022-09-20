using FluentValidation;
using FluentValidation.AspNetCore;
using LoginUser.WebApi.Authentication;
using LoginUser.WebApi.Authorization;
using LoginUser.WebApi.Context;
using LoginUser.WebApi.Entities;
using LoginUser.WebApi.InterFaces;
using LoginUser.WebApi.Middleware;
using LoginUser.WebApi.Models;
using LoginUser.WebApi.Services;
using LoginUser.WebApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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

            var authenticationSettings = new AuthenticationSettings();//https://youtu.be/exKLvxaPI6Y?t=3094

            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);

            //https://youtu.be/exKLvxaPI6Y?t=3216
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });
            //wlasna polityka autoryzacji https://youtu.be/Ei7Uk-UgSAY?t=1114
            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality", "polish"));
                options.AddPolicy("Atleast20", builder => builder.AddRequirements(new MinimumAgeRequirement(20)));//https://youtu.be/Ei7Uk-UgSAY?t=1557
            });

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();//aby zadzialala wlasna policy "Atleast20"
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();//aby zadzialalo np. kasowanie zasobow przez uzytkownika ktory je stworzyl

            services.AddControllers().AddFluentValidation();// aby zadzialal IValidator trzeba to dodac! paczka FluentValidation.AspNetCore

            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<UserSeeder>();
            services.AddScoped<ClientSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClientService, ClientService>();
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
            UserSeeder userSeeder,ClientSeeder cliebtSeeder,
            ApplicationDbContext context) //TODO wstzykujemy seedera
        {
            context?.Database.Migrate();
            userSeeder.Seed();
            cliebtSeeder.Seed();
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoginUser.WebApi v1"));//TODO nie odplaca stronki z /swagger!!!
                //app.UseSwaggerUI(c => c.RoutePrefix = string.Empty);       
            }

            app.UseAuthentication();

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
