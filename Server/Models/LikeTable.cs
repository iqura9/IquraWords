using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class LikeTable
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        [Column("User_Id")]
        public int User_Id { get; set; }
        public User User { get; set; }
        [ForeignKey("Collection")]
        [Column("Collection_Id")]
        public int Collection_Id { get; set; }
        public Collection Collection { get; set; }
    }
}
