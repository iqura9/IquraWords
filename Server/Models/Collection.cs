﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsPrivate { get; set; } = false;

        [ForeignKey("User")]
        [Column("User_Id")]
        public string User_Id { get; set; }
        public virtual User User { get; set; } = null!;

        [InverseProperty("Collection")]
        public virtual ICollection<Cluster> Clusters { get; set; } = new List<Cluster>();
        [InverseProperty("Collection")]
        public virtual ICollection<LikeTable> LikeTable { get; } = new List<LikeTable>();

    }
}
