
using System;

namespace CarDealer.Models
{
    public class Log
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Operation Operation { get; set; }
        public string ModifiedTable { get; set; }
        public DateTime DateModified { get; set; }

    }
    public enum Operation
    { Add,Edit,Delete }
}
