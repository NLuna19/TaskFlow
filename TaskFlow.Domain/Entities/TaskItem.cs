using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Todo;

        public int ProjectId { get; set; }

        public Project? Project { get; set; }
    }
}