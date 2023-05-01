﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Language
    {
        public Language() 
        { 
            
        }
        public int Id { get; set; }
        public string Name { get; set; }
        [InverseProperty("Language")]
        public virtual ICollection<Word> Words { get; set;} = new List<Word>();
    }
}
