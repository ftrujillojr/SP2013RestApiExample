using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP2013_ContextInfo
{
    public class Metadata
    {
        public string type { get; set; }
    }

    public class SupportedSchemaVersions
    {
        public Metadata __metadata { get; set; }
        public IList<string> results { get; set; }
    }

    public class GetContextWebInformation
    {
        public Metadata __metadata { get; set; }
        public int FormDigestTimeoutSeconds { get; set; }
        public string FormDigestValue { get; set; }
        public string LibraryVersion { get; set; }
        public string SiteFullUrl { get; set; }
        public SupportedSchemaVersions SupportedSchemaVersions { get; set; }
        public string WebFullUrl { get; set; }
    }

    public class D
    {
        public GetContextWebInformation GetContextWebInformation { get; set; }
    }

    public class ContextInfo
    {
        public D d { get; set; }
    }

}
