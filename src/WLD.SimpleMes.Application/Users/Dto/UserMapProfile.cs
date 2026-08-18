using AutoMapper;
using System.Linq;
using WLD.SimpleMes.Authorization.Users;

namespace WLD.SimpleMes.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.WorkStationUserRelations, opt => opt.Ignore());

            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

            CreateMap<User,UserDto>()
               .ForMember(x => x.WorkStationUserRelationIds, opt => opt.Ignore());
        }
    }
}

