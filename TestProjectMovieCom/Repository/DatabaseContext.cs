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
        public async Task<MoviceComContext> GetDatabaseContext()
        {
            var option = new DbContextOptionsBuilder<MoviceComContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new MoviceComContext(option);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Genres.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Genres.Add(
                        new Genre()
                        {
                            Name = "GenreName: " + i.ToString(),
                            ContentGenres = new List<ContentGenre>(),
                            Status = 1


                        }
                    );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

     
     
    }
}
