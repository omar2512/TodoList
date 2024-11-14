using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Commons;
using TodoList.Domain.RepositoryInterFaces;
using TodoList.Infrastrucutre.DataBaseContext;

namespace TodoList.Infrastrucutre.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly TaskDataBaseContext _context;

        public GenericRepo(TaskDataBaseContext context)
        {
            _context = context;
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public  IQueryable<T> GetAllAsync()
        {
            return  _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return  _context.Set<T>().Where(predicate).AsNoTracking();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

       
    }
 }
