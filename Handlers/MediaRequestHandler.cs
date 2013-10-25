using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace SWEET.WebProjects.Core.SitecoreExtensions.Handlers
{
    public class XMediaRequestHandler : MediaRequestHandler
    {
        public override void ProcessRequest(System.Web.HttpContext context)
        {
            
            MediaRequest request = MediaManager.ParseMediaRequest(context.Request);

            if (request != null)
            {
                Media media = MediaManager.GetMedia(request.MediaUri);

                if (media != null)
                {
                    MediaItem mediaItem = media.MediaData.MediaItem;

                    if (mediaItem != null)
                    {
                        if (!IsValidExtension(context.Request.RawUrl,
                            mediaItem.Extension, ".ashx"))
                        {
                            context.Response.StatusCode = 404;
                            context.Response.End();
                        }
                    }
                }
            }
            base.ProcessRequest(context);
        }

        private static bool IsValidExtension(string rawUrl, string mediaExtension,
            string defaultMediaExtension)
        {
            if (rawUrl.Contains('?'))
            {
                rawUrl = rawUrl.Substring(0, rawUrl.IndexOf('?'));
            }

            if (!String.IsNullOrEmpty(mediaExtension)
                && rawUrl.EndsWith(mediaExtension,
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (!String.IsNullOrEmpty(defaultMediaExtension)
                && rawUrl.EndsWith(defaultMediaExtension,
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }


            //Exceptional Scenario: xsl and xslt are treated as same extension
            if (!String.IsNullOrEmpty(mediaExtension))
            {
                if (mediaExtension.EndsWith("xsl"))
                {
                    if (rawUrl.EndsWith("xslt", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                if (mediaExtension.EndsWith("xslt"))
                {
                    if (rawUrl.EndsWith("xsl", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

            }

            //Allow Urls without extensions also
            if (rawUrl.IndexOf('.') < 0)
            {
                return true;
            }

            return false;
        }
    }
}