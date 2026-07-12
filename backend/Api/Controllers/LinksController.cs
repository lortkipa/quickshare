using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync()
        {
            return Ok();
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
