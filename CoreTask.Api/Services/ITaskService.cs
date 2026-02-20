using CoreTask.Api.Dto;
using CoreTask.Api.Model;

namespace CoreTask.Api.Services
{
    public interface ITaskService
    {
        Task<PagedResult<TaskDto>> GetAllAsync(Pagination pagination, bool? isCompleted, string? sortBy, string? sortDirection);

        Task<TaskDto?> GetByIdAsync(int id);

        Task<TaskDto> CreateAsync(CreateTaskDto dto);

        Task<bool> UpdateAsync(int id, UpdateTaskDto dto);

        Task<bool> DeleteAsync(int id);

        Task<bool> MarkCompleteAsync(int id);
    }
}
