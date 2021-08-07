using App.Common.DTO;
using App.Infrastructure.Database.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Config.Dependencies
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UserDTO>().ReverseMap();
        }
    }
}
