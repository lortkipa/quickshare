using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Interfaces
{
    public interface ILinkFileRepository : IBaseRepository<LinkFile>
    {
        Task<IEnumerable<LinkFile>> GetAllByLinkId(int id);
    }
}
