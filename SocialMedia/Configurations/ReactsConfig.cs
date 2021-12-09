using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Configurations
{
    public class ReactsConfig : IEntityTypeConfiguration<React>
    {
        public void Configure(EntityTypeBuilder<React> builder)
        {
            builder
                .HasOne(r => r.User)
                .WithMany(u => u.Reacts)
                .HasForeignKey(r => r.UserId);
            builder
                .HasOne(r => r.Post)
                .WithMany(p => p.Reacts)
                .HasForeignKey(r => r.PostId);
        }
    }
}
