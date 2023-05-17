using FreeCourses.Service.Catalog.Dtos;
using FreeCourses.Shared.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FreeCourses.Service.Catalog.Service
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAsync();
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<List<CourseDto>>> GetByUserIdAsync(string userid);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
