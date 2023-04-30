using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class WordMeaning
    {
        public WordMeaning() { }

        public int Id { get; set; }
        public string? Image_URL { get; set; }
        [Range(0, 1, ErrorMessage = "Invalid level. Verified should be between 0 and 1.")]
        public int Verified { get; set; } = 0;
        [ForeignKey("Term")]
        [Column("Term_Id")]
        public int Term_Id { get; set; }
        public Word Term { get; set; }

        [ForeignKey("Meaning")]
        [Column("Meaning_Id")]
        public int Meaning_Id { get; set; }
        public Word Meaning { get; set; }

        [ForeignKey("Cluster")]
        [Column("Cluster_Id")]
        public int Cluster_Id { get; set; }
        public Cluster Cluster { get; set; }
        [InverseProperty("WordMeaning")]
        public ICollection<UserWord> UserWords { get; set; }
    }
}
