namespace GolabalForum.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ForumModelDb : DbContext
    {
        public ForumModelDb()
            : base("name=MyForumModelDb")
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Topics)
                .WithRequired(e => e.Categories)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Topics>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Topics)
                .HasForeignKey(e => e.TopicId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Topics)
                .WithRequired(e => e.Auther)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);
        }
    }
}
