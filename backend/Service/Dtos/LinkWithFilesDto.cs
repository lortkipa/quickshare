using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Dtos
{
    public class LinkWithFilesDto
    {
        public LinkDto Link { get; set; } = null!;
        public IEnumerable<LinkFileDto> Files { get; set; } = null!;
    }
}
