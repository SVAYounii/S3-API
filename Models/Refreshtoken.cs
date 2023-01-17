using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace S3_Api_indi.Models
{
    [Table("Refreshtoken")]
    public partial class Refreshtoken
    {
        [Key]
        [Column("token_id")]
        public int TokenId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("token")]
        [StringLength(200)]
        [Unicode(false)]
        public string Token { get; set; } = null!;
        [Column("expiry_date", TypeName = "datetime")]
        public DateTime ExpiryDate { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Refreshtokens")]
        public virtual User User { get; set; } = null!;
    }
}
