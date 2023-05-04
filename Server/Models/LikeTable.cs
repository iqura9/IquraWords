using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class LikeTable
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; } = null!;
    }
}
