using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet.Classes
{
    public class CurrentUser
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        private static CurrentUser _instance;

        public static CurrentUser Instance => _instance ?? (_instance = new CurrentUser());

        private CurrentUser() { }
    }
}
