using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HoangTrungThong_54.Models
{
    public class Encryption
    {
        public string PasswordEncryption(string pass)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pass.Trim(), "MD5");
        }
    }
}