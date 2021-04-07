using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Sample.Foundation.SitecoreExtensions.Extensions
{
    public static class RenderingExtensions
    {
        public static Sitecore.Layouts.RenderingReference[] GetRenderingReferences(this Item item, string deviceName)
        {
            LayoutField layoutField = item.Fields["__final renderings"];
            if (layoutField == null)
                return null;
            Sitecore.Layouts.RenderingReference[] renderings = null;
            if (item.Database != null)
            {
                renderings = layoutField.GetReferences(item.Database.GetDeviceItem(deviceName));
            }
            else
            {
                renderings = layoutField.GetReferences(Sitecore.Context.Database.GetDeviceItem(deviceName));
            }
            return renderings;
        }

        public static DeviceItem GetDeviceItem(this Database db, string deviceName)
        {
            return db.Resources.Devices.GetAll().Where(d => d.Name.ToLower() == deviceName.ToLower()).First();
        }
    }
}