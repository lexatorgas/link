using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public class Url
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public int TransitionsCount { get; set; }
        public string creationData { get; set; }
    }
}
