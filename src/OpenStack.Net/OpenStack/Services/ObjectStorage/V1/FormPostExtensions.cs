﻿namespace OpenStack.Services.ObjectStorage.V1
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using Rackspace.Threading;


    /// <summary>
    /// This class provides extension methods for using the optional Form POST functionality provided by the Object
    /// Storage service.
    /// </summary>
    /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/form-post.html">Form POST middleware (OpenStack Object Storage API v1 Reference)</seealso>
    /// <threadsafety static="true" instance="false"/>
    /// <preliminary/>
    public static class FormPostExtensions
    {
        /// <summary>
        /// Determines whether a particular Object Storage Service supports the optional Form POST middleware extension.
        /// <note type="warning">This method relies on properties which are not defined by OpenStack, and may not be
        /// consistent across vendors.</note>
        /// </summary>
        /// <remarks>
        /// <para>If the Object Storage Service supports the Form POST middleware, but does not support feature
        /// discoverability, this method might return <see langword="false"/> or result in an
        /// <see cref="HttpWebException"/> even though the Form POST feature is supported. To ensure this situation does
        /// not prevent the use of the Form POST functionality, it is not automatically checked during the
        /// <see cref="CreateFormPostUriAsync"/> operation.</para>
        /// </remarks>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes successfully, the
        /// <see cref="Task{TResult}.Result"/> property contains a value indicating whether or not the service supports
        /// the Form POST middleware extension.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/discoverability.html">Discoverability (OpenStack Object Storage API V1 Reference)</seealso>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/form-post.html">Form POST middleware (OpenStack Object Storage API v1 Reference)</seealso>
        public static Task<bool> SupportsFormPostAsync(this IObjectStorageService service, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            IFormPostExtension extension;
            try
            {
                extension = service.GetServiceExtension(PredefinedObjectStorageExtensions.FormPost);
            }
            catch (NotSupportedException)
            {
                return CompletedTask.FromResult(false);
            }

            return TaskBlocks.Using(
                () => extension.PrepareFormPostSupportedAsync(cancellationToken),
                task => task.Result.SendAsync(cancellationToken).Select(innerTask => innerTask.Result.Item2));
        }

        /// <summary>
        /// Construct a <see cref="Uri"/> and field information supporting the public upload of objects to an Object
        /// Storage container via an HTTP form submission.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The HTTP form used for uploading files has the following form, where <em>uri</em> is a placeholder for the
        /// URI described in the return value from this method.
        /// </para>
        ///
        /// <code language="html">
        /// &lt;form action="<em>uri</em>" method="POST" enctype="multipart/form-data"&gt;
        ///   &lt;input type="file" name="file1"/&gt;&lt;br/&gt;
        ///   &lt;input type="submit"/&gt;
        /// &lt;/form&gt;
        /// </code>
        ///
        /// <para>
        /// In addition to the above <c>&lt;input&gt;</c> fields, the form should include one hidden input for each of
        /// the key/value pairs described in the return value from this method. Each of these fields should have the
        /// following form, where <em>key</em> and <em>value</em> are placeholders for one key/value pair.
        /// </para>
        ///
        /// <code>
        /// &lt;input type="hidden" name="<em>key</em>" value="<em>value</em>"/&gt;
        /// </code>
        /// </remarks>
        /// <param name="service">The <see cref="IObjectStorageService"/> instance.</param>
        /// <param name="container">The container where uploaded files are placed.</param>
        /// <param name="objectPrefix">The prefix applied to uploaded objects.</param>
        /// <param name="key">The account key to use with the Form POST middleware extension, as specified in the
        /// account <see cref="AccountMetadataExtensions.AccountSecretKey"/> metadata.</param>
        /// <param name="expiration">The expiration time for the generated URI.</param>
        /// <param name="redirectUri">The URI to redirect the user to after the upload operation completes.</param>
        /// <param name="maxFileSize">Specifies the maximum size in bytes of a single file.</param>
        /// <param name="maxFileCount">The maximum number of files which can be uploaded in a single request.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task completes successfully, the
        /// <see cref="Task{TResult}.Result"/> property will contain a <see cref="Tuple{T1, T2}"/> object containing the
        /// information necessary to submit a POST operation uploading one or more files to the Object Storage Service.
        /// The first item in the tuple is the absolute URI where the form data should be submitted, which is valid
        /// until the <paramref name="expiration"/> time passes or the account key is changed. The second item is a
        /// collection of key/value pairs describing the names/values of additional fields to submit with the form.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para>If <paramref name="service"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="container"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="objectPrefix"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="key"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="redirectUri"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para>If <paramref name="maxFileSize"/> is less than 0.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="maxFileCount"/> is less or equal to 0.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para>If <paramref name="key"/> is empty.</para>
        /// </exception>
        /// <seealso cref="AccountMetadataExtensions.AccountSecretKey"/>
        /// <seealso href="http://docs.openstack.org/api/openstack-object-storage/1.0/content/form-post.html">Form POST middleware (OpenStack Object Storage API v1 Reference)</seealso>
        public static Task<Tuple<Uri, ReadOnlyDictionary<string, string>>> CreateFormPostUriAsync(this IObjectStorageService service, ContainerName container, ObjectName objectPrefix, string key, DateTimeOffset expiration, Uri redirectUri, long maxFileSize, int maxFileCount, CancellationToken cancellationToken)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            IFormPostExtension formPostExtension = service.GetServiceExtension(PredefinedObjectStorageExtensions.FormPost);
            return formPostExtension.CreateFormPostUriAsync(container, objectPrefix, key, expiration, redirectUri, maxFileSize, maxFileCount, cancellationToken);
        }
    }
}
