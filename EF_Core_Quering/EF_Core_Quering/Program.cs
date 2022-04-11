﻿using EF_Core_Quering.Entities;
using System;
using System.Linq;

namespace EF_Core_Quering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SimpleQuering();

            Console.WriteLine("End");
            Console.ReadLine();
        }

        private static void SimpleQuering()
        {
            // extract all posts that contains C#
            var db = new BlogDbContext();

            var posts = db.Posts.Where(x => x.Title.Contains("C#") || x.Content.Contains("C#"));
            foreach (var post in posts)
            {
                Console.WriteLine($"PostID:{post.Id} {post.Title} {post.Content}, BlogID: {post.Blog.Id}");
            }
        }
    }
}
