using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAPI.Configurations
{
    public class TagConfigurations : IEntityTypeConfiguration<TagEntity>
    {
        public void Configure(EntityTypeBuilder<TagEntity> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasMany(s => s.PostTags)
                .WithOne(pt => pt.Tag)
                .HasForeignKey(pt => pt.TagId);


        }
    }
}
