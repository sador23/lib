using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        [StringLength(60)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(60)]
        public string Lastname { get; set; }

        [Required]
        public string Status { get; set; }

        public int BookId { get; set; }

        public List<Book> Books { get; set; }
    }
}
