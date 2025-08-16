using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using stepic.Models;

namespace stepic.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // 1. Настройка таблицы
        builder.ToTable("users");

        // 2. Настройка первичного ключа
        builder.HasKey(u => u.Id);

        // 3. Настройка свойств
        builder.Property(u => u.Id)
            .HasColumnName("id");

        builder.Property(u => u.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(50);

        builder.Property(u => u.Details)
            .HasColumnName("details")
            .HasMaxLength(50);

        builder.Property(u => u.JoinDate)
            .HasColumnName("join_date");

        builder.Property(u => u.Avatar)
            .HasColumnName("avatar");

        builder.Property(u => u.IsActive)
            .HasColumnName("is_active");

        builder.Property(u => u.Knowledge)
            .HasColumnName("knowledge");

        builder.Property(u => u.Reputation)
            .HasColumnName("reputation");

        builder.Property(u => u.FollowersCount)
            .HasColumnName("followers_count");

        builder.Property(u => u.DaysWithoutBreak)
            .HasColumnName("days_without_break");

        builder.Property(u => u.DaysWithoutBreakMax)
            .HasColumnName("days_without_break_max");

        builder.Property(u => u.SolvedTasks)
            .HasColumnName("solved_tasks");
    }
}
