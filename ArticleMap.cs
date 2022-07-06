using ArtBloger.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtBloger.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Title).HasMaxLength(100);
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Content).HasColumnType("NVARCHAR(MAX)");
            builder.Property(p => p.Content).IsRequired();
            builder.Property(p => p.SeoAuthor).IsRequired();
            builder.Property(p => p.SeoAuthor).HasMaxLength(100);
            builder.Property(p => p.CreatedTime).IsRequired();
            builder.Property(p => p.SeoTags).IsRequired();
            builder.Property(p => p.SeoTags).HasMaxLength(70);
            builder.Property(p => p.SeoDescription).IsRequired();
            builder.Property(p => p.SeoDescription).HasMaxLength(150);
            builder.Property(p => p.ViewsCount).IsRequired();
            builder.Property(p => p.CommentCount).IsRequired();
            builder.Property(p => p.Image).IsRequired();
            builder.Property(p => p.Image).HasMaxLength(250);
            builder.HasOne<Category>(p => p.Category).WithMany(p => p.Articles).HasForeignKey(p => p.CategoryId);//bire çok ilişki
            builder.HasOne<User>(p => p.User).WithMany(p => p.Articles).HasForeignKey(p => p.UserId);//bire çok ilişki
            builder.ToTable("Articles");
        }
    }
}
