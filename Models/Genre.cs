using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace S3_Api_indi.Models
{
    [Table("Genre")]
    public partial class Genre
    {
        public Genre()
        {
            ContentGenres = new HashSet<ContentGenre>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Unicode(false)]
        public string? Name { get; set; }
        public byte? Status { get; set; }

        [InverseProperty("Genre")]
        public virtual ICollection<ContentGenre> ContentGenres { get; set; }
    }
}
