using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Dtos
{
    public class LinkDto
    {
        public string Slug { get; set; } = null!;
        public bool Expired { get; set; }
    }
}
