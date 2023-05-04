using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public class User : IdentityUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime Registration_date { get; set; }
        public string? Avatar_url { get; set; } = null;
        [InverseProperty("User")]
        public virtual ICollection<UserWord> UserWords { get; } = new List<UserWord>();
        [InverseProperty("User")]
        [JsonIgnore]
        public virtual ICollection<LikeTable> LikeTables { get; } = new List<LikeTable>();
        [InverseProperty("User")]
        [JsonIgnore]
        public virtual ICollection<Collection> Collections { get; } = new List<Collection>();
    }
}
