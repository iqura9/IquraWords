using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class LikeTable
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        [Column("User_Id")]
        public string User_Id { get; set; }
        public virtual User User { get; set; } = null!;
        [ForeignKey("Collection")]
        [Column("Collection_Id")]
        public int Collection_Id { get; set; }
        public virtual Collection Collection { get; set; } = null!;
    }
}
