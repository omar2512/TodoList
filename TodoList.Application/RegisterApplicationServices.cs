using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Commands.Cateogory;
using TodoList.Application.Commands.Task;
using TodoList.Application.Handlers.Cateogory;
using TodoList.Application.Queries.Cateogory;
using TodoList.Application.Queries.Task;
using TodoList.Domain.Commons;

namespace TodoList.Application
{
    public static class RegisterApplicationServices
    {
        public static void RegisterApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddMediatR(cfg =>
            {

                cfg.RegisterServicesFromAssembly(typeof(AddCateogoryCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(AddTaskCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateCateogoryCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateTaskCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteCateogoryCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(DeleteTaskCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllTaskQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllCateogiesQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetCateogoryTaskByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetTaskByIdQuery).Assembly);
            });
        }
    }
}
