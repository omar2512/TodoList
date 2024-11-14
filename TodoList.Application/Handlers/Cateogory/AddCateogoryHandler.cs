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
    public class AddCateogoryHandler : IRequestHandler<AddCateogoryCommand, Result<bool>>
    {
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public AddCateogoryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
                _uniteOfWork = unitOfWork;  
                _mapper = mapper;
        }
        public async Task<Result<bool>> Handle(AddCateogoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region Mapping from dto to entity model
                var MappedCateogory =_mapper.Map<TaskCateogory>(request.AddCateogoryDto);
                MappedCateogory.Created=DateTime.Now;
                #endregion
                #region add to database
                var addResult = _uniteOfWork.Repository<TaskCateogory>().AddAsync(MappedCateogory).GetAwaiter();
                await _uniteOfWork.CompleteAsync();
                #endregion
                #region result
                if (addResult.IsCompleted)
                {
                    return Result<bool>.Success(true);
                }
                return Result<bool>.Failure("InCompleted Task");
                #endregion

            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
