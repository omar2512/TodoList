using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Domains;

namespace TodoList.Infrastrucutre.DataBaseContext
{
    public class TaskDataBaseContext: IdentityDbContext<User,Roles,Guid>
    {
   
        public TaskDataBaseContext(DbContextOptions<TaskDataBaseContext> options):base(options)
        {
                
        }
        public DbSet<TaskCateogory> TaskCateogory=> Set<TaskCateogory>();    
        public DbSet<TodoItems> TodoItems  => Set<TodoItems>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define a global filter for Products that ignores soft-deleted items
            modelBuilder.Entity<TodoItems>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<TaskCateogory>().HasQueryFilter(p => !p.IsDeleted);
        }

    }
}
