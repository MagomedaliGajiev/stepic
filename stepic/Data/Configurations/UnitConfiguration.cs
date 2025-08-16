﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using stepic.Models;

namespace stepic.Data.Configurations;

public class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.ToTable("units");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("id");

        builder.Property(u => u.CourseId)
            .HasColumnName("course_id")
            .IsRequired();

        builder.Property(u => u.Title)
            .HasColumnName("title")
            .IsRequired(false);

        builder.HasOne(u => u.Course)
            .WithMany(c => c.Units)
            .HasForeignKey(u => u.CourseId);
    }
}
