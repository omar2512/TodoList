using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Dtos.Task;
using TodoList.Domain.Commons;

namespace TodoList.Application.Queries.Task
{
    public class GetTaskByIdQuery:IRequest<Result<GetTaskByIdDto>>
    {
        public int Id { get; set; }
    }
}
