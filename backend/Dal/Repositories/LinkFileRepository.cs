using Dal.Interfaces;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Repositories
{
    public class LinkFileRepository : BaseRepository<LinkFile>, ILinkFileRepository
    {
        private readonly ProjectDBContext _context;

        public LinkFileRepository(ProjectDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LinkFile>> GetAllByLinkId(int id)
        {
            return await _context.LinkFiles
                .Where(linkFile => linkFile.LinkId == id)
                .ToListAsync();
        }
    }
}
