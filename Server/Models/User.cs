using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Registration_date { get; set; }
        public string? Avatar_url { get; set; } = null;
        [InverseProperty("User")]
        public ICollection<UserWord> UserWords { get; set; }
        [InverseProperty("User")]
        public ICollection<LikeTable> LikeTable { get; set; }
    }
}
