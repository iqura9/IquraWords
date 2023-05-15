using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Controllers;
using Server.Data;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace UnitTestApp
{
    public class WordMeaningsControllerTests
    {
        private DbContextOptions<WordsDbContext> CreateNewContextOptions()
        {
            // In-memory database provider for testing
            return new DbContextOptionsBuilder<WordsDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task GetWordMeaning_ReturnsOkResult_WithWordMeanings()
        {
            // Arrange
            var options = CreateNewContextOptions();

            // Initialize the database with test data
            using (var context = new WordsDbContext(options))
            {
                var testLanguage = new Language { Name = "English", Short_Name = "en" };
                var testLanguage2 = new Language { Name = "Ukrainian", Short_Name = "ua" };
                var testWords = new List<Word>
                {
                    new Word { Term = "Word 1", Level = 1, Language = testLanguage },
                    new Word { Term = "Word 2", Level = 2, Language = testLanguage },
                    new Word { Term = "Слово 3", Level = 3, Language = testLanguage2 }
                };

                var testWordMeanings = new List<WordMeaning>
                {
                    new WordMeaning { Term = testWords[0], Meaning = testWords[2] },
                    new WordMeaning { Term = testWords[1], Meaning = testWords[2] },
                    new WordMeaning { Term = testWords[0], Meaning = testWords[1] },
                };

                context.Languages.Add(testLanguage);
                context.Languages.Add(testLanguage2);
                context.Words.AddRange(testWords);
                context.WordMeanings.AddRange(testWordMeanings);
                await context.SaveChangesAsync();
            }

            using (var context = new WordsDbContext(options))
            {
                var controller = new WordMeaningsController(context);

                // Act
                var result = await controller.GetWordMeaning();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var wordMeanings = Assert.IsAssignableFrom<IEnumerable<WordMeaning>>(okResult.Value);
                Assert.Equal(3, wordMeanings.Count());
            }
        }

        [Fact]
        public async Task FindWord_ReturnsOkResult_WithMatchingWords()
        {
            // Arrange
            var options = CreateNewContextOptions();

            using (var context = new WordsDbContext(options))
            {
                // Initialize the database with test data
                var testLanguage = new Language { Name = "English", Short_Name = "en" };
                var testWords = new List<Word>
                {
                    new Word { Term = "Apple", Level = 1, Language = testLanguage },
                    new Word { Term = "Banana", Level = 2,  Language = testLanguage },
                    new Word { Term = "Orange", Level = 3, Language = testLanguage },
                    new Word { Term = "Sophisticated", Level = 1, Language = testLanguage },
                    new Word { Term = "Some", Level = 2, Language = testLanguage },
                    new Word { Term = "Sophi", Level = 2, Language = testLanguage },
                };

                context.Languages.Add(testLanguage);
                context.Words.AddRange(testWords);
                context.SaveChanges();
            }

            using (var context = new WordsDbContext(options))
            {
                var controller = new WordsController(context);

                // Act
                var result = await controller.FindWord("So", "en");

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var words = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value);
                Assert.Equal(3, words.Count());
                var word = words.First();
                Assert.Equal("Sophisticated", word.GetType().GetProperty("Term").GetValue(word, null));
            }
        }
    }
}