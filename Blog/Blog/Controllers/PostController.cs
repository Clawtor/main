using Blog.data;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blog.Controllers
{
    public class PostController : ApiController
    {
        private IPostRepository _postRepo;

        public PostController(IPostRepository repo)
        {
            _postRepo = repo;
        }

        public IEnumerable<Post> GetPosts()
        {
            return _postRepo.GetPosts();
        }

        public string Get()
        {
            return "HI";
        }
    }
}