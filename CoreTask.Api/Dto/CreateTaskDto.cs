using CoreTask.Api.Model;
using System.ComponentModel.DataAnnotations;

namespace CoreTask.Api.Dto
{
    public class CreateTaskDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        [Range(1, 4)]
        public TaskPriority Priority { get; set; }
    }
}
