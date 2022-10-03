using ForumAppExercise.Data;
using ForumAppExercise.Data.Models;
using ForumAppExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ForumAppExercise.Controllers
{
    public class PostsController : Controller
    {
        private readonly ForumAppDbContext dbContext;
        public PostsController(ForumAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var posts = dbContext
                .Posts
                .Select(x => new PostViewModel 
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content
                })
                .ToList();

            return View(posts);
        }
        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PostFormModel model) 
        {
            var post = new Post() 
            {
                Title = model.Title,
                Content = model.Content
            };

            this.dbContext.Add(post);
            this.dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var post = dbContext.Posts.Find(id);

            return View(new PostFormModel 
            {
                Title = post.Title,
                Content = post.Content
            });
        }
        [HttpPost]
        public IActionResult Edit(int id, PostFormModel model) 
        {
            var post = dbContext.Posts.Find(id);
            if (post != null) 
            {
                post.Title = model.Title;
                post.Content = model.Content;
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
