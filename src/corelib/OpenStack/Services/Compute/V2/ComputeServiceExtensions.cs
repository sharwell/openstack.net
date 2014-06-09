namespace OpenStack.Services.Compute.V2
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Collections;
    using OpenStack.Net;
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

        public static Task<Server> CreateServerAsync(this IComputeService service, ServerData data, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateServerAsync(new ServerRequest(data), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Server);
        }

        public static Task<Server> GetServerAsync(this IComputeService service, ServerId serverId, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetServerAsync(serverId, cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Server);
        }

        public static Task<Server> UpdateServerAsync(this IComputeService service, ServerData data, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareUpdateServerAsync(new ServerRequest(data), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Server);
        }

        public static Task RemoveServerAsync(this IComputeService service, ServerId serverId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveServerAsync(serverId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Server Actions

        public static Task ChangePasswordAsync(this IComputeService service, ServerId serverId, ChangePasswordData data, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareChangePasswordAsync(serverId, new ChangePasswordRequest(data), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RebootServerAsync(this IComputeService service, ServerId serverId, RebootData data, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRebootServerAsync(serverId, new RebootRequest(data), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<Server> RebuildServerAsync(this IComputeService service, ServerId serverId, ServerData data, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareRebuildServerAsync(serverId, new RebuildRequest(data), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2.Server);
        }

        public static Task ResizeServerAsync(this IComputeService service, ServerId serverId, ResizeData data, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareResizeServerAsync(serverId, new ResizeRequest(data), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task ConfirmServerResizeAsync(this IComputeService service, ServerId serverId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareConfirmServerResizeAsync(serverId, new ConfirmServerResizeRequest(new ConfirmServerResizeData()), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task RevertServerResizeAsync(this IComputeService service, ServerId serverId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRevertServerResizeAsync(serverId, new RevertServerResizeRequest(new RevertServerResizeData()), cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        public static Task<Uri> CreateImageAsync(this IComputeService service, ServerId serverId, CreateImageData data, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareCreateImageAsync(serverId, new CreateImageRequest(data), cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
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

        public static Task RemoveImageAsync(this IComputeService service, ImageId imageId, CancellationToken cancellationToken)
        {
            return CoreTaskExtensions.Using(
                () => service.PrepareRemoveImageAsync(imageId, cancellationToken),
                task => task.Result.SendAsync(cancellationToken));
        }

        #endregion

        #region Additional optional parameters for API calls

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

            return task.Select(
                innerTask =>
                {
                    Uri requestUri = task.Result.RequestMessage.RequestUri;
                    UriTemplate template = new UriTemplate(string.IsNullOrEmpty(requestUri.Query) ? "{?changes%2Dsince}" : "{&changes%2Dsince}");
                    var parameters = new Dictionary<string, string> { { "changes%2Dsince", timestamp.UtcDateTime.ToString("s") + "Z" } };
                    Uri queryFragment = template.BindByName(parameters);
                    task.Result.RequestMessage.RequestUri = new Uri(requestUri.OriginalString + queryFragment.OriginalString, UriKind.Absolute);
                    return task.Result;
                });
        }

        #endregion
    }
}
