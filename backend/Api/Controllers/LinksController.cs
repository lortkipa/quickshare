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
        private readonly ILinkFileService _linkFileService;

        public LinksController(ILinkService linkService, ILinkFileService linkFileService)
        {
            _linkService = linkService;
            _linkFileService = linkFileService;
        }

        [HttpPost]
        public async Task<ActionResult<LinkDto>> CreateAsync([FromForm] IFormFile[] files)
        {
            var link = await _linkService.CreateAsync(files);
            Console.WriteLine($"New link created: {link.Slug}");

            return CreatedAtRoute("GetLinkBySlug", new { slug = link.Slug }, link);
        }

        [HttpGet("{slug}", Name = "GetLinkBySlug")]
        public async Task<ActionResult<LinkWithFilesDto>> GetAsync(string slug)
        {
            try
            {
                var linkWithFiles = await _linkFileService.GetAllBySlugAsync(slug);
                return Ok(linkWithFiles);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{slug}/download")]
        public async Task<IActionResult> DownloadFilesAsync(string slug)
        {
            return Ok(slug);
        }
    }
}
