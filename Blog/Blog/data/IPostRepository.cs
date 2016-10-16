using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.data
{
	public interface IPostRepository
	{
        void SavePost(Post post);

        Post GetPost(int id);

        Post GetLatestPost();

        IEnumerable<Post> GetPosts();
    }
}