using EF_Core_Quering.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace EF_Core_Quering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //SimpleQuering();

            FasterSelect();
            Console.WriteLine("End");
            Console.ReadLine();
        }

        private static void FasterSelect()
        {
            var timer = new Stopwatch();
            var db = new BlogDbContext();
            timer.Start();
            var post = db.Posts.ToList();
            timer.Stop();
            Console.WriteLine("Simple: " + timer.ElapsedTicks);
            timer.Reset();
            timer.Start();
            post = db.Posts
                    .AsNoTracking()
                    .ToList();
            timer.Stop();
            Console.WriteLine("Fast " + timer.ElapsedTicks);
            timer.Reset();

            timer.Start();
            var post1 = db.Posts
                    .ToArray();
            timer.Stop();
            Console.WriteLine("Fast 2 " + timer.ElapsedTicks);

            timer.Reset();

            timer.Start();
            post1 = db.Posts.AsNoTracking()
                    .ToArray();
            timer.Stop();
            Console.WriteLine("Fast 3 " + timer.ElapsedTicks);

            // Execute raw sql code
            var posts = db.Posts.FromSqlRaw("Select * from dbo.Posts WITH (NOLOCK)").ToArray();
            Console.WriteLine(posts.Length);
        }

        private static void SimpleQuering()
        {
            // extract all posts that contains C#
            var db = new BlogDbContext();

           // db.Posts.Add(new Post() { Title = "Fresh", Content = " Fresh Post C#", Blog = db.Blogs.FirstOrDefault() });
            var posts = db.Posts
                        .Include(x => x.Blog)
                        .Where(x => x.Title.Contains("C#") || x.Content.Contains("C#"))
                        .ToArray();

            // remove first element from the list
            db.Posts.Remove(posts[0]);
            
            foreach (var post in posts)
            {
                Console.WriteLine($"PostID:{post.Id} {post.Title} {post.Content}, BlogID: {post.Blog.Id} PostState: {db.Entry(post).State}");
            }

            var manager = db.Users.SingleOrDefault(x => x.Name=="manager");
            Console.WriteLine($"Manager USER: {manager.Id} {manager.Name} {manager.DisplayName}");

            Console.WriteLine(new string('_',20));
            // Extract all posts that contsins EF from EF Blog.

            var efBlog = db.Blogs.FirstOrDefault(x => x.Id == 2);
            //var efPosts = db.Entry(efBlog)
            //    .Collection(p => p.Posts)
            //    .Query()
            //    .Where(x => x.Title.Contains("EF"))
            //    .ToList();
            var efPosts = db.Posts.Include(x => x.Blog)
                .Where(x => x.Title.Contains("EF"))
                .ToList();

            foreach (var post in efPosts)
            {
                Console.WriteLine($"PostID:{post.Id} {post.Title} {post.Content}, Blog_Name: {post.Blog.Name}");
            }

        }
    }
}
