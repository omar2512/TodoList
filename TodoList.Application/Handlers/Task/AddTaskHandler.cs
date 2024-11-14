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
    public class AddTaskHandler : IRequestHandler<AddTaskCommand, Result<bool>>
    {
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public AddTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uniteOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region map data
                var mappedRecord=_mapper.Map<TodoItems>(request.AddTaskDto);
                mappedRecord.Created=DateTime.Now;  
                #endregion
                #region add data to data base
                await _uniteOfWork.Repository<TodoItems>().AddAsync(mappedRecord);  
                await _uniteOfWork.CompleteAsync();
                #endregion
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
