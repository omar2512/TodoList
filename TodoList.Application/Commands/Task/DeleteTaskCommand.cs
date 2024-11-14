using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Commons;

namespace TodoList.Application.Commands.Task
{
    public class DeleteTaskCommand:IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }
}
