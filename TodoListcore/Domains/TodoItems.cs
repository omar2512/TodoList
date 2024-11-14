using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Enums;

namespace TodoList.Domain.Domains
{
    public class TodoItems:BaseDomain
    {
        [Key ,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string TaskTitle { get; set; }
        [Required,MaxLength(500)]
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
        [Required]
        public DateOnly DueDate { get; set; }
        [Required]  
        public string DueTime { get; set; }
        public PriorityLevel Priority { get; set; }
        [ForeignKey(nameof(TaskCateogory))]
        public int TaskCateogoryId { get; set; }
        public TaskCateogory TaskCateogory { get; set; }

    }
}
