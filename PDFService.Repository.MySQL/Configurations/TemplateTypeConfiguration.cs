using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDFService.Entities;

namespace PDFService.Repository.MySQL.Configurations
{
    sealed class TemplateTypeConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable("templates");
            builder.HasKey(t => t.ID);
            builder.Property(t => t.ClientID).HasColumnName("clientID").IsRequired();
            builder.Property(t => t.Title).HasColumnName("title").IsRequired();
            builder.Property(t => t.Description).HasColumnName("desc");
            builder.Property(t => t.Page).HasColumnName("page");
            builder.Property(t => t.Content).HasColumnName("content").HasColumnType("longtext");
            builder.Property(t => t.CreatedAt).HasColumnName("createdAt").HasColumnType("datetime").HasDefaultValueSql("now()");
            builder.Property(t => t.UpdatedAt).HasColumnName("updatedAt").HasColumnType("datetime").HasDefaultValueSql("now()");
            //indexes
            builder.HasIndex(t => t.ClientID);
        }
    }
}
