using AutoMapper;
using FreeCourses.Service.Catalog.Dtos;
using FreeCourses.Service.Catalog.Models;

namespace FreeCourses.Service.Catalog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Feature, FeatureCreateDto>().ReverseMap();

            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
            CreateMap<Feature, FeatureUpdateDto>().ReverseMap();
        }
    }
}
