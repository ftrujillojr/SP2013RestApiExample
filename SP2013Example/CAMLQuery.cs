using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SP2013_CAMLQuery
{
    public class Metadata
    {
        public string type { get; set; }
    }

    public class Query
    {
        public Metadata __metadata { get; set; }
        public string ViewXml { get; set; }
    }

    public class CAMLQuery
    {
        public Query query { get; set; }
    }
}
