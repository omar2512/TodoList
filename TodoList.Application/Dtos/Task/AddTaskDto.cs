using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Enums;

namespace TodoList.Application.Dtos.Task
{
    public class AddTaskDto
    {
        
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
        public DateOnly DueDate { get; set; }
        public string DueTime { get; set; }
        public PriorityLevel Priority { get; set; }
    }
}
