using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Language
    {
        public Language() 
        { 
            
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Short_Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Word> Words { get; set;} = new List<Word>();
    }
}
