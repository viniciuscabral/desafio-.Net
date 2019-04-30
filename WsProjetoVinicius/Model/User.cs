using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WsTestes.Model
{
    public class User 
    {
        [JsonIgnore]
        public int Id { get; set; }      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        public string Password { get; set; }
        public string Created_at { get; set; } = DateTime.Now.ToString();
        public string Autentication { get; set; } =  Guid.NewGuid().ToString();
        public string Last_login { get; set; } 
        public IEnumerable<Phone> Phones { get; set; }
    }
}
