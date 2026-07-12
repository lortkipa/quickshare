using Dal.Interfaces;
using Dal.Repositories;
using Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Service.Dtos;
using Service.Interfaces;

namespace Service.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;
        private readonly ILinkFileRepository _linkFileRepository;
        private readonly string _webRootPath;

        public LinkService(ILinkRepository linkRepository, ILinkFileRepository linkFileRepository, IWebHostEnvironment environment)
        {
            _linkRepository = linkRepository;
            _linkFileRepository = linkFileRepository;
            _webRootPath = environment.WebRootPath ?? Path.Combine(environment.ContentRootPath, "wwwroot");
        }

        public async Task<LinkDto> CreateAsync(IFormFile[] files)
        {
            string slug;
            do
            {
                slug = Random.Shared.Next(100000, 1000000).ToString();
            } while (await _linkRepository.GetBySlug(slug) != null);

            var shareDirectory = Path.Combine(_webRootPath, "shares", slug);
            Directory.CreateDirectory(shareDirectory);

            try
            {
                var storedFiles = new List<string>();
                foreach (var file in files ?? Array.Empty<IFormFile>())
                {
                    if (file == null || file.Length == 0)
                        continue;

                    var fileName = Path.GetFileName(file.FileName);
                    if (string.IsNullOrWhiteSpace(fileName))
                        continue;

                    var storedPath = GetAvailableFilePath(shareDirectory, fileName);
                    await using var stream = File.Create(storedPath);
                    await file.CopyToAsync(stream);

                    storedFiles.Add(Path.GetRelativePath(_webRootPath, storedPath));
                }

                var link = new Link { Slug = slug };
                await _linkRepository.AddAsync(link);

                foreach (var path in storedFiles)
                {
                    await _linkFileRepository.AddAsync(new LinkFile
                    {
                        LinkId = link.Id,
                        Path = path
                    });
                }
                _ = ScheduleDirectoryDeletionAsync(shareDirectory, slug);

                return new LinkDto { Slug = slug, Expired = false };
            }
            catch
            {
                if (Directory.Exists(shareDirectory))
                    Directory.Delete(shareDirectory, recursive: true);

                throw;
            }
        }

        private async Task ScheduleDirectoryDeletionAsync(string directory, string slug)
        {
            try
            {
                await Task.Delay(TimeSpan.FromHours(24));
                if (Directory.Exists(directory))
                    Directory.Delete(directory, recursive: true);

                var link = await _linkRepository.GetBySlug(slug);
                if (link != null)
                {
                    link.Expired = true;
                    await _linkRepository.UpdateAsync(link);
                }
            }
            catch
            {
            }
        }

        private static string GetAvailableFilePath(string directory, string fileName)
        {
            var path = Path.Combine(directory, fileName);
            if (!File.Exists(path))
                return path;

            var name = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);
            var suffix = 1;

            do
            {
                path = Path.Combine(directory, $"{name} ({suffix++}){extension}");
            } while (File.Exists(path));

            return path;
        }
    }
}
