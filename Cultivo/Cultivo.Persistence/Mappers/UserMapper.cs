using Cultivo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Persistence.Mappers
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("usuario");

            builder.HasKey(model => model.Id);

            builder.Property(model => model.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(model => model.Name)
                   .HasColumnName("nome")
                   .IsRequired();

            builder.Property(model => model.LastName)
                   .HasColumnName("sobrenome")
                   .IsRequired();

            builder.Property(model => model.Email)
                   .HasColumnName("email")
                   .IsRequired();

            builder.Property(model => model.Password)
                   .HasColumnName("senha")
                   .IsRequired();

            builder.Property(model => model.PhotoURL)
                   .HasColumnName("url_foto");
        }
    }
}
