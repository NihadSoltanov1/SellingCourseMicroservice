using AutoMapper;
using FreeCourses.Service.Catalog.Dtos;
using FreeCourses.Service.Catalog.Models;
using FreeCourses.Service.Catalog.Settings;
using FreeCourses.Shared.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace FreeCourses.Service.Catalog.Service
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<List<CourseDto>>> GetAsync()
        {
           var courses = await _courseCollection.Find(x => true).ToListAsync();
            if (courses.Any()) foreach (var course in courses) course.Category = await _categoryCollection.Find<Category>(x => x.Id ==course.CategoryId).FirstAsync();

            else courses = new List<Course>();
            return Response<CourseDto>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course =  await  _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null) return Response<CourseDto>.Fail("This Course can not find", 404);
            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.Id).FirstAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetByUserIdAsync(string userid)
        {
            var courses = await _courseCollection.Find<Course>(x => x.UserId == userid).ToListAsync();
            if (courses.Any()) { foreach (var course in courses) course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync(); }
            else courses = new List<Course>();
            return Response<CourseDto>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
              var newCourse = _mapper.Map<Course>(courseCreateDto);
             newCourse.CreatedTime = DateTime.Now;
             await _courseCollection.InsertOneAsync(newCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        } 
        public async Task<Response<Shared.Dtos.NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);
            _courseCollection.FindOneAndReplace(x => x.Id == courseUpdateDto.Id, updateCourse);
            if (updateCourse == null) return Response<Shared.Dtos.NoContent>.Fail("This Course can not updated",404);
            return Response<Shared.Dtos.NoContent>.Success(204);
        }

        public async Task<Response<Shared.Dtos.NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0) return Response<Shared.Dtos.NoContent>.Success(204);
            return Response<Shared.Dtos.NoContent>.Fail("This course can not deleted",404);
        }
    }



}

