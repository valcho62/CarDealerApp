
using System.Collections.Generic;

namespace CarDealer.Models
{
    public class User
    {
        public User()
        {
            this.Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}
