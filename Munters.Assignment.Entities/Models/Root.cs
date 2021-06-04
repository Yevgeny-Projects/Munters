using System.Collections.Generic;

namespace Munters.Assignment.Entities
{
    public class Root
    {
        public List<Datum> data { get; set; }
        public Pagination pagination { get; set; }
        public Meta meta { get; set; }
    }
}