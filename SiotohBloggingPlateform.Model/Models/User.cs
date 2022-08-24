using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiotohBloggingPlateform.Model.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage ="Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name should be between 2 to 50 characters")]
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
