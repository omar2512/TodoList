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
    public class GetAllCateogiesQuery:IRequest<Result<List<GetAllCateogoriesDto>>>
    {

    }
}
