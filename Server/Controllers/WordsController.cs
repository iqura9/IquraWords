using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly WordsDbContext _context;

        public WordsController(WordsDbContext context)
        {
            _context = context;
        }

        // GET: api/Words
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Word>>> GetWord()
        {
          if (_context.Words == null)
          {
              return NotFound();
          }
            return await _context.Words.ToListAsync();
        }

        // GET: api/Words/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Word>> GetWord(int id)
        {
          if (_context.Words == null)
          {
              return NotFound();
          }
            var word = await _context.Words.FindAsync(id);

            if (word == null)
            {
                return NotFound();
            }

            return word;
        }

        // PUT: api/Words/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWord(int id, Word word)
        {
            if (id != word.Id)
            {
                return BadRequest();
            }

            _context.Entry(word).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WordExists(id))
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

        // POST: api/Words
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Word>> PostWord(Word word)
        {
          if (_context.Words == null)
          {
              return Problem("Entity set 'WordsDbContext.Word'  is null.");
          }
            _context.Words.Add(word);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWord", new { id = word.Id }, word);
        }

        // DELETE: api/Words/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(int id)
        {
            if (_context.Words == null)
            {
                return NotFound();
            }
            var word = await _context.Words.FindAsync(id);
            if (word == null)
            {
                return NotFound();
            }

            _context.Words.Remove(word);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// get Word
        /// </summary>
        [HttpGet("term")]
        public async Task<IActionResult> FindWord(string term, string language)
        {
            if (_context.Words == null)
            {
                return NotFound();
            }

            var words = await _context.Words
                .Include(w => w.Language)
                .Where(w => w.Language.Short_Name == language && w.Term.StartsWith(term))
                .Select(w => new { w.Id, w.Term, w.Language })
                .ToListAsync();

            if (words == null)
            {
                return NotFound();
            }

            return Ok(words);
        }

        private bool WordExists(int id)
        {
            return (_context.Words?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
