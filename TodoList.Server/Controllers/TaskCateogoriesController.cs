using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Commands.Cateogory;
using TodoList.Application.Commands.Task;
using TodoList.Application.Dtos.Cateogory;
using TodoList.Application.Dtos.Task;
using TodoList.Application.Queries.Cateogory;
using TodoList.Application.Queries.Task;
using TodoList.Domain.Commons;

namespace TodoList.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCateogoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TaskCateogoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddCateogory")]

        public async Task<Result<bool>> AddCateogory(AddCateogoryDto request)
        {
            try
            {
                var AddCateogory = await _mediator.Send(new AddCateogoryCommand() { AddCateogoryDto = request });
                if (AddCateogory == null || !AddCateogory.IsSuccess) { return Result<bool>.Failure("fail to add record"); }
                return Result<bool>.Success(true);

            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        [HttpPost("UpdateTaskCateogogry")]

        public async Task<Result<bool>> UpdateTaskCateogogry(UpdateCateogoryDto request)
        {
            try
            {
                var UpdateTaskCateogogry = await _mediator.Send(new UpdateCateogoryCommand() { UpdateCateogoryDto = request });
                if (UpdateTaskCateogogry == null || !UpdateTaskCateogogry.IsSuccess) { return Result<bool>.Failure("fail to update record"); }
                return Result<bool>.Success(true);

            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }


        [HttpPost("DeleteTaskCateogory")]

        public async Task<Result<bool>> DeleteTaskCateogory(int Id)
        {
            try
            {
                var DeleteTaskCateogory = await _mediator.Send(new DeleteCateogoryCommand() { Id = Id });
                if (DeleteTaskCateogory == null || !DeleteTaskCateogory.IsSuccess) { return Result<bool>.Failure("fail to delete record"); }
                return Result<bool>.Success(true);

            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        [HttpGet("GetTaskCateogogryByTask")]

        public async Task<Result<GetCateogoryByIdDto>> GetTaskCateogogryByTask(int Id)
        {
            try
            {
                var GetTaskCateogogryByTask = await _mediator.Send(new GetCateogoryTaskByIdQuery() { Id = Id });
                if (GetTaskCateogogryByTask == null || !GetTaskCateogogryByTask.IsSuccess) { return Result<GetCateogoryByIdDto>.Failure("record not found"); }
                return Result<GetCateogoryByIdDto>.Success(GetTaskCateogogryByTask.Data);

            }
            catch (Exception ex)
            {
                return Result<GetCateogoryByIdDto>.Failure(ex.Message);
            }
        }
        [HttpGet("GetAllTaskCateogories")]

        public async Task<Result<List<GetAllCateogoriesDto>>> GetAllTaskCateogories()
        {
            try
            {
                var GetAllTaskCateogoryList = await _mediator.Send(new GetAllCateogiesQuery() { });
                if (GetAllTaskCateogoryList == null || !GetAllTaskCateogoryList.IsSuccess) { return Result<List<GetAllCateogoriesDto>>.Failure("fail to delete record"); }
                return Result<List<GetAllCateogoriesDto>>.Success(GetAllTaskCateogoryList.Data);

            }
            catch (Exception ex)
            {
                return Result<List<GetAllCateogoriesDto>>.Failure(ex.Message);
            }
        }
    }
}
