namespace TaskFlow.Application.DTOs
{
    public class TaskCreateDto
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int AssignedUserId { get; set; }
    }

    public class TaskUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class TaskReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public int ProjectId { get; set; }
        public int AssignedUserId { get; set; }
    }
}
