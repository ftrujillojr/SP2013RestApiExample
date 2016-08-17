using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP2013_WebList
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

    public class ContentTypes
    {
        public Deferred __deferred { get; set; }
    }

    public class DefaultView
    {
        public Deferred __deferred { get; set; }
    }

    public class EventReceivers
    {
        public Deferred __deferred { get; set; }
    }

    public class Fields
    {
        public Deferred __deferred { get; set; }
    }

    public class Forms
    {
        public Deferred __deferred { get; set; }
    }

    public class InformationRightsManagementSettings
    {
        public Deferred __deferred { get; set; }
    }

    public class Items
    {
        public Deferred __deferred { get; set; }
    }

    public class ParentWeb
    {
        public Deferred __deferred { get; set; }
    }

    public class RootFolder
    {
        public Deferred __deferred { get; set; }
    }

    public class UserCustomActions
    {
        public Deferred __deferred { get; set; }
    }

    public class Views
    {
        public Deferred __deferred { get; set; }
    }

    public class WorkflowAssociations
    {
        public Deferred __deferred { get; set; }
    }

    public class D
    {
        public Metadata __metadata { get; set; }
        public FirstUniqueAncestorSecurableObject FirstUniqueAncestorSecurableObject { get; set; }
        public RoleAssignments RoleAssignments { get; set; }
        public ContentTypes ContentTypes { get; set; }
        public DefaultView DefaultView { get; set; }
        public EventReceivers EventReceivers { get; set; }
        public Fields Fields { get; set; }
        public Forms Forms { get; set; }
        public InformationRightsManagementSettings InformationRightsManagementSettings { get; set; }
        public Items Items { get; set; }
        public ParentWeb ParentWeb { get; set; }
        public RootFolder RootFolder { get; set; }
        public UserCustomActions UserCustomActions { get; set; }
        public Views Views { get; set; }
        public WorkflowAssociations WorkflowAssociations { get; set; }
        public bool AllowContentTypes { get; set; }
        public int BaseTemplate { get; set; }
        public int BaseType { get; set; }
        public bool ContentTypesEnabled { get; set; }
        public DateTime? Created { get; set; }
        public string DefaultContentApprovalWorkflowId { get; set; }
        public string Description { get; set; }
        public string Direction { get; set; }
        public string DocumentTemplateUrl { get; set; }
        public int DraftVersionVisibility { get; set; }
        public bool EnableAttachments { get; set; }
        public bool EnableFolderCreation { get; set; }
        public bool EnableMinorVersions { get; set; }
        public bool EnableModeration { get; set; }
        public bool EnableVersioning { get; set; }
        public string EntityTypeName { get; set; }
        public bool ForceCheckout { get; set; }
        public bool HasExternalDataSource { get; set; }
        public bool Hidden { get; set; }
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IrmEnabled { get; set; }
        public bool IrmExpire { get; set; }
        public bool IrmReject { get; set; }
        public bool IsApplicationList { get; set; }
        public bool IsCatalog { get; set; }
        public bool IsPrivate { get; set; }
        public int ItemCount { get; set; }
        public DateTime? LastItemDeletedDate { get; set; }
        public DateTime? LastItemModifiedDate { get; set; }
        public string ListItemEntityTypeFullName { get; set; }
        public int MajorVersionLimit { get; set; }
        public int MajorWithMinorVersionsLimit { get; set; }
        public bool MultipleDataList { get; set; }
        public bool NoCrawl { get; set; }
        public string ParentWebUrl { get; set; }
        public bool ServerTemplateCanCreateFolders { get; set; }
        public string TemplateFeatureId { get; set; }
        public string Title { get; set; }
    }

    public class WebList
    {
        public D d { get; set; }
    }

}
