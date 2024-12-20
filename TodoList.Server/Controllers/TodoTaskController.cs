﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class TodoTaskController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TodoTaskController> _logger;
        public TodoTaskController(IMediator mediator, ILogger<TodoTaskController> logger)
        {
                _mediator = mediator;   
                _logger = logger;  
        }
        [HttpPost("AddTask")]

        public async Task<Result<bool>> AddTask(AddTaskDto request)
        {
            try
            {
                _logger.LogInformation("AddTask");
                var AddTask=await _mediator.Send(new AddTaskCommand() { AddTaskDto=request}); 
                if (AddTask == null || !AddTask.IsSuccess ) { return Result<bool>.Failure("fail to add record"); }
                return Result<bool>.Success(true);

            }catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        [HttpPost("UpdateTask")]

        public async Task<Result<bool>> UpdateTask(UpdateTaskDto request)
        {
            try
            {
                _logger.LogInformation("UpdateTask");
                var UpdateTask = await _mediator.Send(new UpdateTaskCommand() { UpdateTaskDto = request });
                if (UpdateTask == null || !UpdateTask.IsSuccess) { return Result<bool>.Failure("fail to update record"); }
                return Result<bool>.Success(true);

            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }


        [HttpPost("DeleteTask")]

        public async Task<Result<bool>> UpdateTask(int Id )
        {
            try
            {
                _logger.LogInformation("UpdateTask");
                var DeleteTask = await _mediator.Send(new DeleteTaskCommand() { Id = Id });
                if (DeleteTask == null || !DeleteTask.IsSuccess) { return Result<bool>.Failure("fail to delete record"); }
                return Result<bool>.Success(true);

            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        [HttpGet("GetTaskByTask")]

        public async Task<Result<GetTaskByIdDto>> GetTaskByTask(int Id)
        {
            try
            {
                _logger.LogInformation("GetTaskByTask");
                var TaskRecord = await _mediator.Send(new GetTaskByIdQuery() { Id = Id });
                if (TaskRecord == null || !TaskRecord.IsSuccess) { return Result<GetTaskByIdDto>.Failure("record not found"); }
                return Result<GetTaskByIdDto>.Success(TaskRecord.Data);

            }
            catch (Exception ex)
            {
                return Result<GetTaskByIdDto>.Failure(ex.Message);
            }
        }
        [HttpGet("GetAllTasks")]

        public async Task<Result<List<GetAllTasksDto>>> GetAllTasks()
        {
            try
            {
                _logger.LogInformation("GetAllTasks");
                var GetAllTasksList = await _mediator.Send(new GetAllTaskQuery() {  });
                if (GetAllTasksList == null || !GetAllTasksList.IsSuccess) { return Result<List<GetAllTasksDto>>.Failure("no record found"); }
                return Result<List<GetAllTasksDto>>.Success(GetAllTasksList.Data);

            }
            catch (Exception ex)
            {
                return Result<List<GetAllTasksDto>>.Failure(ex.Message);
            }
        }
    }
}
