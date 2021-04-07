using Sitecore.Data.Items;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using Glass.Mapper.Sc;

namespace Sample.Foundation.GlassMapper.Services
{
    public interface IGlassMapperService
    {
        /// <summary>
        /// Get the Context you would like to use. Values are "web", "master", "core" and stored in the Constants file in the GM Foundation Project.
        /// Default context is the Web Context
        /// </summary>
        /// <param name="contextName"></param>     
        IMvcContext GetMvcContext(string contextName = Constants.WebContextName);
        T GetCurrentItem<T>(IMvcContext context = null) where T : class;
        T GetDataSourceItem<T>(IMvcContext context = null) where T : class;
        T GetItem<T>(string path, IMvcContext context = null) where T : class;
        T GetItem<T>(Guid guid, IMvcContext context = null) where T : class;        
        T Cast<T>(Item item, IMvcContext context = null) where T : class;
        T CreateItem<T>(CreateByModelOptions options) where T : class;
        void WriteToItem(WriteToItemOptions options, IMvcContext context = null);
        void SaveItem<T>(T model) where T : class;
        T GetTenantItem<T>(IMvcContext context = null) where T : class;
        T GetStartItem<T>(IMvcContext context = null) where T : class;
        void DeleteItem<T>(DeleteByModelOptions options, IMvcContext context = null) where T : class;        
    }
}
