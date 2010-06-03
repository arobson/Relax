using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Relax.Impl.Json
{
    public class BulkPersist
    {
        [JsonProperty(PropertyName = "all_or_nothing")]
        public bool AllOrNothing { get; set; }

        [JsonProperty(PropertyName = "non_atomic")]
        public bool NonAtomic { get; set; }

        [JsonProperty(PropertyName = "docs")]
        public object[] Documents { get; set; }

        public BulkPersist()
        {
        }

        public BulkPersist(IEnumerable<object> docs)
        {
            Documents = docs.ToArray();
        }

        public BulkPersist(bool allOrNothing, bool nonAtomic, IEnumerable<object> docs)
        {
            AllOrNothing = allOrNothing;
            NonAtomic = nonAtomic;
            Documents = docs.ToArray();
        }
    }
}