using AutoMapper;
using DateTimeIntervals.DomainLayer.DomainModels;
using DateTimeIntervals.Dtos.Dtos;

namespace DateTimeIntervals.Api.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForLoginDto, User>();

            CreateMap<DateTimeIntervalForCreationDto, DateTimeInterval>();

            CreateMap<DateTimeIntervalForIntersectionDto, DateTimeInterval>();

            CreateMap<User, UserForReturnDto>();
        }
    }
}
