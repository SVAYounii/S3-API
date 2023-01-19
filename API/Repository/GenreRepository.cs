using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using S3_Api_indi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace S3_Api_indi.Repository
{
    public class GenreRepository
    {
        private readonly MoviceComContext _context;

        public GenreRepository(MoviceComContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.Where(s => s.Status == 1).ToListAsync();
        }

        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);


            return genre;
        }
    }
}
