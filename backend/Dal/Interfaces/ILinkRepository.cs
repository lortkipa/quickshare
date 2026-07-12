using Data.Entities;

namespace Dal.Interfaces
{
    public interface ILinkRepository : IBaseRepository<Link>
    {
        Task<Link?> GetBySlug(string slug);
    }
}
