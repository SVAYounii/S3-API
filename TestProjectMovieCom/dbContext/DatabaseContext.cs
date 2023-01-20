using Microsoft.EntityFrameworkCore;
using S3_Api_indi.Models;
using S3_Api_indi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S3_Api_indi.Controllers;

namespace TestProject.Repository
{
    public class DatabaseContext
    {
       

        public async Task<MovieComContext> GetDatabaseContextWithValue()
        {
            var option = new DbContextOptionsBuilder<MovieComContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new MovieComContext(option);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Contents.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Content content = new Content()
                    {
                        Title = "Movie: " + i.ToString(),
                        Status = 1,
                        Movie = (byte?)(new Random().Next(0, 2) == 0 ? 1 : 0)


                    };
                    Genre genre = new Genre()
                    {
                        Name = "Genre: " + i.ToString(),
                        Status = 1,
                    };
                    User user = new User()
                    {
                        FirstName = "Name " + i.ToString(),
                        Username = "Username" + i.ToString(),
                        LastName = "Lastname " + i.ToString() ,
                        Password = "Password" + i.ToString(),
                        Status = 1,
                    };
                    ContentGenre contentGenre = new ContentGenre()
                    {
                        Content = content,
                        ContentId = content.Id,
                        Genre = genre,
                        GenreId = genre.Id
                    };
                    Favourite favourite = new Favourite()
                    {
                        Content = content,
                        ContentId = content.Id,
                        UserId = user.Id
                    };
                    databaseContext.Contents.Add(
                        content
                    );
                    databaseContext.Genres.Add(
                       genre
                   );
                    databaseContext.ContentGenres.Add(
                       contentGenre
                    );
                    databaseContext.Users.Add(
                      user
                   );
                    databaseContext.Favourites.Add(
                      favourite
                   );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        public async Task<MovieComContext> GetDatabaseContext()
        {
            var option = new DbContextOptionsBuilder<MovieComContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new MovieComContext(option);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Genres.CountAsync() <= 0)
            {
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }




    }
}
