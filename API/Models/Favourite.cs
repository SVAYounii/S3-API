using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace S3_Api_indi.Models
{
    [Table("Favourite")]
    public partial class Favourite
    {
        [Column("ContentID")]
        public int? ContentId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Key]
        public int Id { get; set; }

        [ForeignKey("ContentId")]
        [InverseProperty("Favourites")]
        public virtual Content? Content { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Favourites")]
        public virtual User? User { get; set; }
    }
}
