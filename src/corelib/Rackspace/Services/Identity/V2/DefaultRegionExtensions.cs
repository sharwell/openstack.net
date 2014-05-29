namespace Rackspace.Services.Identity.V2
{
    using System;
    using Newtonsoft.Json.Linq;
    using OpenStack.Services.Identity.V2;

    /// <summary>
    /// Provides extension methods for accessing the default region properties of
    /// authenticated users in the Rackspace Cloud Identity product.
    /// </summary>
    public static class DefaultRegionExtensions
    {
        /// <summary>
        /// The name of the default region property as it appears in the JSON representation.
        /// </summary>
        private const string DefaultRegionProperty = "RAX-AUTH:defaultRegion";

        /// <summary>
        /// Gets the default region for an authenticated user.
        /// </summary>
        /// <param name="userDetails">The <see cref="UserDetails"/> object for a user authenticated with Rackspace Cloud Identity.</param>
        /// <returns>
        /// The default region for the user.
        /// <para>-or-</para>
        /// <para><see langword="null"/> if no default region is assigned for the user.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="userDetails"/> is <see langword="null"/>.</exception>
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
