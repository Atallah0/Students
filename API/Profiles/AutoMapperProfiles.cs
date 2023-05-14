using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DomainModels;
using API.Profiles.AfterMap;
using AutoMapper;
using Core.Models;

namespace API.Profiles
{
    public class AutoMapperProfiles : Profile
    {

       public AutoMapperProfiles()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<UpdateStudentRequestDto, Student>()
                .AfterMap<UpdateStudentRequestAfterMap>();
            CreateMap<AddStudentRequestDto, Student>()
                .AfterMap<AddStudentRequestAfterMap>();
        }
    }
}