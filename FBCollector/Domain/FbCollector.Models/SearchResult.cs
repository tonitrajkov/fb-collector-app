using System.Collections.Generic;

namespace FbCollector.Models
{
    public class SearchResult<T> where T : class
    {
        public int TotalItems { get; set; }

        public IList<T> Items { get; set; }
    }
}
