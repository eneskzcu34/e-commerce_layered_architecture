using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Shopping.Domain.Interfaces.Repositories;
using E_Shopping.Infrastructure.Persistence.Context;
using E_Shopping.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace E_Shopping.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }
    }
}