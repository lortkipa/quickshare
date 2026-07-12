using Microsoft.AspNetCore.Http;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface ILinkService
    {
        Task<LinkDto> CreateAsync(IFormFile[] files);
    }
}
