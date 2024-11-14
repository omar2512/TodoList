using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Dtos.Task;
using TodoList.Domain.Commons;

namespace TodoList.Application.Commands.Task
{
    public class UpdateTaskCommand:IRequest<Result<bool>>
    {
        public UpdateTaskDto UpdateTaskDto { get; set; }
    }
}
