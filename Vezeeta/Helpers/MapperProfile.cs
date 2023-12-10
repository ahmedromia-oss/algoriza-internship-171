using AutoMapper;
using AutoMapper.Execution;
using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.Auth;
using Core.DTOs.bookers;
using Core.DTOs.Discount;
using Core.DTOs.Doctor;
using Core.DTOs.patient;
using Core.DTOs.Specialization;
using Core.DTOs.User;
using Core.Models;
using System.Security.Cryptography;

namespace Vezeeta.Helpers
{
    public class MapperProfile:Profile
    {
      

        public MapperProfile()
        {
            CreateMap<GetPatientDto, getPatientZipped>();
            CreateMap<PatientTime, getBookers>().ForMember(e=>e.status , opt => opt.MapFrom((s)=>((Enums.BookingStatus)s.status).ToString()));
            CreateMap<Time, getTimeForBookers>();
            CreateMap<Patient, getBookers>();
            CreateMap<addPatientDto, Patient>();
            CreateMap<Doctor , getDoctorZipped>();
            CreateMap<Day, getOnlyDayDTO>().ForMember(e=>e.day , opt=>opt.MapFrom((s)=> ((Enums.Days)s.Id).ToString()));
            CreateMap<Patient , GetPatientDto>();
            CreateMap<Patient, getPatientZipped>();
            CreateMap<addDiscountDto, Discount>();
            CreateMap<Discount , getDiscountDto>().ForMember(e=>e.Type , opt=>opt.MapFrom((s)=>((Enums.DiscountType)s.Type).ToString()))
                .ForMember(e => e.status, opt => opt.MapFrom((s) => ((Enums.DiscountStatus)s.status).ToString()));
            CreateMap<updateDiscountDto , Discount>();
            CreateMap<Time, getTimeDtoForUser>();
            CreateMap<signUpDto, User>().BeforeMap((s, d) => d.UserName = s.Email);
            CreateMap<Day , getDayDto>().ForMember(e=>e.day , opt => opt.MapFrom((s)=>((Enums.Days)s.Id).ToString()));
            CreateMap<Time , getTimeDto>();
            CreateMap<User, GetUserDto>().ForMember(e=>e.Gender , opt=>opt.MapFrom((s) =>((Enums.Gender)s.Gender).ToString()));
            CreateMap<Doctor, GetDoctorDto>();
            CreateMap<addDoctorDto, Doctor>();
            CreateMap<addDoctorDto, signUpDto>();
            CreateMap<Specialization, GetSpecializationDto>();
            CreateMap<updateDoctorDto, Doctor>().ForMember(dest => dest.SpecializationId, act => act.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, member, destmember) => (member != null)));
            CreateMap<updateUserDto, User>()
             .ForMember(dst => dst.DateOfBirth, opt =>opt.MapFrom((s , t)=>Convert.ToDateTime(s.DateOfBirth) != default(DateTime)?s.DateOfBirth:t.DateOfBirth))
                .ForMember(dst=>dst.Gender , opt=>opt.MapFrom((s,t)=>Convert.ToInt32(s.Gender)==0?Convert.ToInt32(t.Gender):Convert.ToInt32(s.Gender)))
                .ForAllMembers(opt => opt.Condition((src, dest, member, destmember) => (member != null)));
        }

}
}
