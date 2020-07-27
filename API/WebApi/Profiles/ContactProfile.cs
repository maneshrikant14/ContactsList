using AutoMapper;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDto>()
                    .ForMember(
                        dest => dest.ContactName,
                        opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}" ))
                    .ForMember(
                        dest => dest.Status,
                        opt => opt.MapFrom(src => src.Status.GetStatus()))                        
                    .ForMember(
                        dest=> dest.PhoneNo,
                        opt=> opt.MapFrom(src=> src.PhoneNumber));


            CreateMap<ContactForSaveDto,Contact>();

            CreateMap<ContactForUpdateDto,Contact>();
        }
    }
}
