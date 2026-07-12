using Dal.Interfaces;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public class LinkRepository : BaseRepository<Link>, ILinkRepository
    {
        private readonly ProjectDBContext _context;

        public LinkRepository(ProjectDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Link?> GetBySlug(string slug)
        {
            return await _context.Links
                .Include(link => link.Files)
                .FirstOrDefaultAsync(link => link.Slug == slug);
        }
    }
}
