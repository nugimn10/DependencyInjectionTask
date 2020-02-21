using System;
using  DependencyInjection;

namespace DependencyInjection.Models
{
    public class Member
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email {get; set;}
        public string full_name {get; set;}
        public int popularity { get; set; }
    
    }
    
}