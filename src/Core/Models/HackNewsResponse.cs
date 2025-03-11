using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class HackNewsResponse
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Uri { get; set; }
        public string? PostedBy { get; set; }
        public DateTime? Time { get; set; }
        public int Score { get; set; }
        public int CommentCount { get; set; }
    }
}
