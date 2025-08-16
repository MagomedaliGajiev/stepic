﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using stepic.Models;

namespace stepic.Data.Configurations;

public class SocialProviderConfiguration : IEntityTypeConfiguration<SocialProvider>
{
    public void Configure(EntityTypeBuilder<SocialProvider> builder)
    {
        builder.ToTable("social_providers");

        builder.HasKey(sp => sp.Id);

        builder.Property(sp => sp.Id)
            .HasColumnName("id");

        builder.Property(sp => sp.Name)
            .HasColumnName("name");

        builder.Property(sp => sp.LogoUrl)
            .HasColumnName("logo_url");
    }
}
