using AutoMapper;
using company.DTO.ClientDtos;
using company.DTO.DepartmentDtos;
using company.DTO.EmployeeDtos;
using DataAccessLayer.Entities;

namespace company
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            CreateMap<Department, DepartmentReadDto>().ReverseMap();
            CreateMap<DepartmentCreateDto, Department>().ReverseMap();
            CreateMap<DepartmentUpdateDto, Department>().ReverseMap();
            CreateMap<DepartmentEmployeeDto, Employee>().ReverseMap();
            CreateMap<EmployeeDepartmentDto , Department>().ReverseMap();

            CreateMap<Employee, EmployeeReadDto>().ReverseMap();
                //.ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<EmployeeCreateDto, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();
            CreateMap<ClientEmployeeDto , Employee>().ReverseMap();
            


            CreateMap<ClientCreateDto, Client>().ReverseMap();
            CreateMap<ClientUpdateDto, Client>().ReverseMap();
            CreateMap<ClientReadDto, Client>().ReverseMap();
            CreateMap<EmployeeClientDto, Client>().ReverseMap();

        }
    }
}
