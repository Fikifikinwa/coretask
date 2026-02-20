using CoreTask.Api.Data;
using CoreTask.Api.Dto;
using CoreTask.Api.Mapper;
using CoreTask.Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoreTask.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly CoreTasksDbContext _context;

        public TaskService(CoreTasksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskDto>> GetAllAsync()
        {
            var tasks = await _context.TodoTasks.ToListAsync();

            return tasks.Select(TaskMapper.ToDto);
        }

        public async Task<PagedResult<TaskDto>> GetAllAsync(Pagination pagination, bool? isCompleted, string? sortBy, string? sortDirection)
        {
            var query = _context.TodoTasks.AsQueryable();

            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }

            query = ApplySorting(query, sortBy, sortDirection);

            var totalCount = await query.CountAsync();

            var tasks = await query               
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            var mapped = tasks.Select(TaskMapper.ToDto);

            return new PagedResult<TaskDto>
            {
                Items = mapped,
                TotalCount = totalCount,
                PageNumber =    pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<TaskDto?> GetByIdAsync(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);

            if (task == null)
                return null;

            return TaskMapper.ToDto(task);
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
        {
            var entity = TaskMapper.ToEntity(dto);

            if (dto.DueDate.HasValue && dto.DueDate.Value < DateTime.Now)
            {
                throw new ArgumentException("Due date cannot be in the past.");
            }

            _context.TodoTasks.Add(entity);
            await _context.SaveChangesAsync();

            return TaskMapper.ToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskDto dto)
        {
            var task = await _context.TodoTasks.FindAsync(id);

            if (task == null)
                return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;
            task.Priority = dto.Priority;
            task.IsCompleted = dto.IsCompleted;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);

            if (task == null)
                return false;

            _context.TodoTasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> MarkCompleteAsync(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);

            if (task == null)
                return false;

            task.IsCompleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

        private IQueryable<TodoTask> ApplySorting( IQueryable<TodoTask> query,  string? sortBy, string? sortDirection)
        {
            var isDescending = string.Equals(sortDirection?.ToLower(), "desc", StringComparison.InvariantCultureIgnoreCase);

            return sortBy?.ToLower() switch
            {
                "title" => isDescending
                    ? query.OrderByDescending(t => t.Title)
                    : query.OrderBy(t => t.Title),

                "priority" => isDescending
                    ? query.OrderByDescending(t => t.Priority)
                    : query.OrderBy(t => t.Priority),

                "duedate" => isDescending
                    ? query.OrderByDescending(t => t.DueDate)
                    : query.OrderBy(t => t.DueDate),

                _ => isDescending
                    ? query.OrderByDescending(t => t.CreatedAt)
                    : query.OrderBy(t => t.CreatedAt)
            };
        }
    }
}
