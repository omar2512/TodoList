using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Infrastrucutre.DataBaseContext
{
    public class User: IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
