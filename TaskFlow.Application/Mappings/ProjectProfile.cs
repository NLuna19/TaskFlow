using AutoMapper;
using TaskFlow.Domain.Entities;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();
            CreateMap<Project, ProjectReadDto>();
        }
    }
}
