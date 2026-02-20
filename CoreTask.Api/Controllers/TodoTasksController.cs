using CoreTask.Api.Dto;
using CoreTask.Api.Model;
using CoreTask.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CoreTask.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TodoTasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TodoTasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll([FromQuery] Pagination pagination, [FromQuery] bool? isCompleted,
            [FromQuery] string? sortBy, [FromQuery] string? sortDirection)
        {

            if (pagination.PageNumber <= 0 || pagination.PageSize <= 0)
                return BadRequest("Page number and size must be greater than zero.");

            var tasks = await _taskService.GetAllAsync(pagination, isCompleted, sortBy, sortDirection);

            return Ok(new ApiResponse<PagedResult<TaskDto>>(true, "Tasks retrieved successfully.", tasks));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound(new ApiResponse<object>(false, "Task not found."));
            }


            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<TaskDto>>> Create(CreateTaskDto dto)
        {
            var createdTask = await _taskService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { Id = createdTask.Id }, 
                new ApiResponse<TaskDto>(true, "Task created successfully.", createdTask));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTaskDto dto)
        {
            var updated = await _taskService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound(new ApiResponse<object>(false, "Task not found."));

            return Ok(new ApiResponse<object>(true, "Task updated sucessfully."));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _taskService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new ApiResponse<object>(false, "Task not found."));

            return Ok(new ApiResponse<object>(true, "Task deleted sucessfully."));
        }

        [HttpPatch("{id}/complete")]
        public async Task<ActionResult> MarkComplete(int id)
        {
            var updated = await _taskService.MarkCompleteAsync(id);

            if (!updated)
                return NotFound(new ApiResponse<object>(false, "Task not found."));

            return Ok(new ApiResponse<object>(true, "Task updated sucessfully."));
        }
    }
}
