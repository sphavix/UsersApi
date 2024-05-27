using AccountsApi.Entities;
using AccountsApi.Models.User;
using AutoMapper;

namespace AccountsApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateUser -> User
            CreateMap<CreateUserRequest, User>().ReverseMap();

            //UpdateUser -> User
            CreateMap<UpdateUserRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        //ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        //ignore null role
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;
                    }));
        }
    }
}