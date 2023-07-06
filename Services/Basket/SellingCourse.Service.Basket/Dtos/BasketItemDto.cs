namespace SellingCourse.Service.Basket.Dtos
{
    public class BasketItemDto
    {
        public int  Quantity { get; set; }
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
    }
}
