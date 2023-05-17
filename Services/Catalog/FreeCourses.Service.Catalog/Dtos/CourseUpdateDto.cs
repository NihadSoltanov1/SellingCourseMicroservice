using FreeCourses.Service.Catalog.Models;

namespace FreeCourses.Service.Catalog.Dtos
{
    public class CourseUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public Feature Feature { get; set; }
        public string CategoryId { get; set; }
    }
}
