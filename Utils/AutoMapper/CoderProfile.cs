using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.Utils.AutoMapper
{
    public class CoderProfile : Profile
    {
        public CoderProfile ()
        {
            CreateMap<CoderDto, Coder>()
                                        .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}