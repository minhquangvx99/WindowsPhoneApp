using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHSM_PhoneApp.Entities
{
    class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string system_name { get; set; }
        public string manufacture { get; set; }
        public string device_model { get; set; }
        public string device_token { get; set; }
    }
}
