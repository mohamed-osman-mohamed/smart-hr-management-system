using AutoMapper;
using BLL_Solution.DataTransferObjects.EmployeeDTOs;
using DAL_Solution.Models.Employees;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Solution.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Create object-object mappings 
            CreateMap<EmployeeDto, Employee>()
                     .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
                     .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType));


            CreateMap<Employee, EmployeeDto>()
                      .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
                      .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
                      .ForMember(dest => dest.Address, option => option.MapFrom(src => src.Address ?? new Address()))
                      .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department == null ? "No Department" : src.Department.Name) );


            CreateMap<Employee, EmployeeDetailsDto>()
                     .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender.ToString()))
                     .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType.ToString()))
                     .ForMember(dest => dest.DepartmentName, Options => Options.MapFrom(src => src.Department == null ? "No Department" : src.Department.Name))
                     .ForMember(dest => dest.Image, options => options.MapFrom(src => src.ImageName));

            CreateMap<CreateEmployeeDto, Employee>()
                      .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly())))
                      .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address))
                      .ForMember(dest => dest.ImageName, options => options.MapFrom(src => src.Image));
           
            CreateMap<UpdateEmployeeDto, Employee>()
                      .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly())))
                      .ForMember(dest => dest.ImageName, option => option.MapFrom(src => src.Image));
            
            CreateMap<Address, AddressDto>().ReverseMap();


        }
    }
}
