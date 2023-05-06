using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Domain.Entities;

namespace VKInternshipTask.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .HasJsonPropertyName("id");
            builder.Property(user => user.Login)
                .HasColumnName("login")
                .HasJsonPropertyName("login")
                .IsRequired();
            builder.Property(user => user.Password)
                .HasColumnName("password")
                .HasJsonPropertyName("password")
                .IsRequired();
            builder.Property(user => user.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date")
                .HasJsonPropertyName("created_date")
                .HasDefaultValue(DateTime.Now)
                .IsRequired();
            builder.Property(user => user.UserGroupId)
                .HasColumnName("user_group_id")
                .HasJsonPropertyName("user_group_id")
                .IsRequired();
            builder.Property(user => user.UserStateId)
                .HasColumnName("user_state_id")
                .HasJsonPropertyName("user_state_id")
                .IsRequired();
            builder.HasOne(user => user.UserGroup)
                .WithMany(group => group.Users)
                .HasForeignKey(user => user.UserGroupId);
            builder.HasOne(user => user.UserState)
                .WithMany(state => state.Users)
                .HasForeignKey(user => user.UserStateId);
        }
    }
}
