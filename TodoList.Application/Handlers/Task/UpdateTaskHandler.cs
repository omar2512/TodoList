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
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, Result<bool>>
    {
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public UpdateTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uniteOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region get task record by id
                var taskRecord = await _uniteOfWork.Repository<TodoItems>().GetByIdAsync(request.UpdateTaskDto.Id);
                if (taskRecord == null)
                {
                    return Result<bool>.Failure("record not found");
                }
                #endregion
                #region update
                taskRecord.Id = request.UpdateTaskDto.Id;   
                taskRecord.TaskDescription= request.UpdateTaskDto.TaskDescription;
                taskRecord.IsCompleted=request.UpdateTaskDto.IsCompleted;
                taskRecord.DueDate = new DateOnly(request.UpdateTaskDto.DueDate.Year, request.UpdateTaskDto.DueDate.Month, request.UpdateTaskDto.DueDate.Day);
                taskRecord.DueTime=request.UpdateTaskDto.DueTime;
                taskRecord.TaskTitle= request.UpdateTaskDto.TaskTitle;  
                taskRecord.Updated=DateTime.Now;
                _uniteOfWork.Repository<TodoItems>().Update(taskRecord);
                #endregion
                await _uniteOfWork.CompleteAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex) {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
