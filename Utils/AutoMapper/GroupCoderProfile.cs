using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualBasic;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Models;

namespace RiwiTalent.Utils.Mappings
{
    public class GroupCoderProfile : Profile
    {
        public GroupCoderProfile() {
           CreateMap<GroupCoderDto, GroupCoder>()
                                        .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); 
        } 
    }
}