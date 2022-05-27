using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandsiteMobile.Models
{
    public class UserModel : User
    {
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
    }
}
