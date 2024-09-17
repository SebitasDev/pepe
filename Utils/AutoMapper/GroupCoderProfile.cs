using AutoMapper;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Models;

namespace RiwiTalent.Utils.AutoMapper
{
    public class GroupCoderProfile : Profile
    {
        public GroupCoderProfile() {
           CreateMap<GroupCoderDto, GruopCoder>()
                                        .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); 
        } 
    }
}