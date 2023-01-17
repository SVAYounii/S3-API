using Microsoft.EntityFrameworkCore;

namespace S3_Api_indi.Models
{
    public interface IMoviceComContext
    {
        DbSet<ContentGenre> ContentGenres { get; set; }
        DbSet<Content> Contents { get; set; }
        DbSet<Episode> Episodes { get; set; }
        DbSet<Favourite> Favourites { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Refreshtoken> Refreshtokens { get; set; }
        DbSet<Season> Seasons { get; set; }
        DbSet<User> Users { get; set; }
    }
}