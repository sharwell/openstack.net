namespace Rackspace.Services.Databases.V1
{
    using System;
    using OpenStack.Collections;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to list backup resources in the database service.
    /// </summary>
    /// <seealso cref="DatabaseBackupExtension.PrepareListBackupsAsync"/>
    /// <seealso cref="DatabaseBackupExtension.ListBackupsAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class ListBackupsApiCall : DelegatingHttpApiCall<ReadOnlyCollectionPage<Backup>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListBackupsApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public ListBackupsApiCall(IHttpApiCall<ReadOnlyCollectionPage<Backup>> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}