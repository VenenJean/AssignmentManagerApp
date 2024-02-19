using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AssignmentConfig : IEntityTypeConfiguration<Assignment> {
  public void Configure(EntityTypeBuilder<Assignment> builder) {
    builder
      .ToTable("Assignments")
      .HasKey(assignment => assignment.Id);

    builder.HasIndex(assignment => assignment.Name).IsUnique();
    builder.Property(assignment => assignment.Name).HasMaxLength(500);
    
    builder.HasOne(assignment => assignment.User);
  }
}