using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsTestes.Model;

namespace WsProjetoVinicius.Model
{
    public class UserDto
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }        
        public string Created_at { get; set; } = DateTime.Now.ToString();        
        public string Last_login { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
    }
}
