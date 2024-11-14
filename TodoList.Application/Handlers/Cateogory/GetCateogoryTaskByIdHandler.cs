using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.Dtos.Cateogory;
using TodoList.Application.Queries.Cateogory;
using TodoList.Domain.Commons;
using TodoList.Domain.Domains;

namespace TodoList.Application.Handlers.Cateogory
{
    public class GetCateogoryTaskByIdHandler : IRequestHandler<GetCateogoryTaskByIdQuery, Result<GetCateogoryByIdDto>>
    {
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public GetCateogoryTaskByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uniteOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<GetCateogoryByIdDto>> Handle(GetCateogoryTaskByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                #region
                var cateogoryRecord =await _uniteOfWork.Repository<TaskCateogory>().GetByIdAsync(request.Id);
                var mappedRecord=_mapper.Map<GetCateogoryByIdDto>(cateogoryRecord);
                #endregion
                return Result<GetCateogoryByIdDto>.Success(mappedRecord);

            }
            catch(Exception ex)
            {
                return Result<GetCateogoryByIdDto>.Failure(ex.Message);
            }
        }
    }
}
