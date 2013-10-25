using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Resources;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Framework.Scripts;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XamlSharp.Continuations;

namespace SWEET.WebProjects.Core.SitecoreExtensions.Commands
{
    public class LayoutsViewDetails : Command, ISupportsContinuation
    {

        // Methods
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            if (context.Items.Length == 1)
            {
                if (WebUtil.GetFormValue("scEditorTabs").Contains("layouts:openviewlayoutsdetails"))
                {
                    SheerResponse.Eval("scContent.onEditorTabClick(null, null, 'OpensViewLayoutsDetails')");
                }
                else
                {
                    UrlString urlString = new UrlString("/sitecore/shell/Applications/Sweet/layouts/ViewDetails.aspx");
                    context.Items[0].Uri.AddToUrlString(urlString);
                    UIUtil.AddContentDatabaseParameter(urlString);
                    
                    //urlString["fld"] = "__Tracking";
                    SheerResponse.Eval(new ShowEditorTab { Command = "layouts:openviewlayoutsdetails", Header = "Layout Preview", Icon = Images.GetThemedImageSource("Network/16x16/lock.png"), Url = urlString.ToString(), Id = "openviewlayoutsdetails", Closeable = true }.ToString());
                }
            }
        }

    }
}