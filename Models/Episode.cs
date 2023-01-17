using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace S3_Api_indi.Models
{
    [Table("Episode")]
    public partial class Episode
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("SeasonID")]
        public int? SeasonId { get; set; }
        [Unicode(false)]
        public string? Title { get; set; }
        [Unicode(false)]
        public string? Description { get; set; }
        [Unicode(false)]
        public string? ImgUrl { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public byte? Status { get; set; }

        [ForeignKey("SeasonId")]
        [InverseProperty("Episodes")]
        public virtual Season? Season { get; set; }
    }
}
