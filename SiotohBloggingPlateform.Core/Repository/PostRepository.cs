using SiotohBloggingPlateform.Core.Interfaces;
using SiotohBloggingPlateform.DataAccess.Data;
using SiotohBloggingPlateform.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiotohBloggingPlateform.Core.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts;
        }

        public Post GetPostById(int postId)
        {
            return _context.Posts.FirstOrDefault(p => p.Id.Equals(postId));
        }

        public bool CreatePost(Post post)
        {
            _context.Posts.Add(post);
            return _context.SaveChanges() > 0;
        }
    }
}
