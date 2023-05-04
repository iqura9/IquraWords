using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class UserWord
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime Date_Added { get; set; }
        public DateTime Date_Leaned { get; set; }
        //public DateTime Date_Updated { get; set;}
        public int WordMeaningId { get; set; }
        public virtual WordMeaning WordMeaning { get; set; } = null!;
        public string UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
