using System.Runtime.InteropServices;
using NzWalks.Models.DomainModel;

namespace NzWalks.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn=null,String? filterQuery=null,string?
            sortBy=null,bool isAscending=true,int pageSize=1000,int PageNumber=1);
        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task<Walk> DeleteAsync(Guid id);

    }
}
