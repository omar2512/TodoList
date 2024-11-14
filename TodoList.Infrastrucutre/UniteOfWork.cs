using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application;
using TodoList.Domain.RepositoryInterFaces;
using TodoList.Infrastrucutre.DataBaseContext;
using TodoList.Infrastrucutre.Repository;

namespace TodoList.Infrastrucutre
{
    public class UniteOfWork : IUnitOfWork
    {
        private readonly TaskDataBaseContext _context;
        private readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();

        public UniteOfWork(TaskDataBaseContext context)
        {
            _context = context;
        }

        public IGenericRepo<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new GenericRepo<T> (_context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepo<T>)_repositories[type];
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
