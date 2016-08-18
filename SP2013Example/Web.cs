using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP2013_Web
{
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Deferred
    {
        public string uri { get; set; }
    }

    public class FirstUniqueAncestorSecurableObject
    {
        public Deferred __deferred { get; set; }
    }

    public class RoleAssignments
    {
        public Deferred __deferred { get; set; }
    }

    public class AllProperties
    {
        public Deferred __deferred { get; set; }
    }

    public class AssociatedMemberGroup
    {
        public Deferred __deferred { get; set; }
    }

    public class AssociatedOwnerGroup
    {
        public Deferred __deferred { get; set; }
    }

    public class AssociatedVisitorGroup
    {
        public Deferred __deferred { get; set; }
    }

    public class AvailableContentTypes
    {
        public Deferred __deferred { get; set; }
    }

    public class AvailableFields
    {
        public Deferred __deferred { get; set; }
    }

    public class ContentTypes
    {
        public Deferred __deferred { get; set; }
    }

    public class CurrentUser
    {
        public Deferred __deferred { get; set; }
    }

    public class EventReceivers
    {
        public Deferred __deferred { get; set; }
    }

    public class Features
    {
        public Deferred __deferred { get; set; }
    }

    public class Fields
    {
        public Deferred __deferred { get; set; }
    }

    public class Folders
    {
        public Deferred __deferred { get; set; }
    }

    public class Lists
    {
        public Deferred __deferred { get; set; }
    }

    public class ListTemplates
    {
        public Deferred __deferred { get; set; }
    }

    public class Navigation
    {
        public Deferred __deferred { get; set; }
    }

    public class ParentWeb
    {
        public Deferred __deferred { get; set; }
    }

    public class PushNotificationSubscribers
    {
        public Deferred __deferred { get; set; }
    }

    public class RecycleBin
    {
        public Deferred __deferred { get; set; }
    }

    public class RegionalSettings
    {
        public Deferred __deferred { get; set; }
    }

    public class RoleDefinitions
    {
        public Deferred __deferred { get; set; }
    }

    public class RootFolder
    {
        public Deferred __deferred { get; set; }
    }

    public class SiteGroups
    {
        public Deferred __deferred { get; set; }
    }

    public class SiteUserInfoList
    {
        public Deferred __deferred { get; set; }
    }

    public class SiteUsers
    {
        public Deferred __deferred { get; set; }
    }

    public class ThemeInfo
    {
        public Deferred __deferred { get; set; }
    }

    public class UserCustomActions
    {
        public Deferred __deferred { get; set; }
    }

    public class Webs
    {
        public Deferred __deferred { get; set; }
    }

    public class WebInfos
    {
        public Deferred __deferred { get; set; }
    }

    public class WorkflowAssociations
    {
        public Deferred __deferred { get; set; }
    }

    public class WorkflowTemplates
    {
        public Deferred __deferred { get; set; }
    }

    public class D
    {
        public Metadata __metadata { get; set; }
        public FirstUniqueAncestorSecurableObject FirstUniqueAncestorSecurableObject { get; set; }
        public RoleAssignments RoleAssignments { get; set; }
        public AllProperties AllProperties { get; set; }
        public AssociatedMemberGroup AssociatedMemberGroup { get; set; }
        public AssociatedOwnerGroup AssociatedOwnerGroup { get; set; }
        public AssociatedVisitorGroup AssociatedVisitorGroup { get; set; }
        public AvailableContentTypes AvailableContentTypes { get; set; }
        public AvailableFields AvailableFields { get; set; }
        public ContentTypes ContentTypes { get; set; }
        public CurrentUser CurrentUser { get; set; }
        public EventReceivers EventReceivers { get; set; }
        public Features Features { get; set; }
        public Fields Fields { get; set; }
        public Folders Folders { get; set; }
        public Lists Lists { get; set; }
        public ListTemplates ListTemplates { get; set; }
        public Navigation Navigation { get; set; }
        public ParentWeb ParentWeb { get; set; }
        public PushNotificationSubscribers PushNotificationSubscribers { get; set; }
        public RecycleBin RecycleBin { get; set; }
        public RegionalSettings RegionalSettings { get; set; }
        public RoleDefinitions RoleDefinitions { get; set; }
        public RootFolder RootFolder { get; set; }
        public SiteGroups SiteGroups { get; set; }
        public SiteUserInfoList SiteUserInfoList { get; set; }
        public SiteUsers SiteUsers { get; set; }
        public ThemeInfo ThemeInfo { get; set; }
        public UserCustomActions UserCustomActions { get; set; }
        public Webs Webs { get; set; }
        public WebInfos WebInfos { get; set; }
        public WorkflowAssociations WorkflowAssociations { get; set; }
        public WorkflowTemplates WorkflowTemplates { get; set; }
        public bool AllowRssFeeds { get; set; }
        public string AlternateCssUrl { get; set; }
        public string AppInstanceId { get; set; }
        public int Configuration { get; set; }
        public DateTime Created { get; set; }
        public string CustomMasterUrl { get; set; }
        public string Description { get; set; }
        public bool DocumentLibraryCalloutOfficeWebAppPreviewersDisabled { get; set; }
        public bool EnableMinimalDownload { get; set; }
        public string Id { get; set; }
        public bool IsMultilingual { get; set; }
        public int Language { get; set; }
        public DateTime LastItemModifiedDate { get; set; }
        public string MasterUrl { get; set; }
        public bool OverwriteTranslationsOnChange { get; set; }
        public bool QuickLaunchEnabled { get; set; }
        public bool RecycleBinEnabled { get; set; }
        public string ServerRelativeUrl { get; set; }
        public string SiteLogoUrl { get; set; }
        public bool SyndicationEnabled { get; set; }
        public string Title { get; set; }
        public bool TreeViewEnabled { get; set; }
        public int UIVersion { get; set; }
        public bool UIVersionConfigurationEnabled { get; set; }
        public string Url { get; set; }
        public string WebTemplate { get; set; }
    }

    public class Web
    {
        public D d { get; set; }
    }
}
