using Dal.Interfaces;
using Data;
using Data.Entities;
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
    }
}
