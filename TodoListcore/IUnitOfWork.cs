using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.RepositoryInterFaces;

namespace TodoList.Application
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepo<T> Repository<T>() where T : class;
        Task<int> CompleteAsync();
    }

}
