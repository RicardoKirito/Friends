using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Interface.Repository
{
    public interface IGenericRepository<Entity>
        where Entity : class
    {
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int id);
        Task<Entity> AddAsync(Entity entity); 
        Task UpdateAsync(Entity entity, int id);
        Task DeleteAsync(int id);
        Task<List<Entity>> GetWithOne(List<string> properties);
    }
}
