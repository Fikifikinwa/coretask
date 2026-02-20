using CoreTask.Api.Dto;
using CoreTask.Api.Model;

namespace CoreTask.Api.Mapper
{
    public class TaskMapper
    {
        public static TaskDto ToDto(TodoTask task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                Priority = (TaskPriority)task.Priority
            };
        }

        public static TodoTask ToEntity(CreateTaskDto dto)
        {
            return new TodoTask
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = (TaskPriority)(dto.Priority),
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            };
        }
    }
}

