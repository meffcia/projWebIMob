using proj5.Domain.Models;

namespace proj4.Services
{
    public interface IWriterService
    {
        Task<ServiceReponse<Writer>> GetWriterByIdAsync(int id);
        Task<ServiceReponse<List<Writer>>> GetAllWriterAsync();
        Task<ServiceReponse<Writer>> AddWriterAsync(Writer writer);
        Task<ServiceReponse<Writer>> UpdateWriterAsync(Writer writer);
        Task<ServiceReponse<Writer>> DeleteWriterAsync(int id);
    }
}