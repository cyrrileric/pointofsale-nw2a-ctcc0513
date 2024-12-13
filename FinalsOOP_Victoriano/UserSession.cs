using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalsOOP_Victoriano
{
    public static class UserSession
    {
        public static string Username { get; set; }
        public static string LoginType { get; set; }

        public static void Clear()
        {
            Username = null;
            LoginType = null;
        }
    }
}
