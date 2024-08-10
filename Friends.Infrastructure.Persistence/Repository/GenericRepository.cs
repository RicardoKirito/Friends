using Friends.Core.Application.Interface.Repository;
using Friends.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Infrastructure.Persistence.Repository
{
    public class GenericRepository<Entity> : IGenericRepository<Entity>
        where Entity: class
    {
        private readonly ApplicationContext context;

        public GenericRepository(ApplicationContext _context)
        {
            context = _context;
        }

        public async Task<List<Entity>> GetAllAsync()
        {
            return  context.Set<Entity>().ToList();

        }
        public async Task<Entity> GetByIdAsync(int id)
        {
            return await context.Set<Entity>().FindAsync(id);
        }
        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await context.Set<Entity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Entity entity, int id)
        {
            Entity entry = await context.Set<Entity>().FindAsync(id);
            context.Entry(entry).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(int id)
        {
            Entity entity = await context.Set<Entity>().FindAsync(id);
            context.Set<Entity>().Remove(entity);
            await context.SaveChangesAsync();

        }
        public virtual async Task<List<Entity>> GetWithOne(List<string> properties)
        {
            var query = context.Set<Entity>().AsQueryable();
            foreach (var property in properties)
            {
                query = query.Include(property);
            }
            return query.ToList();
        }
    }
}
