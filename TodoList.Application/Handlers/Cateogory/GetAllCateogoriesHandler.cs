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
    public class GetAllCateogoriesHandler : IRequestHandler<GetAllCateogiesQuery, Result<List<GetAllCateogoriesDto>>>
    {
        private readonly IUnitOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public GetAllCateogoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uniteOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllCateogoriesDto>>> Handle(GetAllCateogiesQuery request, CancellationToken cancellationToken)
        {
            try
            {

                #region
                var cateogoryList =  _uniteOfWork.Repository<TaskCateogory>().GetAllAsync().ToList();
                if (cateogoryList.Count == 0 && cateogoryList == null) return Result<List<GetAllCateogoriesDto>>.Failure("no data found");
                var mappedResult=_mapper.Map<List<GetAllCateogoriesDto>>(cateogoryList);
                #endregion
                return Result<List<GetAllCateogoriesDto>>.Success(mappedResult);    

            }
            catch (Exception ex) { 
            return Result<List<GetAllCateogoriesDto>>.Failure(ex.Message);  
            }
        }
    }
}
