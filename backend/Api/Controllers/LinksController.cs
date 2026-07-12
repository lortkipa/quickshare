using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ILinkService _linkService;

        public LinksController(ILinkService linkService)
        {
            _linkService = linkService;    
        }

        [HttpPost]
        public async Task<ActionResult<LinkDto>> CreateAsync(IFormFile[] files)
        {
            var link = await _linkService.CreateAsync(files);

            return Ok(link);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetAsync([FromQuery] string slug)
        {
            return Ok(slug);
        }

        [HttpGet("{slug}/download")]
        public async Task<IActionResult> DownloadFilesAsync([FromQuery] string slug)
        {
            return Ok(slug);
        }
    }
}
