using Microsoft.EntityFrameworkCore;
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
    public class UserStateConfiguration : IEntityTypeConfiguration<UserState>
    {
        public void Configure(EntityTypeBuilder<UserState> builder)
        {
            builder.ToTable("user_state");
            builder.HasKey(state => state.Id);
            builder.Property(state => state.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .HasJsonPropertyName("id");
            builder.Property(state => state.UserStateCode)
                .HasConversion(
                    value => value.ToString(),
                    value => (UserStateCode)Enum.Parse(typeof(UserStateCode), value)
                 )
                .HasColumnName("code")
                .HasJsonPropertyName("code")
                .IsRequired();
            builder.Property(state => state.Description)
                .HasColumnName("description")
                .HasJsonPropertyName("description")
                .IsRequired();
        }
    }
}
