using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace S3_Api_indi.Models
{
    [Table("ContentGenre")]
    public partial class ContentGenre
    {
        [Column("ContentID")]
        public int? ContentId { get; set; }
        [Column("GenreID")]
        public int? GenreId { get; set; }
        [Key]
        public int Id { get; set; }

        [ForeignKey("ContentId")]
        [InverseProperty("ContentGenres")]
        public virtual Content? Content { get; set; }
        [ForeignKey("GenreId")]
        [InverseProperty("ContentGenres")]
        public virtual Genre? Genre { get; set; }
    }
}
