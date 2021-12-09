using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.Models
{
    public class User : IdentityUser
    {
        public DateTime DateRegistered { get; set; }
        public DateTime Birthday { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<React> Reacts { get; set; }
        public ICollection<Friend> FriendsSentRequests { get; set; }
        public ICollection<Friend> FriendsReceivedRequests { get; set; }
    }
}
