using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Domain.Domains
{
    public class BaseDomain
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public Guid CraetedBy { get; set; }
        public DateTime Updated { get; set; } 
        public Guid UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
