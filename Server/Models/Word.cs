using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Term { get; set; }
        [Range(0, 6, ErrorMessage = "Invalid level. Level should be between 0 and 6.")]
        public int Level { get; set; } // 0 - Undefined; 1 - A1; 2 - A2; 3 - B1; 4 - B2; 5 - C1; 6 - C2;
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; } = null!;
        [JsonIgnore]
        [InverseProperty("Term")]
        public virtual ICollection<WordMeaning> Terms { get; } = new List<WordMeaning>();
        [InverseProperty("Meaning")]
        public virtual ICollection<WordMeaning> Meanings { get; } = new List<WordMeaning>();
    }
}
