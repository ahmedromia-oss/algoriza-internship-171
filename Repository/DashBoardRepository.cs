using AutoMapper;
using Core.DTOs.Doctor;
using Core.DTOs.Requests;
using Core.DTOs.Specialization;
using Core.Models;
using Core.ReboInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public DashBoardRepository(AppDbContext appDbContext , IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

       
        

        public async Task<getNumberOfRequests> numberOfRequests()
        {
            return await this.appDbContext.PatientTime.GroupBy(x => 1).Select(e => new getNumberOfRequests {
                CancelledRequests = e.Count(e => e.status == Convert.ToInt32(Enums.BookingStatus.cancelled)),
                CompletedRequest = e.Count(e => e.status == Convert.ToInt32(Enums.BookingStatus.completed)),
                PendingRequest = e.Count(e => e.status == Convert.ToInt32(Enums.BookingStatus.pending))


            }).FirstOrDefaultAsync();
        }

        public async Task<Object> Top10Doctors()
        {
            return (await this.appDbContext.PatientTime
                .GroupBy(x => x.time.Doctor)
                .Select(e =>
                new
                {
                    doctor = mapper.Map<ICollection<getDoctorZipped>>(e.Select(e => e.time.Doctor)).FirstOrDefault(),
                    completedRequests = e.Count(d => d.status == Convert.ToInt32(Enums.BookingStatus.completed))
                })
                .OrderByDescending(d => d.completedRequests)
                .Take(10)
                .ToListAsync());
        }

            public async Task<Object> Top5Specialization()
        {
            return (await this.appDbContext.PatientTime
               .GroupBy(x => x.time.Doctor.specialization)
               .Select(e =>
               new { Specialization = mapper.Map<ICollection<GetSpecializationDto>>( e.Select(e => e.time.Doctor.specialization)).FirstOrDefault(),
               completedRequests = e.Count(d => d.status == Convert.ToInt32(Enums.BookingStatus.completed)) })
               .OrderByDescending(d=>d.completedRequests)
               .Take(5)
               .ToListAsync());
        }
    }
}
