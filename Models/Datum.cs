using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteFinder.Models
{
    public class Datum
    {
        public string _id { get; set; }
        public string quoteText { get; set; }
        public string quoteAuthor { get; set; }
        public string quoteGenre { get; set; }
        public int __v { get; set; }
    }
}
