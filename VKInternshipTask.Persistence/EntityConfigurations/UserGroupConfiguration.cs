﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            builder.Property(group => group.UserGroupCode)
                .HasConversion(
                    value => value.ToString(),
                    value => (UserGroupCode)Enum.Parse(typeof(UserGroupCode), value)
                 )
                .HasColumnName("code")
                .HasJsonPropertyName("code")
                .IsRequired();
            builder.Property(group => group.Description)
                .HasColumnName("description")
                .HasJsonPropertyName("description")
                .IsRequired();
        }
    }
}
