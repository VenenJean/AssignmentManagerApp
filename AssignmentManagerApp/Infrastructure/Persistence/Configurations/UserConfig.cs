using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfig : IEntityTypeConfiguration<User> {
  public void Configure(EntityTypeBuilder<User> builder) {
    builder
      .ToTable("Users")
      .HasKey(user => user.Id);

    builder.HasIndex(user => user.Name).IsUnique();
    builder.Property(user => user.Name).HasMaxLength(40);
    
    builder.Property(user => user.Password).HasMaxLength(100);
    
    builder.HasMany(user => user.Assignments);
  }
}