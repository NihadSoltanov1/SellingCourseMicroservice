

using FreeCourses.Service.Catalog.Dtos;
using FreeCourses.Shared.Dtos;

namespace FreeCourses.Service.Catalog.Service
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAll();
        Task<Response<CategoryDto>> GetById(string id);
        Task<Response<CategoryDto>> Create(CategoryCreateDto categoryCreateDto);
    }
}
