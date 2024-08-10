using Friends.Core.Domain.Common;
using Friends.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Friends.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using Friends.Core.Application.ViewModels.User;

namespace Friends.Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellation = new CancellationToken())
        {
            foreach( var entry in ChangeTracker.Entries<Auditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreadedBy = "Default";
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastUpdatedBy = "Default";
                        entry.Entity.LastUpdatedDate = DateTime.Now;
                        break;

                }
            }
            return base.SaveChangesAsync(cancellation);
        }



        protected override void OnModelCreating(ModelBuilder model)
        {

            model.Entity<Post>().ToTable("Posts");
            model.Entity<Reply>().ToTable("Replies");
            model.Entity<Comment>().ToTable("Comments");
            model.Entity<Friendship>().ToTable("Friends");
            model.Entity<User>().ToTable("Users");

            #region Keys

            model.Entity<User>().HasKey(e => e.Id);
            model.Entity<Post>().HasKey(e => e.Id);
            model.Entity<Comment>().HasKey(e => e.Id);
            model.Entity<Reply>().HasKey(e => e.Id);
            model.Entity<Friendship>().HasKey(e => e.Id);

            #endregion

            #region Relations

            model.Entity<User>()
                .HasMany<Post>(user => user.Posts)
                .WithOne(posts => posts.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            model.Entity<User>()
                .HasMany<Comment>()
                .WithOne(c => c.User)
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            model.Entity<User>()
                .HasMany<Reply>()
                .WithOne(reply => reply.User)
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            model.Entity<User>()
                .HasMany<Friendship>(u => u.Friendships)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.User1)
                .OnDelete(DeleteBehavior.Cascade);

            model.Entity<Post>()
                .HasMany<Comment>(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            model.Entity<Comment>()
                .HasMany<Reply>(c => c.Replys)
                .WithOne(r => r.Comment)
                .HasForeignKey(r => r.CommentId)
                .OnDelete(DeleteBehavior.Cascade);
            
                

            #endregion

            #region User

            model.Entity<User>()
                .Property(e => e.Id)
                .IsRequired();

            model.Entity<User>()
                .Property(e => e.Name)
                .IsRequired();
            model.Entity<User>()
                .Property(e => e.LastName)
                .IsRequired();
            model.Entity<User>()
                .HasIndex(e => e.Email)
                .IsUnique();
            model.Entity<User>()
                .Property(e => e.Phone)
                .IsRequired();
            model.Entity<User>()
                .Property(e => e.Password)
                .IsRequired();
            model.Entity<User>()
                .HasIndex(e => e.UserName)
                .IsUnique();

            #endregion

            #region Post

            model.Entity<Post>()
                .Property(p => p.Type)
                .IsRequired();
            model.Entity<Post>()
                .Property(p => p.Content)
                .IsRequired();

            #endregion

            #region Comment

            model.Entity<Comment>()
                .Property(c => c.Content)
                .IsRequired();

            #endregion

            #region Friends
            model.Entity<Friendship>()
                .Property(f => f.Id)
                .IsRequired();

            #endregion
        }
    }
}
