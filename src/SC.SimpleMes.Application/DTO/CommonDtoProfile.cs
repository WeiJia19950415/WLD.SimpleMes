using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization.Users;

namespace SC.SimpleMes.DTO
{
    public class CommonDtoProfile : Profile
    {
        public CommonDtoProfile()
        {
            this.CreateMap<User, TransferItemDto>()
                .ForMember(p => p.Key, d => d.MapFrom(opt => opt.Id))
                .ForMember(p => p.label, d => d.MapFrom(opt => string.IsNullOrEmpty(opt.Name) ? opt.UserName : opt.Name));
        }
    }
}
