using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using FreeCourses.Service.Catalog.Models;

namespace FreeCourses.Service.Catalog.Dtos
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedTime { get; set; }
        public Feature Feature { get; set; }
        public string CategoryId { get; set; }
    }
}
