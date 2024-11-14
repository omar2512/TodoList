using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Commands.Cateogory;
using TodoList.Domain.Commons;
using TodoList.Domain.Domains;

namespace TodoList.Application.Handlers.Cateogory
{
    public class DeleteCateogoryHandler : IRequestHandler<DeleteCateogoryCommand, Result<bool>>
    {
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public DeleteCateogoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uniteOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(DeleteCateogoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region get record
                var getTaskCateogoryById = await _uniteOfWork.Repository<TaskCateogory>().GetByIdAsync(request.Id);
                if (getTaskCateogoryById == null) return Result<bool>.Failure("Record NotFound");
                #endregion
                #region delete
                 _uniteOfWork.Repository<TaskCateogory>().Remove(getTaskCateogoryById);
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
