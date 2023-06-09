﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VKInternshipTask.Domain.Entities;
using VKInternshipTask.Domain.Enums;

namespace VKInternshipTask.Persistence.EntityConfigurations
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("user_group");
            builder.HasKey(group => group.Id);
            builder.Property(group => group.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .HasJsonPropertyName("id");
            builder.Property(group => group.Code)
                .HasConversion(
                    value => value.ToString(),
                    value => (UserGroupCode)Enum.Parse(typeof(UserGroupCode), value)
                 )
                .HasColumnName("code")
                .HasJsonPropertyName("code")
                .IsRequired();
            builder.HasIndex(group => group.Code).IsUnique();
            builder.Property(group => group.Description)
                .HasColumnName("description")
                .HasJsonPropertyName("description")
                .IsRequired();
            builder.HasData(
                new UserGroup()
                {
                    Id = 1,
                    Code = UserGroupCode.Admin,
                    Description = "User with administrative rights"
                },
                new UserGroup()
                {
                    Id = 2,
                    Code = UserGroupCode.User,
                    Description = "User with common rights"
                }
            );
        }
    }
}
