using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Cluster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        [ForeignKey("Collection")]
        [Column("Collection_Id")]
        public int Collection_Id { get; set; }
        public virtual Collection Collection { get; set; } = null!;

        [InverseProperty("Cluster")]
        public virtual ICollection<WordMeaning> WordMeanings { get; } = new List<WordMeaning>();
        
    }
}
