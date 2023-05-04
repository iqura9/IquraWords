using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public class Cluster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<WordMeaning> WordMeanings { get; } = new List<WordMeaning>();
        
    }
}
