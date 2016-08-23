using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP2013_Webs
{
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result
    {
        public Metadata __metadata { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class D
    {
        public IList<Result> results { get; set; }
    }

    public class Webs
    {
        public D d { get; set; }
    }

}
