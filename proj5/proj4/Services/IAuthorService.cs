using proj5.Domain.Models;

namespace proj4.Services
{
    public interface IAuthorService
    {
        Task<ServiceReponse<Author>> GetAuthorByIdAsync(int id);
        Task<ServiceReponse<List<Author>>> GetAllAuthorAsync();
        Task<ServiceReponse<Author>> AddAuthorAsync(Author author);
        Task<ServiceReponse<Author>> UpdateAuthorAsync(Author author);
        Task<ServiceReponse<Author>> DeleteAuthorAsync(int id);
    }
}