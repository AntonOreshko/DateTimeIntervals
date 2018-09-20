using AutoMapper;
using DateTimeIntervalsServer.Data.DomainModels;
using DateTimeIntervalsServer.Data.Dtos;

namespace DateTimeIntervalsServer.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForLoginDto, User>();

            CreateMap<DateTimeIntervalForCreationDto, DateTimeInterval>();

            CreateMap<DateTimeIntervalForIntersectionDto, DateTimeInterval>();

            CreateMap<User, DateTimeIntervalForReturnDto>();
        }
    }
}
