using Dapper;
using FreeCourses.Shared.Dtos;
using Npgsql;
using System.Data;

namespace SellingCourse.Service.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;

        private readonly IDbConnection _dbConnection;
        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));

        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var deleteStatus = await _dbConnection.ExecuteAsync("DELETE FROM discount WHERE id=@Id", new { Id = id });
            return deleteStatus > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userid)
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount where userid=@UserId and code = @Code", new { UserId = userid, Code = code });
            var hasDiscount = discounts.FirstOrDefault();
            return hasDiscount == null ? Response<Models.Discount>.Fail("Discount not found", 404) : Response<Models.Discount>.Success(hasDiscount,200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount where id=@Id", new { Id = id })).SingleOrDefault();
            return discount==null ? Response<Models.Discount>.Fail("discount not found",404) : Response<Models.Discount>.Success(discount,200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) set (@UserId, @Rate, @Code)", discount);
            return saveStatus > 0 ? Response<NoContent>.Success(200) : Response<NoContent>.Fail("an occure error while adding",500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var updateStatus = await _dbConnection.ExecuteAsync("UPDATE discount SET userid=@UserId, rate = @Rate, code = @Code where id = @Id", new { UserId = discount.UserId, Rate = discount.Rate, Code = discount.Code, Id = discount.Id });
            return updateStatus > 0 ? Response<NoContent>.Success(200) : Response<NoContent>.Fail("an occure error while updating", 500);
        }
    }
}
