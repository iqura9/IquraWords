using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public class WordMeaning
    {
        public WordMeaning() { }

        public int Id { get; set; }
        public string? Image_URL { get; set; }
        [Range(0, 1, ErrorMessage = "Invalid level. Verified should be between 0 and 1.")]
        [ForeignKey("Term")]
        public int TermId { get; set; }
        public virtual Word Term { get; set; } = null!;
        [ForeignKey("Meaning")]
        public int MeaningId { get; set; }
        public virtual Word Meaning { get; set; } = null!;
        public int ClusterId { get; set; }
        public virtual Cluster Cluster { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<UserWord> UserWords { get; } = new List<UserWord>();
    }
}
