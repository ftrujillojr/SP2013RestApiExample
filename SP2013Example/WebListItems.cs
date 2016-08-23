using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP2013_WebListItems
{
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string etag { get; set; }
        public string type { get; set; }
    }

    public class Deferred
    {
        public string uri { get; set; }
    }

    public class FirstUniqueAncestorSecurableObject
    {
        public Deferred __deferred { get; set; }
    }

    public class RoleAssignments
    {
        public Deferred __deferred { get; set; }
    }

    public class AttachmentFiles
    {
        public Deferred __deferred { get; set; }
    }

    public class ContentType
    {
        public Deferred __deferred { get; set; }
    }

    public class FieldValuesAsHtml
    {
        public Deferred __deferred { get; set; }
    }

    public class FieldValuesAsText
    {
        public Deferred __deferred { get; set; }
    }

    public class FieldValuesForEdit
    {
        public Deferred __deferred { get; set; }
    }

    public class File
    {
        public Deferred __deferred { get; set; }
        public string Name { get; set; }
        public string ServerRelativeUrl { get; set; }
        public string Title { get; set; }
    }

    public class Folder
    {
        public Deferred __deferred { get; set; }
    }

    public class ParentList
    {
        public Deferred __deferred { get; set; }
    }

    public class ODataDlcDocIdUrl
    {
        public Metadata __metadata { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }

    public class EDC_MemoryArea
    {
        public Metadata __metadata { get; set; }
        public string Label {get; set;}
        public string TermGuid {get; set;}
        public int WssId {get; set;}

        public EDC_MemoryArea()
        {
            this.Label = "N/A";
            this.TermGuid = "N/A";
            this.WssId = -1;
        }
    }
    public class EDC_MemoryDocType
    {
        public Metadata __metadata { get; set; }
        public string Label {get; set;}
        public string TermGuid {get; set;}
        public int WssId {get; set;}

        public EDC_MemoryDocType()
        {
            this.Label = "N/A";
            this.TermGuid = "N/A";
            this.WssId = -1;
        }
    }
    public class EDC_MicronProduct
    {
        public Metadata __metadata { get; set; }
        public string Label {get; set;}
        public string TermGuid {get; set;}
        public int WssId {get; set;}

        public EDC_MicronProduct()
        {
            this.Label = "N/A";
            this.TermGuid = "N/A";
            this.WssId = -1;
        }
    }

    public class Result
    {
        public Metadata __metadata { get; set; }
        public FirstUniqueAncestorSecurableObject FirstUniqueAncestorSecurableObject { get; set; }
        public RoleAssignments RoleAssignments { get; set; }
        public AttachmentFiles AttachmentFiles { get; set; }
        public ContentType ContentType { get; set; }
        public FieldValuesAsHtml FieldValuesAsHtml { get; set; }
        public FieldValuesAsText FieldValuesAsText { get; set; }
        public FieldValuesForEdit FieldValuesForEdit { get; set; }
        public File File { get; set; }
        public Folder Folder { get; set; }
        public ParentList ParentList { get; set; }
        public int FileSystemObjectType { get; set; }
        public int Id { get; set; }
        public string ContentTypeId { get; set; }
        public string Title { get; set; }
        public string OData__dlc_DocId { get; set; }
        public ODataDlcDocIdUrl OData__dlc_DocIdUrl { get; set; }
        public string r_object_id { get; set; }
        public string i_chronicle_id { get; set; }
        public string r_version_label { get; set; }
        public string DocType { get; set; }
        public object object_name { get; set; }
        public string MTKeywords { get; set; }
        public object WorkflowRouteId { get; set; }
        public object WorkflowNotificationId { get; set; }
        public string WorkflowType { get; set; }
        public object Description { get; set; }
        public object Name { get; set; }
        public object EDC_AdminArea { get; set; }
        public object EDC_Category { get; set; }
        public object EDC_DateTime { get; set; }
        public object EDC_Facility { get; set; }
        public object EDC_System { get; set; }
        public object EDC_Project { get; set; }
        public object EDC_Status { get; set; }
        public string MicronRecord { get; set; }
        public string DocumentComment { get; set; }
        public DateTime? OData__dlc_ExpireDate { get; set; }
        public object Micron_x0020_Approval_x0020_Workflow { get; set; }
        public object EDC_MfgArea { get; set; }
        public object EDC_ControlPlanDocument { get; set; }
        public object EDC_MfgDepartment { get; set; }
        public object EDC_DesignID { get; set; }
        public object EDC_DocumentType { get; set; }
        public object EDC_EquipmentTechnology { get; set; }
        public object EDC_FabModule { get; set; }
        public object EDC_MfgFacility { get; set; }
        public object EDC_ManufacturingGroup { get; set; }
        public object EDC_MfgProcess { get; set; }
        public object EDC_MfgStatus { get; set; }
        public EDC_MemoryArea EDC_MemoryArea { get; set; }
        public EDC_MicronProduct EDC_MicronProduct { get; set; }
        public EDC_MemoryDocType EDC_MemoryDocType { get; set; }
        public object EDC_MicronPrdRequirement { get; set; }
        public object Approval { get; set; }
        public object EmFrom { get; set; }
        public object EmSubject { get; set; }
        public object EmReceivedDate { get; set; }
        public object EmCategory { get; set; }
        public object EmAttachment { get; set; }
        public object EmConversationID { get; set; }
        public object EmFolder { get; set; }
        public int ID { get; set; }
        public DateTime? Created { get; set; }
        public int AuthorId { get; set; }
        public DateTime? Modified { get; set; }
        public int EditorId { get; set; }
        public object OData__CopySource { get; set; }
        public object CheckoutUserId { get; set; }
        public string OData__UIVersionString { get; set; }
        public string GUID { get; set; }
    }

    public class D
    {
        public IList<Result> results { get; set; }
        public string __next { get; set; }
    }

    public class WebListItems
    {
        public D d { get; set; }
    }
}
