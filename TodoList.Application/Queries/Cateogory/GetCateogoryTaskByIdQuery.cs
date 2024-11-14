using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Dtos.Cateogory;
using TodoList.Domain.Commons;

namespace TodoList.Application.Queries.Cateogory
{
    public class GetCateogoryTaskByIdQuery:IRequest<Result<GetCateogoryByIdDto>>
    {
        public int Id { get; set; }
    }
}
