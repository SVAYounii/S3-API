using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace S3_Api_indi.Models
{
    public partial class User
    {
        public User()
        {
            Favourites = new HashSet<Favourite>();
            Refreshtokens = new HashSet<Refreshtoken>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Username { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        public byte Status { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Favourite> Favourites { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Refreshtoken> Refreshtokens { get; set; }
    }
}
