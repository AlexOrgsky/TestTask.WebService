using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.WebService.ViewModels
{
    public class UserCredentials
    {
        public UserCredentials(string username, string email)
        {
            this.username = username;
            this.email = email;
        }
        public string username { get; set; }
        public string email { get; set; }
    }
}
