
using Core.Interfaces;
using Core.ReboInterfaces;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using SimpleInjector;


using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace DependencyInjection
{
    public static class Inject
    {
       
            public static void Injection(this ModelBuilder modelBuilder)
            {




            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped(typeof(IRepository<Object, Object>), typeof(Repository<Object, Object>));
            builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
            builder.Services.AddScoped(typeof(ISpecializationService), typeof(SpecializationService));
            builder.Services.AddScoped(typeof(IFileOperation), typeof(FileOperation));
            builder.Services.AddScoped(typeof(IPatientService), typeof(PatientService));
            builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
            builder.Services.AddScoped(typeof(IAppointmentRepository), typeof(AppointmentRepository));
            builder.Services.AddScoped(typeof(IDiscountRepository), typeof(DiscountRebository));
            builder.Services.AddScoped(typeof(IDiscountService), typeof(DiscountService));
            builder.Services.AddScoped(typeof(IRequestService), typeof(RequestService));
            builder.Services.AddScoped(typeof(IDashBoardRepository), typeof(DashBoardRepository));









            builder.Services.AddScoped(typeof(IDoctorService), typeof(DoctorService));
            builder.Services.AddScoped(typeof(IPatientRepository), typeof(PatientRepository));

            builder.Services.AddScoped(typeof(IDoctorRepository), typeof(DoctorRepository));

        }
}
