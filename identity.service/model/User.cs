using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace identity.service.model
{
    internal class User
    {
        public int Id { get; set; }
        public string FirtName { get; set; }
        public string SecondName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
