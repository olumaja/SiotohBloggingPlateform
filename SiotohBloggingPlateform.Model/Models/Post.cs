using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiotohBloggingPlateform.Model.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Title is required")]
        [StringLength(50, ErrorMessage ="Title should not exceed 50 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Description is required")]
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
