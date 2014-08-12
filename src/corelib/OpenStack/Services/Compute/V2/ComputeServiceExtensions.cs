namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Collections;
    using OpenStack.Net;
    using OpenStack.Threading;
    using Rackspace.Net;
    using Rackspace.Threading;

    public static class ComputeServiceExtensions
    {
        #region Servers

        public static Task<ReadOnlyCollectionPage<Server>> ListServersAsync(this IComputeService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListServersAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Server> CreateServerAsync(this IComputeService service, ServerData data, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Server> progress)
        {
            Task<Server> result =
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateServerAsync(new ServerRequest(data), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Server);

            if (completionOption == AsyncCompletionOption.RequestSubmitted)
                return result;

            ServerStatus[] exitStatus = { ServerStatus.Active, ServerStatus.Error, ServerStatus.Unknown, ServerStatus.Suspended };
            return result.Then(task => service.WaitForServerStatusAsync(task.Result.Id, exitStatus, progress));
        }

        public static Task<Server> GetServerAsync(this IComputeService service, ServerId serverId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetServerAsync(serverId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Server);
        }

        public static Task<Server> UpdateServerAsync(this IComputeService service, ServerId serverId, ServerData data, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Server> progress)
        {
            Task<Server> result =
                CoreTaskExtensions.Using(
                    () => service.PrepareUpdateServerAsync(serverId, new ServerRequest(data), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Server);

            if (completionOption == AsyncCompletionOption.RequestSubmitted)
                return result;

            ServerStatus[] exitStatus = { ServerStatus.Active, ServerStatus.Error, ServerStatus.Unknown, ServerStatus.Suspended };
            return result.Then(task => service.WaitForServerStatusAsync(serverId, exitStatus, progress));
        }

        public static Task RemoveServerAsync(this IComputeService service, ServerId serverId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Server> progress)
        {
            Task result = CoreTaskExtensions.Using(
                () => service.PrepareRemoveServerAsync(serverId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));

            if (completionOption == AsyncCompletionOption.RequestSubmitted)
                return result;

            ServerStatus[] exitStatus = { ServerStatus.Deleted, ServerStatus.Error, ServerStatus.Unknown, ServerStatus.Suspended };
            return result.Then(task => service.WaitForServerStatusAsync(serverId, exitStatus, progress));
        }

        #endregion

        #region Server Addresses

        public static Task<Addresses> GetServerAddressesAsync(this IComputeService service, ServerId serverId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetServerAddressesAsync(serverId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Addresses);
        }

        #endregion

        #region Server Actions

        public static Task ChangePasswordAsync(this IComputeService service, ServerId serverId, ChangePasswordData data, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Server> progress)
        {
            Task result = CoreTaskExtensions.Using(
                () => service.PrepareChangePasswordAsync(serverId, new ChangePasswordRequest(data), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));

            if (completionOption == AsyncCompletionOption.RequestSubmitted)
                return result;

            ServerStatus[] exitStatus = { ServerStatus.Active, ServerStatus.Error, ServerStatus.Unknown, ServerStatus.Suspended };
            return result.Then(task => service.WaitForServerStatusAsync(serverId, exitStatus, progress));
        }

        public static Task RebootServerAsync(this IComputeService service, ServerId serverId, RebootData data, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Server> progress)
        {
            Task result = CoreTaskExtensions.Using(
                () => service.PrepareRebootServerAsync(serverId, new RebootRequest(data), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));

            if (completionOption == AsyncCompletionOption.RequestSubmitted)
                return result;

            ServerStatus[] exitStatus = { ServerStatus.Active, ServerStatus.Error, ServerStatus.Unknown, ServerStatus.Suspended };
            return result.Then(task => service.WaitForServerStatusAsync(serverId, exitStatus, progress));
        }

        public static Task<Server> RebuildServerAsync(this IComputeService service, ServerId serverId, ServerData data, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Server> progress)
        {
            Task<Server> result =
                CoreTaskExtensions.Using(
                    () => service.PrepareRebuildServerAsync(serverId, new RebuildRequest(data), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Server);

            if (completionOption == AsyncCompletionOption.RequestSubmitted)
                return result;

            ServerStatus[] exitStatus = { ServerStatus.Active, ServerStatus.Error, ServerStatus.Unknown, ServerStatus.Suspended };
            return result.Then(task => service.WaitForServerStatusAsync(serverId, exitStatus, progress));
        }

        public static Task ResizeServerAsync(this IComputeService service, ServerId serverId, ResizeData data, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Server> progress)
        {
            Task result = CoreTaskExtensions.Using(
                () => service.PrepareResizeServerAsync(serverId, new ResizeRequest(data), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));

            if (completionOption == AsyncCompletionOption.RequestSubmitted)
                return result;

            ServerStatus[] exitStatus = { ServerStatus.Active, ServerStatus.Error, ServerStatus.Unknown, ServerStatus.Suspended };
            return result.Then(task => service.WaitForServerStatusAsync(serverId, exitStatus, progress));
        }

        public static Task ConfirmServerResizeAsync(this IComputeService service, ServerId serverId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Server> progress)
        {
            Task result = CoreTaskExtensions.Using(
                () => service.PrepareConfirmServerResizeAsync(serverId, new ConfirmServerResizeRequest(new ConfirmServerResizeData()), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));

            if (completionOption == AsyncCompletionOption.RequestSubmitted)
                return result;

            ServerStatus[] exitStatus = { ServerStatus.Active, ServerStatus.Error, ServerStatus.Unknown, ServerStatus.Suspended };
            return result.Then(task => service.WaitForServerStatusAsync(serverId, exitStatus, progress));
        }

        public static Task RevertServerResizeAsync(this IComputeService service, ServerId serverId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Server> progress)
        {
            Task result = CoreTaskExtensions.Using(
                () => service.PrepareRevertServerResizeAsync(serverId, new RevertServerResizeRequest(new RevertServerResizeData()), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));

            if (completionOption == AsyncCompletionOption.RequestSubmitted)
                return result;

            ServerStatus[] exitStatus = { ServerStatus.Active, ServerStatus.Error, ServerStatus.Unknown, ServerStatus.Suspended };
            return result.Then(task => service.WaitForServerStatusAsync(serverId, exitStatus, progress));
        }

        public static Task<Uri> CreateImageAsync(this IComputeService service, ServerId serverId, CreateImageData data, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Server> progress)
        {
            Task<Uri> result =
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateImageAsync(serverId, new CreateImageRequest(data), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);

            if (completionOption == AsyncCompletionOption.RequestSubmitted)
                return result;

            ServerStatus[] exitStatus = { ServerStatus.Active, ServerStatus.Error, ServerStatus.Unknown, ServerStatus.Suspended };
            return result.Then(task => service.WaitForServerStatusAsync(serverId, exitStatus, progress))
                .Select(_ => result.Result);
        }

        #endregion

        #region Flavors

        public static Task<ReadOnlyCollectionPage<Flavor>> ListFlavorsAsync(this IComputeService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListFlavorsAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Flavor> GetFlavorAsync(this IComputeService service, FlavorId flavorId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetFlavorAsync(flavorId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Flavor);
        }

        #endregion

        #region Images

        public static Task<ReadOnlyCollectionPage<Image>> ListImagesAsync(this IComputeService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListImagesAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Image> GetImageAsync(this IComputeService service, ImageId imageId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetImageAsync(imageId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Image);
        }

        public static Task RemoveImageAsync(this IComputeService service, ImageId imageId, AsyncCompletionOption completionOption, CancellationToken cancellationToken, IProgress<Image> progress)
        {
            Task result = CoreTaskExtensions.Using(
                () => service.PrepareRemoveImageAsync(imageId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));

            if (completionOption == AsyncCompletionOption.RequestSubmitted)
                return result;

            ImageStatus[] exitStatus = { ImageStatus.Deleted, ImageStatus.Error, ImageStatus.Unknown };
            return result.Then(task => service.WaitForImageStatusAsync(imageId, exitStatus, progress));
        }

        #endregion

        #region Metadata

        public static Task<ReadOnlyDictionary<string, string>> GetServerMetadataAsync(this IComputeService service, ServerId serverId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetServerMetadataAsync(serverId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Metadata);
        }

        public static Task<ReadOnlyDictionary<string, string>> SetServerMetadataAsync(this IComputeService service, ServerId serverId, IDictionary<string, string> metadata, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareSetServerMetadataAsync(serverId, new MetadataRequest(metadata), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Metadata);
        }

        public static Task<string> GetServerMetadataItemAsync(this IComputeService service, ServerId serverId, string key, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetServerMetadataItemAsync(serverId, key, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(
                    task =>
                    {
                        var metadata = task.Result.Item2.Metadata;
                        if (metadata == null)
                            return null;

                        string value;
                        if (!metadata.TryGetValue(key, out value))
                            return null;

                        return value;
                    });
        }

        public static Task<ReadOnlyDictionary<string, string>> SetServerMetadataItemAsync(this IComputeService service, ServerId serverId, string key, string value, CancellationToken cancellationToken)
        {
            var metadata = new Dictionary<string, string> { { key, value } };
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareSetServerMetadataItemAsync(serverId, key, new MetadataRequest(metadata), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Metadata);
        }

        public static Task RemoveServerMetadataItemAsync(this IComputeService service, ServerId serverId, string key, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveServerMetadataItemAsync(serverId, key, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<ReadOnlyDictionary<string, string>> GetImageMetadataAsync(this IComputeService service, ImageId imageId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetImageMetadataAsync(imageId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Metadata);
        }

        public static Task<ReadOnlyDictionary<string, string>> SetImageMetadataAsync(this IComputeService service, ImageId imageId, IDictionary<string, string> metadata, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareSetImageMetadataAsync(imageId, new MetadataRequest(metadata), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Metadata);
        }

        public static Task<string> GetImageMetadataItemAsync(this IComputeService service, ImageId imageId, string key, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetImageMetadataItemAsync(imageId, key, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(
                    task =>
                    {
                        var metadata = task.Result.Item2.Metadata;
                        if (metadata == null)
                            return null;

                        string value;
                        if (!metadata.TryGetValue(key, out value))
                            return null;

                        return value;
                    });
        }

        public static Task<ReadOnlyDictionary<string, string>> SetImageMetadataItemAsync(this IComputeService service, ImageId imageId, string key, string value, CancellationToken cancellationToken)
        {
            var metadata = new Dictionary<string, string> { { key, value } };
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareSetImageMetadataItemAsync(imageId, key, new MetadataRequest(metadata), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Metadata);
        }

        public static Task RemoveImageMetadataItemAsync(this IComputeService service, ImageId imageId, string key, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveImageMetadataItemAsync(imageId, key, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Extensions

        public static Task<ReadOnlyCollectionPage<Extension>> ListExtensionsAsync(this IComputeService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareListExtensionsAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        public static Task<Extension> GetExtensionAsync(this IComputeService service, ExtensionAlias alias, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetExtensionAsync(alias, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Extension);
        }

        #endregion

        #region Additional optional parameters for API calls

        public static Task<ListServersApiCall> WithPageSize(this Task<ListServersApiCall> task, int pageSize)
        {
            return task.WithQueryParameter("limit", pageSize.ToString());
        }

        public static Task<ListImagesApiCall> WithPageSize(this Task<ListImagesApiCall> task, int pageSize)
        {
            return task.WithQueryParameter("limit", pageSize.ToString());
        }

        public static Task<ListFlavorsApiCall> WithPageSize(this Task<ListFlavorsApiCall> task, int pageSize)
        {
            return task.WithQueryParameter("limit", pageSize.ToString());
        }

        public static Task<ListServersApiCall> WithMarker(this Task<ListServersApiCall> task, ServerId marker)
        {
            return task.WithQueryParameter("marker", marker.Value);
        }

        public static Task<ListImagesApiCall> WithMarker(this Task<ListImagesApiCall> task, ImageId marker)
        {
            return task.WithQueryParameter("marker", marker.Value);
        }

        public static Task<ListFlavorsApiCall> WithMarker(this Task<ListFlavorsApiCall> task, FlavorId marker)
        {
            return task.WithQueryParameter("marker", marker.Value);
        }

        public static Task<ListFlavorsApiCall> WithMinDisk(this Task<ListFlavorsApiCall> task, int value)
        {
            return task.WithQueryParameter("minDisk", value.ToString());
        }

        public static Task<ListFlavorsApiCall> WithMinRam(this Task<ListFlavorsApiCall> task, int value)
        {
            return task.WithQueryParameter("minRam", value.ToString());
        }

        /// <summary>
        /// Restrict an API call prepared by <see cref="IComputeService.PrepareListServersAsync"/> to
        /// only include servers with a <see cref="Server.LastModified"/> timestamp after a specified
        /// time.
        /// </summary>
        /// <remarks>
        /// This method modifies the request URI of the <see cref="ListServersApiCall"/> by including
        /// a <c>changes-since</c> query parameter. This parameter can be used for both filtering of
        /// the returned list of servers and for obtaining lists which include servers in the
        /// <see cref="ServerStatus.Deleted"/> state.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare a <see cref="ListServersApiCall"/>.</param>
        /// <param name="timestamp">A timestamp indicating the filter to apply to the servers listing according to the <see cref="Server.LastModified"/> property.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        public static Task<ListServersApiCall> WithChangesSince(this Task<ListServersApiCall> task, DateTimeOffset timestamp)
        {
            return task.WithChangesSinceImpl(timestamp);
        }

        /// <summary>
        /// Restrict an API call prepared by <see cref="IComputeService.PrepareListImagesAsync"/> to
        /// only include images with a <see cref="Image.LastModified"/> timestamp after a specified
        /// time.
        /// </summary>
        /// <remarks>
        /// This method modifies the request URI of the <see cref="ListImagesApiCall"/> by including
        /// a <c>changes-since</c> query parameter. This parameter can be used for both filtering of
        /// the returned list of images and for obtaining lists which include images in the
        /// <see cref="ImageStatus.Deleted"/> state.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare a <see cref="ListImagesApiCall"/>.</param>
        /// <param name="timestamp">A timestamp indicating the filter to apply to the image listing according to the <see cref="Image.LastModified"/> property.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        public static Task<ListImagesApiCall> WithChangesSince(this Task<ListImagesApiCall> task, DateTimeOffset timestamp)
        {
            return task.WithChangesSinceImpl(timestamp);
        }

        /// <summary>
        /// Restrict an API call prepared by <see cref="IComputeService.PrepareListFlavorsAsync"/> to
        /// only include flavors with a <see cref="Flavor.LastModified"/> timestamp after a specified
        /// time.
        /// </summary>
        /// <remarks>
        /// This method modifies the request URI of the <see cref="ListFlavorsApiCall"/> by including
        /// a <c>changes-since</c> query parameter. This parameter can be used for filtering of the
        /// returned list of flavors.
        /// </remarks>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare a <see cref="ListFlavorsApiCall"/>.</param>
        /// <param name="timestamp">A timestamp indicating the filter to apply to the flavor listing according to the <see cref="Flavor.LastModified"/> property.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        public static Task<ListFlavorsApiCall> WithChangesSince(this Task<ListFlavorsApiCall> task, DateTimeOffset timestamp)
        {
            return task.WithChangesSinceImpl(timestamp);
        }

        /// <summary>
        /// This method provides a generic support for the <c>changes-since</c> query parameter.
        /// It is not exposed publicly in order to prevent suggesting support for the query parameter
        /// on API calls that are not known to support it.
        /// </summary>
        /// <typeparam name="TCall">The type of API call being modified by this method.</typeparam>
        /// <param name="task">A <see cref="Task"/> representing the asynchronous operation to prepare a <see cref="ListImagesApiCall"/>.</param>
        /// <param name="timestamp">A timestamp indicating the filter to apply to the image listing according to the <see cref="Image.LastModified"/> property.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property contains
        /// the modified API call.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="task"/> is <see langword="null"/>.</exception>
        /// <exception cref="ObjectDisposedException">If the API call provided by <paramref name="task"/> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">If the API call provided by <paramref name="task"/> has already been sent.</exception>
        private static Task<TCall> WithChangesSinceImpl<TCall>(this Task<TCall> task, DateTimeOffset timestamp)
            where TCall : IHttpApiRequest
        {
            if (task == null)
                throw new ArgumentNullException("task");

            string parameter = "changes%2Dsince";
            string value = timestamp.UtcDateTime.ToString("s") + "Z";
            return task.WithQueryParameter(parameter, value);
        }

        private static Task<TCall> WithQueryParameter<TCall>(this Task<TCall> task, string parameter, string value)
            where TCall : IHttpApiRequest
        {
            return task.Select(
                innerTask =>
                {
                    Uri requestUri = innerTask.Result.RequestMessage.RequestUri;
                    requestUri = UriUtility.SetQueryParameter(requestUri, parameter, value);
                    innerTask.Result.RequestMessage.RequestUri = requestUri;
                    return innerTask.Result;
                });
        }

        #endregion
    }
}
