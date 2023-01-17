using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace S3_Api_indi.Models
{
    [Table("Season")]
    public partial class Season
    {
        public Season()
        {
            Episodes = new HashSet<Episode>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("ContentID")]
        public int? ContentId { get; set; }
        [Unicode(false)]
        public string? Title { get; set; }
        [Unicode(false)]
        public string? Description { get; set; }
        public int? SeasonNumber { get; set; }
        [Unicode(false)]
        public string? ImgUrl { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public byte? Status { get; set; }

        [ForeignKey("ContentId")]
        [InverseProperty("Seasons")]
        public virtual Content? Content { get; set; }
        [InverseProperty("Season")]
        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
