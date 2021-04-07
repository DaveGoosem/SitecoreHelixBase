using System.Web;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Data.Items;
using Sample.Foundation.SitecoreExtensions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sample.Foundation.Logging.Services;
using Sample.Foundation.GlassMapper.Services;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using Sample.Feature.Analytics.Models;
using System;
using System.Data;
using System.Text;
using Sitecore.Web;
using Sample.Foundation.Caching.Services;

namespace Sample.Feature.Analytics.Handlers
{
    public class SiteMapHandler : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            HttpContext context = HttpContext.Current;

            if (context == null)
            {
                return;
            }

            string requestUrl = context.Request.Url.ToString();

            if (string.IsNullOrEmpty(requestUrl) || !requestUrl.ToLower().EndsWith("sitemap.xml"))
            {
                return;
            }

            string siteMapContent = string.Empty;

            if (Sitecore.Context.Site != null && Sitecore.Context.Database != null)
            {
                Item siteTenantItem = SiteExtensions.GetTenantItem(Sitecore.Context.Site);
                Item homeItem = SiteExtensions.GetStartItem(Sitecore.Context.Site);
                if (siteTenantItem != null && homeItem != null)
                {
                    var logService = ServiceLocator.ServiceProvider.GetService<ILogService>();
                    var glassMapperService = ServiceLocator.ServiceProvider.GetService<IGlassMapperService>();


                    //grab the base domain to use in each page item
                    var siteMapBaseDomain = siteTenantItem.Fields[Constants.SitemapBaseDomainField].Value;
                    var homeSiteMapItem = glassMapperService.GetItem<ISiteMapItem>(homeItem.Paths.Path);

                    //Cached "per site"
                    string siteCacheKey = string.Format("{0}_{1}", Sitecore.Context.Site.Name, Constants.SitemapContentCacheKey);
                    if (CacheManagerService.GetObject(siteCacheKey) == null)
                    {
                        List<ISiteMapItem> allPageItemsFlattened = new List<ISiteMapItem>();

                        if (homeSiteMapItem.IncludeInSitemap)
                        {
                            allPageItemsFlattened.Add(homeSiteMapItem);
                        }

                        var flattenedSiteMapItems = GetRecursiveFlattenedSiteMapItems(homeSiteMapItem, allPageItemsFlattened);
                        if (flattenedSiteMapItems != null)
                        {
                            siteMapContent = GenerateSiteMapXML(flattenedSiteMapItems, siteMapBaseDomain, logService, glassMapperService);
                            CacheManagerService.SetObject(siteCacheKey, siteMapContent);
                        }
                    }
                    else
                    {
                        siteMapContent = CacheManagerService.GetObject(siteCacheKey).ToString();
                    }
                }
            }

            context.Response.ContentType = "text/xml";
            context.Response.Write(siteMapContent);
            context.Response.End();
        }


        private List<ISiteMapItem> GetRecursiveFlattenedSiteMapItems(ISiteMapItem parentItem, List<ISiteMapItem> allPageItemsFlattened)
        {
            if (parentItem != null)
            {
                var filteredPages = parentItem.Children.Where(x => DoesSitecoreItemHavePresentation(x));

                foreach (var item in filteredPages)
                {
                    if (item.IncludeInSitemap)
                        allPageItemsFlattened.Add(item);

                    if (item.Children.Count() > 0)
                    {
                        GetRecursiveFlattenedSiteMapItems(item, allPageItemsFlattened);
                    }
                }

                return allPageItemsFlattened;
            }

            return null;
        }

        /// <summary>
        /// To check whether Sitecore Item have presentation settings
        /// </summary>
        /// <param name="item">Current Item</param>
        /// <returns>HasPresentation</returns>
        public bool DoesSitecoreItemHavePresentation(ISiteMapItem siteMapItem)
        {
            var glassMapperService = ServiceLocator.ServiceProvider.GetService<IGlassMapperService>();
            var item = glassMapperService.GetItem<Item>(siteMapItem.Id);
            return item.GetRenderingReferences("default") != null;
        }

        private string GenerateSiteMapXML(IEnumerable<ISiteMapItem> siteMapItemsList, string siteMapBaseDomain, ILogService logService, IGlassMapperService glassMapperService)
        {
            logService.Info("Entering SiteMapGenerator :: ProcessSiteMapData");
            var sitecoreSitemap = new DataSet()
            {
                EnforceConstraints = false
            };

            string xmlAsString = string.Empty;

            try
            {
                XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

                var sitemaproot = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

                //add the namespace to xml document
                var sitemapRootElement = new XElement(xmlns + "urlset", new XAttribute(XNamespace.Xmlns + "xsi", xsi));

                foreach (var item in siteMapItemsList)
                {
                    // regular Page items
                    sitemapRootElement.Add(new XElement(xmlns + "url",
                        new XElement(xmlns + "loc", string.Format("{0}{1}", siteMapBaseDomain, item.PageUrl.TrimEnd('/'))),
                        new XElement(xmlns + "lastmod", item.UpdatedDate.ToString(item.UpdatedDate == DateTime.MinValue ? "yyyy-MM-ddThh:mm:ss" : "yyyy-MM-ddThh:mm:sszzz")),
                        new XElement(xmlns + "changefreq", "weekly"),
                        new XElement(xmlns + "priority", "0.5")));
                }

                //append the root element with it's contents to the root item to construct the full xml data set
                sitemaproot += sitemapRootElement;

                XmlDocument scSitemapXml = new XmlDocument();
                scSitemapXml.LoadXml(sitemaproot);

                using (XmlNodeReader nodeReader = new XmlNodeReader(scSitemapXml))
                {
                    sitecoreSitemap.ReadXml(nodeReader);
                }

                XmlDocument sitemapDocument = new XmlDocument();
                sitemapDocument.LoadXml(sitecoreSitemap.GetXml());

                XmlAttribute xmlnXsi = null;
                XmlAttribute xmlnImage = null;
                XmlAttribute schemaLocation = null;

                xmlnXsi = sitemapDocument.CreateAttribute("xmlns:xsi");
                xmlnXsi.Value = @"http://www.w3.org/2001/XMLSchema-instance";
                sitemapDocument.DocumentElement.SetAttributeNode(xmlnXsi);

                xmlnImage = sitemapDocument.CreateAttribute("xmlns:image");
                xmlnImage.Value = @"http://www.google.com/schemas/sitemap-image/1.1";
                sitemapDocument.DocumentElement.SetAttributeNode(xmlnImage);

                var location = @"http://www.sitemaps.org/schemas/sitemap/0.9 
                                http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd 
                                http://www.google.com/schemas/sitemap-image/1.1 
                                http://www.google.com/schemas/sitemap-image/1.1/sitemap-image.xsd";

                System.Collections.Specialized.NameValueCollection listItems = WebUtil.ParseUrlParameters(location);
                StringBuilder s = new StringBuilder();
                foreach (string textItem in listItems.Keys)
                {
                    s.Append(listItems[textItem].ToString());
                    s.Append(" ");
                }
                schemaLocation = sitemapDocument.CreateAttribute("xsi", "schemaLocation", "");
                schemaLocation.Value = s.ToString();
                sitemapDocument.DocumentElement.SetAttributeNode(schemaLocation);

                xmlAsString = sitemapDocument.OuterXml;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sitecoreSitemap.Dispose();
            }
            logService.Info("Exiting SiteMapGenerator :: ProcessSiteMapData");

            return xmlAsString;

        }

    }
}