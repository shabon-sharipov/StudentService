using Application.Requests;
using Application.Responses;
using AutoMapper;

namespace Application.Mappers;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<StudentRequestModel, Student>()
            .ForMember(s => s.GuidId, o => o.MapFrom(sr => Guid.NewGuid().ToString()));
        CreateMap<Student, StudentResponseModel>();
    }
}