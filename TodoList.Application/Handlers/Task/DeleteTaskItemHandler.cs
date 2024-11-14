using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Commands.Task;
using TodoList.Domain.Commons;
using TodoList.Domain.Domains;

namespace TodoList.Application.Handlers.Task
{
    public class DeleteTaskItemHandler : IRequestHandler<DeleteTaskCommand, Result<bool>>
    {
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public DeleteTaskItemHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uniteOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var TaskRecordToDelete=await _uniteOfWork.Repository<TodoItems>().GetByIdAsync(request.Id);
                if (TaskRecordToDelete != null) { return Result<bool>.Failure("record not found"); }
                TaskRecordToDelete.IsDeleted= true;
                 await _uniteOfWork.CompleteAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
