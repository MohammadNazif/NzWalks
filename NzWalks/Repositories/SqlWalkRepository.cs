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

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            dbContext.walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;

        }
        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, String? filterQuery = null)
        {
            var walks = dbContext.walks.Include("Difficulty").Include("Region").AsQueryable();
            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
               if (filterOn.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase) && int.TryParse(filterQuery, out var length))
                    {
                        walks = walks.Where(x => x.LengthInKm == length);
                    }
                else if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            return await walks.ToListAsync();
            //return await dbContext.walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await dbContext.walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {

                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.ImageUrl = walk.ImageUrl;

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
