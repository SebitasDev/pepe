using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.Utils.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Coder, CoderDto>();
        }
    }
}