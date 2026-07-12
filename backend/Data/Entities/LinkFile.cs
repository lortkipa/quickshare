using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class LinkFile
    {
        public int Id { get; set; }
        public int LinkId { get; set; }
        public string Path { get; set; } = null!;

        public Link Link { get; set; } = null!;
    }
}
