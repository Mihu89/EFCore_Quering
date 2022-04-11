using EF_Core_Quering.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;

namespace EF_Core_Quering.Migrations
{
    public partial class AddDefaultData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var db = new BlogDbContext();
            var admin = new User()
            {
                Name = "admin",
                DisplayName = "Administrator"
            };
            var manager = new User()
            {
                Name = "manager",
                DisplayName = "Manager"
            };

            db.Users.Add(admin);
            db.Users.Add(manager);
            var blog = new Blog
            {
                Name = "C# Starter",
                Url = "www.csharp-starter.com"
            };
            var blog1 = new Blog
            {
                Name = "EF Core Starter",
                Url = "www.efCore-starter.com"
            };

            db.Blogs.Add(blog);
            db.Blogs.Add(blog1);
            db.SaveChanges();
            var firstBlog = db.Blogs.FirstOrDefault( x=> x.Name == "C# Starter");
            var efBlog = db.Blogs.FirstOrDefault( x=> x.Name == "EF Core Starter");
            var post = new Post
            {
                Title = "Intro C#",
                Content = "<h1>Introduction to C# World</h1><p>First program</p>",
                Blog = firstBlog
            };
            var post1 = new Post
            {
                Title = "Data Types in C#",
                Content = "<h1>Types</h1><p>Ref and Value</p>",
                Blog = firstBlog
            };
            
            var post2 = new Post
            {
                Title = "Collections C#",
                Content = "<h1>Collections in C#</h1><p>LinkedList, IEnumerable, Lists, Stack,Queue, Dictionary etc</p>",
                Blog = firstBlog
            };
            var post3 = new Post
            {
                Title = "ORM and C#",
                Content = "<h1>Ado.Net Origin</h1><p>EFCore1.0</p>",
                Blog = efBlog
            };
            var post4 = new Post
            {
                Title = "Architecture of EF",
                Content = "<h1>Tables, Mappers</h1><p>Data Annotations</p>",
                Blog = firstBlog
            }; 
            var post5 = new Post
            {
                Title = "Fluent API",
                Content = "<h1>Fluent API</h1><p>Fluent API and Conventions</p>",
                Blog = firstBlog
            };
            db.Posts.Add(post);
            db.Posts.Add(post1);
            db.Posts.Add(post2);
            db.Posts.Add(post3);
            db.Posts.Add(post4);
            db.Posts.Add(post5);
            db.SaveChanges();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var db = new BlogDbContext();
            var admin = db.Users.FirstOrDefault(x => x.Name == "admin");
            var manager = db.Users.FirstOrDefault(x => x.Name == "manager");

            db.Users.Remove(admin);
            db.Users.Remove(manager);

            var post = db.Posts.FirstOrDefault(x => x.Title == "Intro C#");
            var post1 = db.Posts.FirstOrDefault(x => x.Title == "Data Types in C#");
            var post2 = db.Posts.FirstOrDefault(x => x.Title == "Collections C#");
            var post3 = db.Posts.FirstOrDefault(x => x.Title == "ORM and C#");
            var post4 = db.Posts.FirstOrDefault(x => x.Title == "Architecture of EF");
            var post5 = db.Posts.FirstOrDefault(x => x.Title == "Fluent API");

            db.Posts.Remove(post);
            db.Posts.Remove(post1);
            db.Posts.Remove(post2);
            db.Posts.Remove(post3);
            db.Posts.Remove(post4);
            db.Posts.Remove(post5);

            var blogCS = db.Blogs.FirstOrDefault(x => x.Name == "C# Starter");
            var blogEF = db.Blogs.FirstOrDefault(x => x.Name == "EF Core Starter");
            db.Blogs.Remove(blogCS);
            db.Blogs.Remove(blogEF);

            db.SaveChanges();
        }
    }
}
