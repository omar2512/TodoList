using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Dtos.Task;
using TodoList.Application.Queries.Task;
using TodoList.Domain.Commons;
using TodoList.Domain.Domains;

namespace TodoList.Application.Handlers.Task
{
    public class GetAllTaskItemsHandler:IRequestHandler<GetAllTaskQuery,Result<List<GetAllTasksDto>>>
    {
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public GetAllTaskItemsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uniteOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllTasksDto>>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var AllTaskItems = _uniteOfWork.Repository<TodoItems>().GetAllAsync().ToList();
                var mappedTasks =_mapper.Map<List<GetAllTasksDto>>(AllTaskItems);
                if(AllTaskItems != null)
                {
                    return Result<List<GetAllTasksDto>>.Success(mappedTasks);
                }
                return Result<List<GetAllTasksDto>>.Failure("no data found");
            }
            catch (Exception ex)
            {
                return Result<List<GetAllTasksDto>>.Failure(ex.Message);
            }
        }
    }
}
