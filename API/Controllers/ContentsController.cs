using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S3_Api_indi.Models;

namespace S3_Api_indi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private readonly MovieComContext _context;

        public ContentsController(MovieComContext context)
        {
            _context = context;
        }

        // GET: api/Contents
        [HttpGet("GetMovies/")]
        public async Task<ActionResult<IEnumerable<Content>>> GetContentsMovie()
        {
            return await _context.Contents.Where(s => s.Movie == 1).ToListAsync();
        }

        // GET: api/Contents
        [HttpGet("GetShows/")]
        public async Task<ActionResult<IEnumerable<Content>>> GetContentsShows()
        {
            var content = await _context.Contents.Where(s => s.Movie == 0).ToListAsync();

            for (int i = 0; i < content.Count; i++)
            {
                content[i].Seasons = await _context.Seasons.Where(c => c.ContentId == content[i].Id && c.Status == 1).ToListAsync();
                for (int j = 0; j < content[i].Seasons.Count; j++)
                {
                    content[i].Seasons[j].Episodes = await _context.Episodes.Where(c => c.Id == content[i].Seasons[j].Id).ToListAsync();
                }
            }

            return content;
        }

        // GET: api/Contents/10
        [HttpGet("{amount:int}")]
        public async Task<ActionResult<IEnumerable<Content>>> GetNewestContents(int amount)
        {
            return await _context.Contents.Where(c => c.Status == 1).OrderByDescending(r => r.Date).Take(amount).ToListAsync();
        }

        // GET: api/Contents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Content>> GetContent(int id)
        {
            var content = await _context.Contents.FindAsync(id);

            if (content == null)
            {
                return NotFound();
            }

            return content;
        }

        // GET: api/Contents/Drama
        [HttpGet("GetContentbyGenre/")]
        public async Task<ActionResult<IEnumerable<Content>>> GetContentbyGenre(int genreId)
        {
            var ContentGenre = new List<int>();

            var id = await _context.ContentGenres.Where(g => g.GenreId == genreId).ToListAsync();
            if (id.Count == 0)
            {
                return NotFound();
            }
            for (int i = 0; i < id.Count; i++)
            {
                ContentGenre.Add((int)id[i].ContentId);

            }

            var content = await _context.Contents.Where(c => ContentGenre.Contains(c.Id)).ToListAsync();

            if (content == null)
            {
                return NotFound();
            }

            for (int i = 0; i < content.Count; i++)
            {
                if (content[i].Movie == 0)
                {
                    content[i].Seasons = await _context.Seasons.Where(c => c.ContentId == content[i].Id && c.Status == 1).ToListAsync();
                    for (int j = 0; j < content[i].Seasons.Count; j++)
                    {
                        content[i].Seasons[j].Episodes = await _context.Episodes.Where(c => c.Id == content[i].Seasons[j].Id).ToListAsync();
                    }
                }
            }




            return content;
        }



        // GET: api/Contents/Drama
        [HttpGet("GetContentInFavourite/")]
        public async Task<ActionResult<IEnumerable<Content>>> GetContentInFavourite(int userId)
        {
            var ContentID = new List<int>();

            var id = await _context.Favourites.Where(g => g.UserId == userId).ToListAsync();

            for (int i = 0; i < id.Count; i++)
            {
                ContentID.Add((int)id[i].ContentId);

            }

            var content = await _context.Contents.Where(c => ContentID.Contains(c.Id)).ToListAsync();

            for (int i = 0; i < content.Count; i++)
            {
                if (content[i].Movie == 0)
                {
                    content[i].Seasons = await _context.Seasons.Where(c => c.ContentId == content[i].Id && c.Status == 1).ToListAsync();
                    for (int j = 0; j < content[i].Seasons.Count; j++)
                    {
                        content[i].Seasons[j].Episodes = await _context.Episodes.Where(c => c.Id == content[i].Seasons[j].Id).ToListAsync();
                    }
                }
            }

            if (content == null)
            {
                return NotFound();
            }

            return content;
        }


        // GET: api/Contents/5
        [HttpGet("{id,name}")]
        public async Task<ActionResult<Content>> GetContent(int id, string name)
        {
            var content = await _context.Contents.FindAsync(id);

            if (content == null)
            {
                return NotFound();
            }

            return content;
        }

        // PUT: api/Contents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContent(int id, Content content)
        {
            if (id != content.Id)
            {
                return BadRequest();
            }

            _context.Entry(content).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost("AddGenreToMovie")]
        public async Task<ActionResult<Content>> AddGenreToMovie([FromBody] Content content)
        {
            _context.Contents.Add(content);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContent", new { id = content.Id }, content);
        }

        [HttpPost("CreateMovie")]
        public async Task<ActionResult<Content>> CreateMovie([FromBody] Content content)
        {
            _context.Contents.Add(content);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContent", new { id = content.Id }, content);
        }

        [HttpPost("RemoveMovie")]
        public async Task<ActionResult<Content>> RemoveContent(int id)
        {
            var content = await _context.Contents.FindAsync(id);
            content.Status = 0;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContent", new { id = content.Id }, content);
        }

        [HttpPost("PublishContent")]
        public async Task<ActionResult<Content>> PublishContent(int id)
        {
            var content = await _context.Contents.FindAsync(id);
            content.Status = 1;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContent", new { id = content.Id }, content);
        }

        [HttpGet("AddToFavourite")]
        public async Task<ActionResult<IEnumerable<Favourite>>> AddToFavourite()
        {
            //Favourite favourite = new Favourite();
            //favourite.UserId = userId;
            //favourite.ContentId = contentId;


            return await _context.Favourites.ToListAsync();

            //await _context.SaveChangesAsync();

            //return "Succes";
        }

        // DELETE: api/Contents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(int id)
        {
            var content = await _context.Contents.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContentExists(int id)
        {
            return _context.Contents.Any(e => e.Id == id);
        }
    }
}
