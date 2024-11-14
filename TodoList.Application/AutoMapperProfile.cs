using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Dtos.Cateogory;
using TodoList.Application.Dtos.Task;
using TodoList.Domain.Domains;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoList.Application
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
               CreateMap<TaskCateogory,AddCateogoryDto>().ReverseMap();
              CreateMap< AddTaskDto, TodoItems>().ForMember(dest => dest.DueDate, dest=> dest.Ignore()).ReverseMap();
            CreateMap<GetAllTasksDto,TodoItems>().ReverseMap();
            CreateMap<GetTaskByIdDto, TodoItems>().ReverseMap();
            CreateMap<GetAllCateogoriesDto, TaskCateogory>().ReverseMap();
        CreateMap<GetCateogoryByIdDto, TaskCateogory>().ReverseMap();   
        }
    }
}
