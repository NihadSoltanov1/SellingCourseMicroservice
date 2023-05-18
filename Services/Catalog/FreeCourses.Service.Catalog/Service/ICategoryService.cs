

using FreeCourses.Service.Catalog.Dtos;
using FreeCourses.Shared.Dtos;

namespace FreeCourses.Service.Catalog.Service
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> GetByIdAsync(string id);
        Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto);
    }
}
