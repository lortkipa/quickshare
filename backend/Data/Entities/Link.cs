using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Link
    {
        public int Id { get; set; }
        public string Slug { get; set; } = null!;

        public ICollection<LinkFile> Files { get; set; } = new List<LinkFile>();
    }
}
