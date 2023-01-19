using Microsoft.EntityFrameworkCore;
using S3_Api_indi.Controllers;
using TestProject.Repository;
using FluentAssertions;

namespace TestProject
{
    public class UserTest
    {
        DatabaseContext _dbcontext = new DatabaseContext();

        [Fact]
        public async void Should_BeNotNull_WhenGetGenre()
        {
            //Arange
            string name = "GenreName: 7";
            int id = 8;
            var DbContext = await _dbcontext.GetDatabaseContext();
            var GenrController = new GenresController(DbContext);

            //Act
            var result = GenrController.GetGenre(id);

            //Assert
            result.Should().NotBeNull();
            Assert.Equal(result.Result.Value.Name, name);
        }

        [Fact]
        public async void Should_BeNotNull_WhenGetAllGenre()
        {
            //Arange
            string name = "GenreName: 7";
            int id = 8;
            var DbContext = await _dbcontext.GetDatabaseContext();
            var GenrController = new GenresController(DbContext);

            //Act
            var result = GenrController.GetGenres();

            //Assert
            result.Should().NotBeNull();
            Assert.True(result.Result.Value.Count() > 0 ? true : false);
        }
    }
}