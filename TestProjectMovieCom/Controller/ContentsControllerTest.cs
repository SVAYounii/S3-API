using FluentAssertions;
using S3_Api_indi.Controllers;
using TestProject.Repository;
using Xunit;
using Assert = Xunit.Assert;

namespace TestProjectMovieCom.ControllersF
{
    public class ContentsControllerTest
    {
        DatabaseContext _dbcontext = new DatabaseContext();

        [Fact]
        public async void Should_GetTitleOfMovie_WhenGetContent()
        {
            //Arange
            string name = "Movie: 7";
            int id = 8;
            var DbContext = await _dbcontext.GetDatabaseContextWithValue();
            var GenrController = new ContentsController(DbContext);

            //Act
            var result = GenrController.GetContent(id);

            //Assert
            Assert.Equal(result.Result.Value.Title, name);
        }

        [Fact]
        public async void Should_GetMultipleMovie_WhenGetContents()
        {
            //Arange
            var DbContext = await _dbcontext.GetDatabaseContextWithValue();
            var ContentsController = new ContentsController(DbContext);

            //Act
            var result = ContentsController.GetContentsMovie();

            //Assert
            Assert.True(result.Result.Value.Count() > 0 ? true : false);
        }

        [Fact]
        public async void Should_GetMultipleShows_WhenGetContents()
        {
            //Arange
            var DbContext = await _dbcontext.GetDatabaseContextWithValue();
            var ContentsController = new ContentsController(DbContext);

            //Act
            var result = ContentsController.GetContentsShows();

            //Assert
            Assert.True(result.Result.Value.Count() > 0 ? true : false);
        }

        [Fact]
        public async void Should_Get5NewestContent_WhenGetNewestContentsIsCalled()
        {
            //Arange
            int amount = 5;
            var DbContext = await _dbcontext.GetDatabaseContextWithValue();
            var ContentsController = new ContentsController(DbContext);

            //Act
            var result = ContentsController.GetNewestContents(amount);

            //Assert
            Assert.True(result.Result.Value.Count() > 0 ? true : false);
        }

        [Fact]
        public async void Should_Get0NewestContent_WhenGetNewestContentsIsCalled()
        {
            //Arange
            int amount = 0;
            var DbContext = await _dbcontext.GetDatabaseContextWithValue();
            var ContentsController = new ContentsController(DbContext);

            //Act
            var result = ContentsController.GetNewestContents(amount);

            //Assert
            Assert.True(result.Result.Value.Count() == 0 ? true : false);
        }

        [Fact]
        public async void Should_GetContent_WhenGivenCorrectId()
        {
            //Arange
            int id = 1;
            string title = "Movie: 0";
            var DbContext = await _dbcontext.GetDatabaseContextWithValue();
            var ContentsController = new ContentsController(DbContext);

            //Act
            var result = ContentsController.GetContent(id);

            //Assert
            Assert.Equal(result.Result.Value.Title, title);
        }

        [Fact]
        public async void Should_ThrowNull_WhenGivenIncorrectId()
        {
            //Arange
            int id = 9999;
            var DbContext = await _dbcontext.GetDatabaseContextWithValue();
            var ContentsController = new ContentsController(DbContext);

            //Act
            var result = ContentsController.GetContent(id);

            //Assert
            Assert.Null(result.Result.Value);
        }

        [Fact]
        public async void Should_GetAllContentWithSameGenre_WhenGivenCorrectGenreId()
        {
            //Arange
            int id = 2;
            var DbContext = await _dbcontext.GetDatabaseContextWithValue();
            var ContentsController = new ContentsController(DbContext);

            //Act
            var result = ContentsController.GetContentbyGenre(id);

            //Assert
            Assert.True(result.Result.Value.Count() > 0 ? true : false);
            result.Result.Value.ToList().ForEach(x => Assert.True(x.ContentGenres[0].GenreId == 2));

        }

        [Fact]
        public async void Should_ThrowNull_WhenGivenCorrectGenreId()
        {
            //Arange
            int id = 9999;
            var DbContext = await _dbcontext.GetDatabaseContextWithValue();
            var ContentsController = new ContentsController(DbContext);

            //Act
            var result = ContentsController.GetContentbyGenre(id);

            //Assert
            Assert.Null(result.Result.Value);


        }

        [Fact]
        public async void Should_GetContentsInFavourite_WhenGivenCorrectUserId()
        {
            //Arange
            int user = 0;
            var DbContext = await _dbcontext.GetDatabaseContextWithValue();
            var ContentsController = new ContentsController(DbContext);

            //Act
            var result = ContentsController.GetContentInFavourite(user);

            //Assert
            Assert.True(result.Result.Value.Count() > 0 ? true : false);


        }
    }
}