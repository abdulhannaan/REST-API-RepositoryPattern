using AutoMapper;
using EMS.Model.DbModels.Employees;
using EMS.Model.Dtos;
using System.Data;

namespace EMS.Utlis.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
		{
			CreateMap<Employee, EmployeeDto>().ReverseMap();
			CreateMap<Employee, ShortListDto>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.LastName));
		}
	}
}
