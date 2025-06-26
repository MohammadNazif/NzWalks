using NzWalks.Models.DomainModel;

namespace NzWalks.Repositories
{
    public interface IimageRepo
    {
        Task<Image> Upload(Image image);
    }
}
