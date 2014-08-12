namespace Rackspace.Services.Databases.V1
{
    using System;
    using OpenStack.Net;

    /// <summary>
    /// This class represents a prepared API call to create a backup of a database within the database service.
    /// </summary>
    /// <seealso cref="DatabaseBackupExtension.PrepareCreateBackupAsync"/>
    /// <seealso cref="DatabaseBackupExtension.CreateBackupAsync"/>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public class CreateBackupApiCall : DelegatingHttpApiCall<BackupResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBackupApiCall"/> class
        /// with the behavior provided by another <see cref="IHttpApiCall{T}"/> instance.
        /// </summary>
        /// <param name="httpApiCall">The <see cref="IHttpApiCall{T}"/> providing the behavior for the API call.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="httpApiCall"/> is <see langword="null"/>.</exception>
        public CreateBackupApiCall(IHttpApiCall<BackupResponse> httpApiCall)
            : base(httpApiCall)
        {
        }
    }
}