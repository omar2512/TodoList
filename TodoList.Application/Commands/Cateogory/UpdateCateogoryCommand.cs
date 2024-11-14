using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Dtos.Cateogory;
using TodoList.Domain.Commons;

namespace TodoList.Application.Commands.Cateogory
{
    public class UpdateCateogoryCommand:IRequest<Result<bool>>
    {
        public UpdateCateogoryDto UpdateCateogoryDto { get; set; }
    }
}
