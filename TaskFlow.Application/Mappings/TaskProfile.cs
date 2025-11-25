using AutoMapper;
using TaskFlow.Domain.Entities;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskCreateDto, TaskItem>();
            CreateMap<TaskUpdateDto, TaskItem>();
            CreateMap<TaskItem, TaskReadDto>();
        }
    }
}
