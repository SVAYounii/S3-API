using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace S3_Api_indi.Models
{
    [Table("Content")]
    public partial class Content
    {
        public Content()
        {
            ContentGenres = new HashSet<ContentGenre>();
            Favourites = new HashSet<Favourite>();
            Seasons = new HashSet<Season>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Unicode(false)]
        public string? Title { get; set; }
        [Unicode(false)]
        public string? Description { get; set; }
        public int? Rating { get; set; }
        [Unicode(false)]
        public string? ImgUrl { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public byte? Status { get; set; }
        public int? Length { get; set; }
        [Unicode(false)]
        public string? FilePath { get; set; }
        public byte? Movie { get; set; }

        [InverseProperty("Content")]
        public virtual ICollection<ContentGenre> ContentGenres { get; set; }
        [InverseProperty("Content")]
        public virtual ICollection<Favourite> Favourites { get; set; }
        [InverseProperty("Content")]
        public virtual ICollection<Season> Seasons { get; set; }
    }
}
