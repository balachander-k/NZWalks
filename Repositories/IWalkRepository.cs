using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(String? filterOn=null, String? filterQuery=null,String? sortBy=null,bool? isAscending=true, int PageNumber=1, int PageSize=5);

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> UpdateAsync(Guid id,Walk walk);

        Task<Walk?> DeleteAsync (Guid id);
    }
}
