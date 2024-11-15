
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TodoList.Application;
using TodoList.Infrastrucutre;
using TodoList.Infrastrucutre.DataBaseContext;
using TodoList.Infrastrucutre.JwtService;

namespace TodoList.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{}
    }
});
            });
            builder.Services.AddScoped<JwtService>();
            builder.Services.AddDbContext<TaskDataBaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("connection"));
            });

            builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddRoles<Roles>()  //be able to add roles
            .AddRoleManager<RoleManager<Roles>>() //be able to make use of role manager
            .AddEntityFrameworkStores<TaskDataBaseContext>()  // be able to use entityframework and dbcontext
            .AddSignInManager<SignInManager<User>>() // to be able to sign in
            .AddUserManager<UserManager<User>>()  // be able use user manager
            .AddDefaultTokenProviders();  //be able to generate token for email confirms
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.RegisterApplicationService();
            builder.Services.Register(); //register infrastructure services
            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            //authenticate user using jwt by validate the authorization token in http header
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //allow to vqalidate signature key in jwtSetting:Secret
                    ValidateIssuerSigningKey = true,
                    //issuer signing key based on jwtSetting:Secret
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    //validate issuer (sho is ever issued to use jwt)
                    ValidateIssuer = true,
                    //the issuer is here the api project url are using
                    ValidIssuer = jwtSettings.Issuer!,
                    //validate audiance (angular side)
                    ValidateAudience = true,
                    //the audiance the recipient of token (clainet side)
                    ValidAudience = jwtSettings.Audience!,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors(options =>
            {
                options.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(builder.Configuration["JwtSettings:clientUrl"]!);
            });
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
