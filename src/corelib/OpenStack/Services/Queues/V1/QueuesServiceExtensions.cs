namespace OpenStack.Services.Queues.V1
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Net;
    using OpenStack.ObjectModel.JsonHome;
    using OpenStack.Security.Authentication;
    using Rackspace.Threading;

    public static class QueuesServiceExtensions
    {
        #region Base endpoints

        /// <summary>
        /// Get a <see cref="HomeDocument"/> describing the operations supported by the service.
        /// </summary>
        /// <param name="service">The <see cref="IQueuesService"/> instance.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation. When the task
        /// completes successfully, the <see cref="Task{TResult}.Result"/> property returns
        /// a <see cref="HomeDocument"/> instance describing the operations supported by the
        /// service, or <see langword="null"/> if no home document was returned by the API.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpWebException">If an HTTP API call failed during the operation.</exception>
        /// <example>
        /// <para>The following example demonstrates the use of this method using the <see cref="QueuesClient"/>
        /// implementation of the <see cref="IQueuesService"/>. For more information about creating the client, see
        /// <see cref="QueuesClient.QueuesClient(IAuthenticationService, string, Guid, bool)"/>.</para>
        /// <token>AsyncAwaitExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="GetHomeAsync (await)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="GetHomeAsync (await)" language="vbnet"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="GetHomeAsync (await)" language="fs"/>
        /// <token>TplExample</token>
        /// <code source="..\Samples\CSharpCodeSamples\QueueingServiceExamples.cs" region="GetHomeAsync (TPL)" language="cs"/>
        /// <code source="..\Samples\VBCodeSamples\QueueingServiceExamples.vb" region="GetHomeAsync (TPL)" language="vbnet"/>
        /// <code source="..\Samples\CPPCodeSamples\QueueingServiceExamples.cpp" region="GetHomeAsync (TPL)" language="cpp"/>
        /// <code source="..\Samples\FSharpCodeSamples\QueueingServiceExamples.fs" region="GetHomeAsync (TPL)" language="fs"/>
        /// </example>
        /// <seealso cref="HomeDocument"/>
        /// <seealso cref="IQueuesService.PrepareGetHomeAsync"/>
        /// <seealso href="https://wiki.openstack.org/w/index.php?title=Marconi/specs/api/v1#Get_Home_Document">Get Home Document (OpenStack Marconi API v1 Blueprint)</seealso>
        public static Task<HomeDocument> GetHomeAsync(this IQueuesService service, CancellationToken cancellationToken)
        {
            return
                CoreTaskExtensions.Using(
                    () => service.PrepareGetHomeAsync(cancellationToken),
                    task => task.Result.SendAsync(cancellationToken))
                .Select(task => task.Result.Item2);
        }

        #endregion Base endpoints
    }
}
