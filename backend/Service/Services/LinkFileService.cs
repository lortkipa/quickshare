using Dal.Interfaces;
using Service.Dtos;
using Service.Interfaces;

namespace Service.Services
{
    public class LinkFileService : ILinkFileService
    {
        private readonly ILinkFileRepository _linkFileRepository;
        private readonly ILinkRepository _linkRepository;

        public LinkFileService(ILinkFileRepository linkFileRepository, ILinkRepository linkRepository)
        {
            _linkFileRepository = linkFileRepository;
            _linkRepository = linkRepository;
        }

        public async Task<LinkWithFilesDto> GetAllBySlugAsync(string slug)
        {
            var link = await _linkRepository.GetBySlug(slug);
            if (link == null)
                throw new KeyNotFoundException($"Link with slug '{slug}' was not found.");

            var files = await _linkFileRepository.GetAllByLinkId(link.Id);
            if (files == null)
                throw new KeyNotFoundException($"Files with link Id '{link.Id}' was not found.");

            return new LinkWithFilesDto
            {
                Link = new LinkDto { Slug = link.Slug, Expired = link.Expired },
                Files = files.Select(file => new LinkFileDto
                {
                    Name = Path.GetFileName(file.Path)
                }).ToArray()
            };
        }
    }
}
