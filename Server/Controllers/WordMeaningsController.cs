using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using ClosedXML.Excel;

using System.Text.Json.Serialization;
using System.Text.Json;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordMeaningsController : ControllerBase
    {
        private readonly WordsDbContext _context;

        public WordMeaningsController(WordsDbContext context)
        {
            _context = context;
        }

        // GET: api/WordMeanings
        /// <summary>
        /// Get a list of WordsMeaning.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WordMeaning>>> GetWordMeaning()
        {
            if (_context.WordMeanings == null)
            {
                return NotFound();
            }

            var wordMeanings = await _context.WordMeanings
                .Include(wm => wm.Meaning)
                .Include(wm => wm.Term)
                .Include(wm => wm.Term.Language)
                .Include(wm => wm.Meaning.Language)
                //.Take(50)
                .ToListAsync();

            return Ok(wordMeanings);
        }


        // GET: api/WordMeanings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WordMeaning>> GetWordMeaning(int id)
        {
          if (_context.WordMeanings == null)
          {
              return NotFound();
          }
            var wordMeaning = await _context.WordMeanings.FindAsync(id);

            if (wordMeaning == null)
            {
                return NotFound();
            }

            return wordMeaning;
        }

        // PUT: api/WordMeanings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWordMeaning(int id, WordMeaning wordMeaning)
        {
            if (id != wordMeaning.Id)
            {
                return BadRequest();
            }

            _context.Entry(wordMeaning).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WordMeaningExists(id))
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

        // POST: api/WordMeanings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WordMeaning>> PostWordMeaning(WordMeaning wordMeaning)
        {
          if (_context.WordMeanings == null)
          {
              return Problem("Entity set 'WordsDbContext.WordMeaning'  is null.");
          }
            _context.WordMeanings.Add(wordMeaning);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWordMeaning", new { id = wordMeaning.Id }, wordMeaning);
        }

        // DELETE: api/WordMeanings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWordMeaning(int id)
        {
            if (_context.WordMeanings == null)
            {
                return NotFound();
            }
            var wordMeaning = await _context.WordMeanings.FindAsync(id);
            if (wordMeaning == null)
            {
                return NotFound();
            }

            _context.WordMeanings.Remove(wordMeaning);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Import
        /// </summary>
        [HttpPost("import")]
        //[Authorize(Roles = "admin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel == null)
                {
                    return BadRequest("400 error");
                }
                using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                {
                    await fileExcel.CopyToAsync(stream);
                    using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                    {
                        foreach (IXLWorksheet worksheet in workBook.Worksheets)
                        {
                            IXLRow firstRow = worksheet.RowsUsed().First();
                            if (firstRow.IsEmpty()) return null;

                            string termLanguageShort = firstRow.Cell(1).Value.ToString();
                            string definitionLanguageShort = firstRow.Cell(2).Value.ToString();
                            Language termLang = new Language();
                            Language definitionLang = new Language();

                            var l1 = (from g in _context.Languages
                                      where g.Short_Name.Contains(termLanguageShort)
                                      select g).ToList();
                            if (l1.Count > 0)
                            {
                                termLang = l1[0];
                            }
                            var l2 = (from g in _context.Languages
                                      where g.Short_Name.Contains(definitionLanguageShort)
                                      select g).ToList();
                            if (l2.Count > 0)
                            {
                                definitionLang = l2[0];
                            }


                            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                            {
                                try
                                {
                                    // add validation for duplicate
                                    string termValue = row.Cell(1).Value.ToString();
                                    string definitionValue = row.Cell(2).Value.ToString();

                                    var existingWordMeaning = await _context.WordMeanings
                                        .Include(wm => wm.Term)
                                        .Include(wm => wm.Meaning)
                                        .Where(wm => wm.Term.Term == termValue && wm.Meaning.Term == definitionValue)
                                        .FirstOrDefaultAsync();

                                    if (existingWordMeaning != null)
                                    {
                                        // handle duplicate error
                                        return BadRequest($"The term '{termValue}' with meaning '{definitionValue}' already exists.");
                                    }

                                    // create new entities and add to the context
                                    Word term = new Word();
                                    term.Term = termValue;
                                    term.Language = termLang;

                                    Word definition = new Word();
                                    definition.Term = definitionValue;
                                    definition.Language = definitionLang;

                                    WordMeaning wordMeaning = new WordMeaning();
                                    wordMeaning.Term = term;
                                    wordMeaning.Meaning = definition;

                                    _context.WordMeanings.Add(wordMeaning);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"An error occurred while processing row {row.RowNumber()}: {e.Message}");
                                }
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                }
            }
            return Ok();
        }

        /// <summary>
        /// get Word
        /// </summary>
        [HttpGet("definition")]
        public async Task<IActionResult> FindWordMeaning(string term, string language)
        {
            if (_context.WordMeanings == null)
            {
                return NotFound();
            }

            var words = await _context.WordMeanings
                .Include(w => w.Meaning)
                .Include(w => w.Term)
                .Include(w => w.Term.Language)
                .Where(w => w.Term.Language.Short_Name == language && w.Term.Term.Equals(term))
                .Select(w => new { w.Meaning.Id, w.Meaning.Term, w.Meaning.Language })
                .ToListAsync();

            if (words == null)
            {
                return NotFound();
            }

            return Ok(words);
        }

        private bool WordMeaningExists(int id)
        {
            return (_context.WordMeanings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
