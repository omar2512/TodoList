using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Domain.Domains
{
    public class TaskCateogory:BaseDomain
    {
        [Required,MaxLength(100)]
        public string Name { get; set; }
        public List<TodoItems> TodoItems { get; set; } = new List<TodoItems>();
    }
}
