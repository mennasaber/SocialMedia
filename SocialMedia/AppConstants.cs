using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia
{
    public abstract class AppConstants
    {
        public const int SuccessfulStatus = 200;
        public const int CreatedStatus = 201;
        public const int BadRequestStatus = 400;
        public const int NotFoundStatus = 404;
        public const string UserExist = "User already exist";
        public const string ReactExist = "User already reacted this post";
        public const string InvalidUser = "Invalid email or password";
        public const string NotFoundMessage = "Not found!";
    }
}
