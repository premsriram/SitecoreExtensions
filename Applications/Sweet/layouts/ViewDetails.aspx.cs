using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore;
using Sitecore.Data;
using Sitecore.Layouts;

namespace SWEET.WebProjects.Core.SitecoreExtensions.Applications.Sweet.layouts
{
    public partial class ViewDetails : System.Web.UI.Page
    {
        protected Database currentDatabase;
        protected Database CurrentDatabase
        {
            get
            {
                if (currentDatabase == null)
                {
                    currentDatabase =
                        Database.GetDatabase(string.IsNullOrEmpty(Request.QueryString["sc_content"])
                                                 ? "master"
                                                 : Request.QueryString["sc_content"]);
                }
                return currentDatabase;
            }
        }

        protected Item currentSitecoreItem;
        protected Item CurrentSitecoreItem
        {
            get
            {
                if (currentSitecoreItem == null)
                {
                    currentSitecoreItem = UIUtil.GetItemFromQueryString(CurrentDatabase);
                }
                return currentSitecoreItem;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Item currentitem = CurrentSitecoreItem;

            string itemid = currentitem.ID.Guid.ToString();
            string itemversion = currentitem.Version.Number.ToString();
            string language = currentitem.Language.Name ;

            viewLayouts.Attributes["src"] = "/sitecore/shell/default.aspx?xmlcontrol=LayoutViewDetails&id=%7b"+ itemid +"%7d&la="+language+"&vs=" + itemversion;

        }

    }
}