using AutoMapper;
using TaskFlow.Domain.Entities;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // CreateDto → Entity
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            // hash en el servicio

            // UpdateDto → Entity
            CreateMap<UserUpdateDto, User>();

            // Entity → ReadDto
            CreateMap<User, UserReadDto>();
        }
    }
}
