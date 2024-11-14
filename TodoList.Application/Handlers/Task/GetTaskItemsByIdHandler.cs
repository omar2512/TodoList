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
    public class GetTaskItemsByIdHandler : IRequestHandler<GetTaskByIdQuery, Result<GetTaskByIdDto>>
    {
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public GetTaskItemsByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uniteOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<GetTaskByIdDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var TaskItemRecord= await _uniteOfWork.Repository<TodoItems>().GetByIdAsync(request.Id);
                var mappedRecord =_mapper.Map< GetTaskByIdDto>(TaskItemRecord); 
                if (TaskItemRecord == null)
                {
                    return Result<GetTaskByIdDto>.Failure("Data not found");
                }
                return Result<GetTaskByIdDto>.Success(mappedRecord);
            }
            catch (Exception ex)
            {
                return Result<GetTaskByIdDto>.Failure(ex.Message);
            }
        }
    }
}
