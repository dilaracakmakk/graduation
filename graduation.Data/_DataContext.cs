using graduation.Data.infrastructure;
using Microsoft.EntityFrameworkCore;

namespace graduation.Data
{
    public class DataContext : DbContext
    {

        public DataContext(string connectionString) : base(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        {


        }

        public DbSet<Model.Author> Authors { get; set; }
        public DbSet<Model.Category> Categories { get; set; }
        public DbSet<Model.Comment> Comments { get; set; }
        public DbSet<Model.Content> Contents { get; set; }
        public DbSet<Model.ContentCategory> ContentCategories { get; set; }
        public DbSet<Model.ContentTag> ContentTags { get; set; }
        public DbSet<Model.Media> Medias { get; set; }
        public DbSet<Model.Tag> Tags { get; set; }
        public DbSet<Model.Setting> Setting { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Model.Author>(entity => entity.ToTable("authors"));
            builder.Entity<Model.Category>(entity => entity.ToTable("categories"));
            builder.Entity<Model.Comment>(entity => entity.ToTable("comments"));
            builder.Entity<Model.Content>(entity => entity.ToTable("contents"));
            builder.Entity<Model.ContentCategory>(entity => entity.ToTable("content_categories"));
            builder.Entity<Model.ContentTag>(entity => entity.ToTable("content_tags"));
            builder.Entity<Model.Media>(entity => entity.ToTable("medias"));
            builder.Entity<Model.Tag>(entity => entity.ToTable("tags"));
            builder.Entity<Model.Setting>(entity => entity.ToTable("setting"));

        }
    }
}
