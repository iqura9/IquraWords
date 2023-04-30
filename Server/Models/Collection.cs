using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsPrivate { get; set; } = false;
        [InverseProperty("Collection")]
        public ICollection<Cluster> Clusters { get; set; }
        [InverseProperty("Collection")]
        public ICollection<LikeTable> LikeTable { get; set; }
    }
}
