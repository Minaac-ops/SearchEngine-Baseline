using System.Collections.Generic;

namespace Common
{
    public class SearchResult
    {
        public double ElapsedMilliseconds { get; set; }
        public List<string> IgnoredTerms { get; set; } = new List<string>();
        public List<Document> Documents { get; set; } = new List<Document>();
    }
}