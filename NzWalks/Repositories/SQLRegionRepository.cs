using Microsoft.EntityFrameworkCore;
using NzWalks.Models.DomainModel;

namespace NzWalks.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly DatabaseContext _db;
        public SQLRegionRepository(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<Region> CreateAsync(Region region)
        {
            await _db.regions.AddAsync(region);
            await _db.SaveChangesAsync();
            return region;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var region = await _db.regions.FindAsync(id);
            _db.regions.Remove(region);
            await _db.SaveChangesAsync();
            return true; // Assuming deletion is successful, you might want to check if the region was found first.
        }

        public async Task<List<Region>> GetAllAsync()
        {
          return await _db.regions.ToListAsync();

        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _db.regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var updateRegionDomain = await _db.regions.FindAsync(id);
            if (updateRegionDomain == null)
            {
                return null;
            }
            //Dto to domain conversion for updating database
            updateRegionDomain.Name = region.Name;
            updateRegionDomain.Code = region.Code;
            updateRegionDomain.RegionImageUrl = region.RegionImageUrl;
            await _db.SaveChangesAsync();
            return updateRegionDomain;
        }
    }
}
