using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;

namespace Blog.data
{
	public class PostRepository : IPostRepository
	{
        private readonly Context _context;

        public PostRepository(Context context)
        {
            _context = context;
        }

        public Post GetLatestPost()
        {
            return _context.Posts.OrderByDescending(p => p.Created).FirstOrDefault();
        }

        public Post GetPost(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void SavePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetPosts()
        {
            return new List<Post>()
            {
                new Post()
                {
                    Title = "Test",
                    Content = "TEST CONTENT"
                }
            };
            return _context.Posts;
        }
    }
}