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
             
        }
    }
}
