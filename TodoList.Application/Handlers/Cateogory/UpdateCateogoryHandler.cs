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
    public class UpdateCateogoryHandler:IRequestHandler<UpdateCateogoryCommand,Result<bool>>
    {
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public UpdateCateogoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uniteOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateCateogoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region mapp
                var getTaskCateogoryById =await _uniteOfWork.Repository<TaskCateogory>().GetByIdAsync(request.UpdateCateogoryDto.Id);
                if (getTaskCateogoryById == null) return Result<bool>.Failure("Record NotFound");
                getTaskCateogoryById.Name=request.UpdateCateogoryDto.Name;
                getTaskCateogoryById.Updated=DateTime.Now;
                #endregion
                #region update
                 _uniteOfWork.Repository<TaskCateogory>().Update(getTaskCateogoryById);
                 await _uniteOfWork.CompleteAsync();

                #endregion
                #region result
                return Result<bool>.Success(true);
                #endregion
            }
            catch (Exception ex) {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
