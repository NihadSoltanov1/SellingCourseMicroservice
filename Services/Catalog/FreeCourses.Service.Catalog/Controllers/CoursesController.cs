using FreeCourses.Service.Catalog.Dtos;
using FreeCourses.Service.Catalog.Service;
using FreeCourses.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourses.Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _course;

        public CoursesController(ICourseService course)
        {
            _course = course;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _course.GetAsync();
            return CreateActionResultInstance(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _course.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [Route("/api/[controller]/GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(string id)
        {
            var response = await _course.GetByUserIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var response = await _course.CreateAsync(courseCreateDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto  courseUpdateDto)
        {
            var response = await _course.UpdateAsync(courseUpdateDto);
            return CreateActionResultInstance(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _course.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }



    }
}
