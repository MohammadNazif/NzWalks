using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NzWalks.Models.DomainModel;

namespace NzWalks.Repositories
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly DatabaseContext dbContext;

        public SqlWalkRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
           return await dbContext.walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await dbContext.walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=>x.Id==id);
            
        }
    }
}
