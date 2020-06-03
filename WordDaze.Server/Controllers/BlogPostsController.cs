using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WordDaze.Server.Services;
using WordDaze.Shared;

namespace WordDaze.Server.Controllers
{
    public class BlogPostsController : ControllerBase
    {
        private readonly BlogPostService _blogPostService;

        public BlogPostsController(BlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet(Urls.BlogPosts)]
        public IActionResult GetBlogPosts()
        {
            return Ok(_blogPostService.GetBlogPosts());
        }

        [HttpGet(Urls.BlogPost)]
        public IActionResult GetBlogPostById(int id)
        {
            var blogPost = _blogPostService.GetBlogPost(id);

            if (blogPost == null)
                return NotFound();

            return Ok(blogPost);
        }
        
        [HttpPost(Urls.AddBlogPost)]
        public IActionResult AddBlogPost([FromBody]BlogPost newBlogPost)
        {
            var savedBlogPost = _blogPostService.AddBlogPost(newBlogPost);

            return Created(new Uri(Urls.BlogPost.Replace("{id}", savedBlogPost.Id.ToString()), UriKind.Relative), savedBlogPost);
        }
    }
}