using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsPrivate { get; set; } = false;
        public string UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Cluster> Clusters { get; set; } = new List<Cluster>();
        [JsonIgnore]
        public virtual ICollection<LikeTable> LikeTables { get; } = new List<LikeTable>();

    }
}
