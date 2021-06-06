using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoangTrungThong_54.Models
{
    public class Account
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}