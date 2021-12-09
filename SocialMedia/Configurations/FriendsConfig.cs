using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Configurations
{
    public class FriendsConfig : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder
                .HasOne(f => f.Sender)
                .WithMany(u => u.FriendsSentRequests)
                .HasForeignKey(f => f.SenderId);

            builder
                .HasOne(f => f.Receiver)
                .WithMany(u => u.FriendsReceivedRequests)
                .HasForeignKey(f => f.ReceiverId);
        }
    }
}
