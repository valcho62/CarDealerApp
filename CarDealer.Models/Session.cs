
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Session
    {
        [Key]
        public string SessionId { get; set; }

        public bool IsActive { get; set; }
        public virtual User User { get; set; }

    }
}
