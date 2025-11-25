namespace TaskFlow.Application.DTOs
{
    public class ProjectCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int OwnerId { get; set; }
    }

    public class ProjectUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class ProjectReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int OwnerId { get; set; }
    }
}
