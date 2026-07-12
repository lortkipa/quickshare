using Service.Dtos;

namespace Service.Interfaces
{
    public interface ILinkFileService
    {
        Task<LinkWithFilesDto> GetAllBySlugAsync(string slug);
    }
}
