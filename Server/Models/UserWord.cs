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
        [ForeignKey("WordMeaning")]
        public int WordMeaning_id;
        public WordMeaning WordMeaning { get; set; }
        [ForeignKey("User")]
        public int User_id;
        public User User { get; set; }
    }
}
