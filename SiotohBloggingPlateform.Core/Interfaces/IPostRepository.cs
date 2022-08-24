using SiotohBloggingPlateform.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiotohBloggingPlateform.Core.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
        Post GetPostById(int postId);
        bool CreatePost(Post post);
    }
}
