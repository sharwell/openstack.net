namespace Rackspace.Services.Identity.V2
{
    using System;
    using Newtonsoft.Json.Linq;
    using OpenStack.Services.Identity.V2;

    public static class DefaultRegionExtensions
    {
        private const string DefaultRegionProperty = "RAX-AUTH:defaultRegion";

        public static string GetDefaultRegion(this UserDetails userDetails)
        {
            if (userDetails == null)
                throw new ArgumentNullException("userDetails");

            JToken value;
            if (!userDetails.ExtensionData.TryGetValue(DefaultRegionProperty, out value))
                return null;

            return value.ToObject<string>();
        }
    }
}
