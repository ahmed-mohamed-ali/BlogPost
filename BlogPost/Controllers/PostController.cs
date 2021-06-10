using BlogPost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogContext db;

        public PostController(BlogContext _db)
        {
            this.db = _db;
        }

        //get all posts
        // GET: Posts
        public IActionResult Index()
        {
            var posts = db.Posts.Include(p => p.comments).ToList();
            return View(posts);
        }

        // Display create form
        // GET: Post/Create
        [HttpGet]
        public IActionResult Create()
        {
          return View();
        }


        // Add new post in database
        // POST: Post/Create
        [HttpPost]
        public IActionResult Create([Bind("PublisherName,PostContent")] Post post)
        {

            if (ModelState.IsValid)
            {
                db.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);

        }

        // get edit form
        // GET: Post/Edit/id
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if ( !CheckPostExists(id))
            {
                return NotFound();
            }

            var post =  db.Posts.Find(id);
                                  
            return View(post);
        }

        // update the database
        // POST: Post/Edit/id
        [HttpPost]
        public IActionResult Edit( Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        //get information about post
        //Details GET: Post/Details/id
        [HttpGet]
        public IActionResult Details(int id)
        {
            //check id exist
            if(!CheckPostExists(id))
            {
                return NotFound();
            }
            var post = db.Posts.FirstOrDefault(ww => ww.PostId == id);
            return View(post);
        }
        //delete post
        //Delete GET : Post/Delete/id
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!CheckPostExists(id))
            {
                return NotFound();
            }
            var post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool CheckPostExists(int id)
        {
            return db.Posts.Any(e => e.PostId == id);
        }



    }
}
