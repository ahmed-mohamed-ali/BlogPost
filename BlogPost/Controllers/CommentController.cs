using BlogPost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Controllers
{
    public class CommentController : Controller
    {
        private readonly BlogContext db;

        public CommentController(BlogContext _db)
        {
            this.db = _db;
        }
        // Display create form
        // GET: Comment/Create/postId
        [HttpGet]
        public IActionResult Create(int postid)
        {

            ViewBag.postId = postid;
            return View();
        }

        // Add new comment in database
        // POST: Post/Create
        [HttpPost]
        public IActionResult Create([Bind("CommentPublisherName,CommentContent,PostId")] Comment comment)
        {

            if (ModelState.IsValid)
            {
                db.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index","Post");
            }
            return View();

        }
        // get edit form
        // GET: Comment/Edit/id
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!CheckCommentExists(id))
            {
                return NotFound();
            }

            var comment = db.Comments.Find(id);

            return View(comment);
        }

        // update the database
        // POST: Comment/Edit/id
        [HttpPost]
        public IActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Post");
            }

            return View(comment);
        }
        //get information about comment
        //Details GET: Comment/Details/id
        [HttpGet]
        public IActionResult Details(int id)
        {
            //check id exist
            if (!CheckCommentExists(id))
            {
                return NotFound();
            }
            var comment = db.Comments.FirstOrDefault(ww => ww.CommentId == id);
            return View(comment);
        }
        //delete post
        //Delete GET : Post/Delete/id
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!CheckCommentExists(id))
            {
                return NotFound();
            }
            var comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index","Post");
        }
        private bool CheckCommentExists(int id)
        {
            return db.Comments.Any(e => e.CommentId == id);
        }
    }
}
