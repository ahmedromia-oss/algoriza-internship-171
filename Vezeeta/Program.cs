using Core.Domain;
using Core.Interfaces;
using Core.ReboInterfaces;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Service;
using Swashbuckle.Swagger;
using System;
using System.Reflection;
using System.Text;
using Vezeeta.Filters;
using Vezeeta.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:secretToken"]))
    };
});
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddTransient<IUnitOfWork , UnitOfWork>();

builder.Services.AddScoped(typeof(IRepository<Object , Object>), typeof(Repository<Object, Object>));
builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
builder.Services.AddScoped(typeof(ISpecializationService), typeof(SpecializationService));
builder.Services.AddScoped(typeof(IFileOperation), typeof(FileOperation));
builder.Services.AddScoped(typeof(IPatientService), typeof(PatientService));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
builder.Services.AddScoped(typeof(IAppointmentRepository), typeof(AppointmentRepository));






builder.Services.AddScoped(typeof(IDoctorService), typeof(DoctorService));
builder.Services.AddScoped(typeof(IPatientService), typeof(PatientService));

builder.Services.AddScoped(typeof(IDoctorRepository), typeof(DoctorRepository));




builder.Services.AddControllers(configure: MvcOptions => MvcOptions.Filters.Add(typeof(ModelValidationFilter))).AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
   
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
   
});
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
