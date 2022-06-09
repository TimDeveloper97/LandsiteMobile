using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandsiteMobile.Models
{
    public class UserModel : User
    {
        public int Age { get; set; }
        public int Gender { get; set; }
        public int Occupation { get; set; }
    }
}
