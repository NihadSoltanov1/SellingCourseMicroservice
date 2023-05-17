using AutoMapper;
using FreeCourses.Service.Catalog.Dtos;
using FreeCourses.Service.Catalog.Models;
using FreeCourses.Service.Catalog.Settings;
using FreeCourses.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourses.Service.Catalog.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAll()
        {
           var categories = await _categoryCollection.Find(x => true).ToListAsync();
            return Response<CategoryDto>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> Create(CategoryCreateDto categoryCreateDto)
        {
            var newCategory = _mapper.Map<Category>(categoryCreateDto);
            await _categoryCollection.InsertOneAsync(newCategory);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(newCategory), 200);
           
        }

        public async Task<Response<CategoryDto>> GetById(string id)
        {
          var category =   await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
            if (category == null) return Response<CategoryDto>.Fail("This Category can not find", 404);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
