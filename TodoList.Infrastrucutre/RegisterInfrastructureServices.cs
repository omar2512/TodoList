using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application;
using TodoList.Domain.RepositoryInterFaces;
using TodoList.Infrastrucutre.Repository;

namespace TodoList.Infrastrucutre
{
    public static class RegisterInfrastructureServices
    {
        public static void Register(this IServiceCollection services) { 
           services.AddScoped<IUnitOfWork,UniteOfWork>();
           services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
        }
    }
}
