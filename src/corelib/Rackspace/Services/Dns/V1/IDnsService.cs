namespace Rackspace.Services.Dns.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenStack.Collections;
    using OpenStack.Services;

    public interface IDnsService : IHttpService
    {
        #region Limits

        /// <summary>
        /// Get information about the provider-specific limits of this service.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsServiceLimits"/> object containing detailed information about the limits for the service provider.</returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/GET_listLimits_v1.0__account__limits_limits.html">List limits (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<ListAllLimitsApiCall> PrepareListAllLimitsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get information about the types of provider-specific limits in place for this service.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will return a collection of <see cref="LimitType"/> objects containing the limit types supported by the service.</returns>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/GET_listLimitTypes_v1.0__account__limits_types_limits.html">List limit types (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<ListLimitTypesApiCall> PrepareListLimitTypesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get information about the provider-specific limits of this service for a particular <see cref="LimitType"/>.
        /// </summary>
        /// <param name="type">The limit type.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsServiceLimits"/> object containing detailed information about the limits of the specified <paramref name="type"/> for the service provider.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="type"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/GET_showLimits_v1.0__account__limits__type__limits.html">Show limits (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<ListLimitsApiCall> PrepareListLimitsAsync(LimitType type, CancellationToken cancellationToken);

        #endregion

        #region Jobs

        /// <summary>
        /// Gets information about an asynchronous task with a strongly-typed result being executed by the DNS service.
        /// </summary>
        /// <typeparam name="TResponse">The class modeling the JSON result of the asynchronous operation.</typeparam>
        /// <param name="job">The <see cref="DnsJob{TResponse}"/> to query.</param>
        /// <param name="showDetails"><see langword="true"/> to include detailed information about the job; otherwise, <see langword="false"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsJob{TResult}"/> object containing the updated job information.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="job"/> is <see langword="null"/>.</exception>
        /// <exception cref="JsonSerializationException">If an error occurs while deserializing the response object.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/sync_asynch_responses.html">Synchronous and Asynchronous Responses (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<GetJobStatusApiCall<TResponse>> PrepareGetJobStatusAsync<TResponse>(DnsJob<TResponse> job, CancellationToken cancellationToken);

        #endregion

        #region Domains

        /// <summary>
        /// Gets information about domains currently listed in the DNS service.
        /// </summary>
        /// <param name="domainName">If specified, the list will be filtered to only include the specified domain and its subdomains (if any exist).</param>
        /// <param name="offset">The index of the last item in the previous page of results. If not specified, the list starts at the beginning.</param>
        /// <param name="limit">The maximum number of domains to return in a single page.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a tuple of the resulting collection of
        /// <see cref="DnsDomain"/> objects and the total number of domains in the list. If the total number of domains
        /// in the list is not available, the second element of the tuple will be <see langword="null"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="offset"/> is less than 0.
        /// <para>-or-</para>
        /// <para>If <paramref name="limit"/> is less than or equal to 0.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/GET_listDomains_v1.0__account__domains_domains.html">List domains (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/GET_listDomainsbyName_v1.0__account__domains_domains.html">List domains by name (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<ListDomainsApiCall> PrepareListDomainsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Registers one or more new domains in the DNS service.
        /// </summary>
        /// <param name="configuration">A <see cref="DnsConfiguration"/> object describing the domains to register in the DNS service.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsJob{TResponse}"/> object
        /// describing the asynchronous server operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/>: In this case the <see cref="DnsJob{TResponse}.Response"/>
        /// property contains a <see cref="DnsDomains"/> object containing the details of the new domains.</item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="configuration"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/create_domains.html">Create Domain(s) (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<CreateDomainsApiCall> PrepareCreateDomainsAsync(DomainsRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Updates one or more domains in the DNS service.
        /// </summary>
        /// <param name="configuration">A <see cref="DnsUpdateConfiguration"/> object describing updates to apply to the domains.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/></item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="configuration"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/Modify_Domain_s_-d1e3848.html">Modify Domain(s) (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<UpdateDomainsApiCall> PrepareUpdateDomainsAsync(DomainsRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Removes one or more domains from the DNS service.
        /// </summary>
        /// <param name="domainIds">A collection of IDs for the domains to remove. These are obtained from <see cref="DnsDomain.Id">DnsDomain.Id</see>.</param>
        /// <param name="deleteSubdomains"><see langword="true"/> to delete any subdomains associated with the specified domains; otherwise, <see langword="false"/> to promote any subdomains to top-level domains.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsJob"/> object
        /// describing the asynchronous server operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/></item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="domainIds"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="domainIds"/> contains any <see langword="null"/> values.
        /// <para>-or-</para>
        /// <para>If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/Remove_Domain_s_-d1e4022.html">Remove</seealso>
        Task<RemoveDomainsApiCall> PrepareRemoveDomainsAsync(IEnumerable<DomainId> domainIds, CancellationToken cancellationToken);

        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/GET_searchDomains_v1.0__account__domains_search_domains.html">Search domain(s) (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<SearchDomainsApiCall> PrepareSearchDomainsAsync(string partialName, CancellationToken cancellationToken);

        /// <summary>
        /// Gets detailed information about a specific domain.
        /// </summary>
        /// <param name="domainId">The domain ID. This is obtained from <see cref="DnsDomain.Id">DnsDomain.Id</see>.</param>
        /// <param name="showRecords"><see langword="true"/> to populate the <see cref="DnsDomain.Records"/> property of the result; otherwise, <see langword="false"/>.</param>
        /// <param name="showSubdomains"><see langword="true"/> to populate the <see cref="DnsDomain.Subdomains"/> property of the result; otherwise, <see langword="false"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsDomain"/> object containing the DNS information for the requested domain.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="domainId"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/GET_listDomainsWithoutSubDomains_v1.0__account__domains__domainId__domains.html">List domain details without subdomains (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/GET_showDomain_v1.0__account__domains__domainId__domains.html">Show domain (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<GetDomainApiCall> PrepareGetDomainAsync(DomainId domainId, CancellationToken cancellationToken);

        Task<UpdateDomainApiCall> PrepareUpdateDomainAsync(DomainId domainId, DomainData domainData, CancellationToken cancellationToken);

        Task<RemoveDomainApiCall> PrepareRemoveDomainAsync(DomainId domainId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets information about all changes made to a domain since a specified time.
        /// </summary>
        /// <param name="domainId">The domain ID. This is obtained from <see cref="DnsDomain.Id">DnsDomain.Id</see>.</param>
        /// <param name="since">The timestamp of the earliest changes to consider. If this is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsDomainChanges"/> object describing the changes made to a domain registered in the DNS service.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="domainId"/> is <see langword="null"/>.</exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/GET_showDomainChanges_v1.0__account__domains__domainId__changes_domains.html">Show domain changes (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<ListDomainChangesApiCall> PrepareListDomainChangesAsync(DomainId domainId, CancellationToken cancellationToken);

        /// <summary>
        /// Exports a domain registered in the DNS service.
        /// </summary>
        /// <remarks>
        /// The exported domain represents a single domain, and does not include subdomains.
        ///
        /// <note>
        /// The <see cref="SerializedDomainFormat.Bind9"/> format does not support comments, so any
        /// comments associated with a domain or its records will not be included in the exported
        /// result.
        /// </note>
        /// </remarks>
        /// <param name="domainId">The domain ID. This is obtained from <see cref="DnsDomain.Id">DnsDomain.Id</see>.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsJob{TResponse}"/> object
        /// describing the asynchronous server operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/>: In this case the <see cref="DnsJob{TResponse}.Response"/>
        /// property contains an <see cref="ExportedDomain"/> object containing the details of the exported domain.</item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="domainId"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/GET_exportDomain_v1.0__account__domains__domainId__export_domains.html">Export domain (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<ExportDomainApiCall> PrepareExportDomainAsync(DomainId domainId, CancellationToken cancellationToken);

        /// <summary>
        /// Clones a domain registered in the DNS service, optionally cloning its subdomains as well.
        /// </summary>
        /// <param name="domainId">The domain ID. This is obtained from <see cref="DnsDomain.Id">DnsDomain.Id</see>.</param>
        /// <param name="cloneName">The name of the new (cloned) domain.</param>
        /// <param name="cloneSubdomains"><see langword="true"/> to recursively clone subdomains; otherwise, <see langword="false"/> to only clone the top-level domain and its records. Cloned subdomain configurations are modified the same way that cloned top-level domain configurations are modified. If this is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="modifyRecordData"><see langword="true"/> to replace occurrences of the reference domain name with the new domain name in comments on the cloned (new) domain. If this is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="modifyEmailAddress"><see langword="true"/> to replace occurrences of the reference domain name with the new domain name in email addresses on the cloned (new) domain. If this is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="modifyComment"><true>true</true> to replace occurrences of the reference domain name with the new domain name in data fields (of records) on the cloned (new) domain. Does not affect NS records. If this is <see langword="null"/>, a provider-specific default value is used.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsJob{TResponse}"/> object
        /// describing the asynchronous server operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/>: In this case the <see cref="DnsJob{TResponse}.Response"/>
        /// property contains a <see cref="DnsDomains"/> object containing the details of the cloned (new) domains.</item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="domainId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="cloneName"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="cloneName"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/clone_domain-dle846.html">Clone Domain (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<CloneDomainApiCall> PrepareCloneDomainAsync(DomainId domainId, string cloneName, CancellationToken cancellationToken);

        /// <summary>
        /// Imports domains into the DNS service.
        /// </summary>
        /// <param name="serializedDomains">A collection of <see cref="SerializedDomain"/> objects containing the serialized domain information to import.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsJob{TResponse}"/> object
        /// describing the asynchronous server operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/>: In this case the <see cref="DnsJob{TResponse}.Response"/>
        /// property contains a <see cref="DnsDomains"/> object containing the details of the imported domains.</item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="serializedDomains"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="serializedDomains"/> is contains any <see langword="null"/> values.
        /// <para>-or-</para>
        /// <para>If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/import_domain.html">Import Domain (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<ImportDomainsApiCall> PrepareImportDomainsAsync(SerializedDomainsRequest request, CancellationToken cancellationToken);

        #endregion

        #region Subdomains

        /// <summary>
        /// Gets information about subdomains currently associated with a domain in the DNS service.
        /// </summary>
        /// <param name="domainId">The top-level domain ID. This is obtained from <see cref="DnsDomain.Id">DnsDomain.Id</see>.</param>
        /// <param name="offset">The index of the last item in the previous page of results. If not specified, the list starts at the beginning.</param>
        /// <param name="limit">The maximum number of subdomains to return in a single page.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a tuple of the resulting collection of
        /// <see cref="DnsSubdomain"/> objects and the total number of domains in the list. If the total number of
        /// subdomains in the list is not available, the second element of the tuple will be <see langword="null"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="domainId"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="offset"/> is less than 0.
        /// <para>-or-</para>
        /// <para>If <paramref name="limit"/> is less than or equal to 0.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/List_Subdomains-d1e4295.html">List Subdomains (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<ListSubdomainsApiCall> PrepareListSubdomainsAsync(DomainId domainId, CancellationToken cancellationToken);

        #endregion

        #region Records

        /// <summary>
        /// Gets information about records currently associated with a domain in the DNS service, optionally filtering the results
        /// to include only records of a specific type, name, and/or data.
        /// </summary>
        /// <param name="domainId">The domain ID. This is obtained from <see cref="DnsDomain.Id">DnsDomain.Id</see>.</param>
        /// <param name="recordType">The specific record type to consider, or <see langword="null"/> to consider all record types.</param>
        /// <param name="recordName">The record name, which is matched to the <see cref="DnsRecord.Name"/> property, or <see langword="null"/> to consider all records.</param>
        /// <param name="recordData">The record data, which is matched to the <see cref="DnsRecord.Data"/> property, or <see langword="null"/> to consider all records.</param>
        /// <param name="offset">The index of the last item in the previous page of results. If not specified, the list starts at the beginning.</param>
        /// <param name="limit">The maximum number of records to return in a single page.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a tuple of the resulting collection of
        /// <see cref="DnsRecord"/> objects and the total number of records in the list. If the total number of
        /// records in the list is not available, the second element of the tuple will be <see langword="null"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="domainId"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="offset"/> is less than 0.
        /// <para>-or-</para>
        /// <para>If <paramref name="limit"/> is less than or equal to 0.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/List_Records-d1e4629.html">List Records (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/Search_Records-e338d7e0.html">Search Records (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<ListRecordsApiCall> PrepareListRecordsAsync(DomainId domainId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds records to a domain in the DNS service.
        /// </summary>
        /// <param name="domainId">The domain ID. This is obtained from <see cref="DnsDomain.Id">DnsDomain.Id</see>.</param>
        /// <param name="recordConfigurations">A collection of <see cref="DnsDomainRecordConfiguration"/> objects describing the records to add.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsJob{TResponse}"/> object
        /// describing the asynchronous server operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/>: In this case the <see cref="DnsJob{TResponse}.Response"/>
        /// property contains a <see cref="DnsRecordsList"/> object containing the details of the added records.</item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="domainId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="recordConfigurations"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="recordConfigurations"/> contains any <see langword="null"/> values.
        /// <para>-or-</para>
        /// <para>If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/Add_Records-d1e4895.html">Add Records (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<CreateRecordsApiCall> PrepareCreateRecordsAsync(DomainId domainId, RecordsRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Removes one or more domain records from the DNS service.
        /// </summary>
        /// <param name="domainId">The domain ID. This is obtained from <see cref="DnsDomain.Id">DnsDomain.Id</see>.</param>
        /// <param name="recordIds">A collection of IDs for the records to remove. These are obtained from <see cref="DnsRecord.Id">DnsRecord.Id</see>.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsJob"/> object
        /// describing the asynchronous server operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/></item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="domainId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="recordIds"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="recordIds"/> contains any <see langword="null"/> values.
        /// <para>-or-</para>
        /// <para>If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/Remove_Records-d1e5188.html">Remove Records (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<RemoveRecordsApiCall> PrepareRemoveRecordsAsync(DomainId domainId, IEnumerable<RecordId> recordIds, CancellationToken cancellationToken);

        /// <summary>
        /// Updates domain records in the DNS service.
        /// </summary>
        /// <param name="domainId">The domain ID. This is obtained from <see cref="DnsDomain.Id">DnsDomain.Id</see>.</param>
        /// <param name="recordConfigurations">A collection of <see cref="DnsDomainRecordUpdateConfiguration"/> objects describing the updates to apply to domain records.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/></item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="domainId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="recordConfigurations"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="recordConfigurations"/> contains any <see langword="null"/> values.
        /// <para>-or-</para>
        /// <para>If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/Modify_Records-d1e5033.html">Modify Records (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<UpdateRecordsApiCall> PrepareUpdateRecordsAsync(DomainId domainId, RecordsRequest request, CancellationToken cancellationToken);

        Task<RemoveRecordApiCall> PrepareRemoveRecordAsync(DomainId domainId, RecordId recordId, CancellationToken cancellationToken);

        Task<UpdateRecordApiCall> PrepareUpdateRecordAsync(DomainId domainId, RecordId recordId, RecordData data, CancellationToken cancellationToken);

        /// <summary>
        /// Gets detailed information about a specific DNS record.
        /// </summary>
        /// <param name="domainId">The domain ID. This is obtained from <see cref="DnsDomain.Id">DnsDomain.Id</see>.</param>
        /// <param name="recordId">The record ID. This is obtained from <see cref="DnsRecord.Id">DnsRecord.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsRecord"/> object containing the details of the specified DNS record.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="domainId"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="recordId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/List_Record_Details-d1e4770.html">List Record Details (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<GetRecordApiCall> PrepareGetRecordAsync(DomainId domainId, RecordId recordId, CancellationToken cancellationToken);

        #endregion

        #region Reverse DNS

        /// <summary>
        /// Gets information about reverse DNS records currently associated with a cloud resource in the DNS service.
        /// </summary>
        /// <param name="serviceName">The name of the service which owns the cloud resource. This is obtained from <see cref="ServiceCatalog.Name"/>.</param>
        /// <param name="deviceResourceUri">The absolute URI of the cloud resource.</param>
        /// <param name="offset">The index of the last item in the previous page of results. If not specified, the list starts at the beginning.</param>
        /// <param name="limit">The maximum number of records to return in a single page.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a tuple of the resulting collection of
        /// <see cref="DnsRecord"/> objects and the total number of domains in the list. If the total number of
        /// subdomains in the list is not available, the second element of the tuple will be <see langword="null"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="serviceName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="deviceResourceUri"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">If <paramref name="serviceName"/> is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="offset"/> is less than 0.
        /// <para>-or-</para>
        /// <para>If <paramref name="limit"/> is less than or equal to 0.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/ReverseDNS-123457000.html">List PTR Records (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<ListPtrRecordsApiCall> PrepareListPtrRecordsAsync(string serviceName, Uri deviceResourceUri, CancellationToken cancellationToken);

        /// <summary>
        /// Gets detailed information about a reverse DNS record currently associated with a cloud resource in the DNS service.
        /// </summary>
        /// <param name="serviceName">The name of the service which owns the cloud resource. This is obtained from <see cref="ServiceCatalog.Name"/>.</param>
        /// <param name="deviceResourceUri">The absolute URI of the cloud resource.</param>
        /// <param name="recordId">The record ID. This is obtained from <see cref="DnsRecord.Id">DnsRecord.Id</see>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <returns>A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully, the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsRecord"/> object containing the details of the specified reverse DNS record.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="serviceName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="deviceResourceUri"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="recordId"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="serviceName"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="recordId"/> is empty.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/ReverseDNS-123457001.html">List PTR Record Details (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<GetPtrRecordApiCall> PrepareGetPtrRecordAsync(string serviceName, Uri deviceResourceUri, RecordId recordId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds reverse DNS records to a cloud resource in the DNS service.
        /// </summary>
        /// <param name="serviceName">The name of the service which owns the cloud resource. This is obtained from <see cref="ServiceCatalog.Name"/>.</param>
        /// <param name="deviceResourceUri">The absolute URI of the cloud resource.</param>
        /// <param name="recordConfigurations">A collection of <see cref="DnsDomainRecordConfiguration"/> objects describing the records to add.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsJob{TResponse}"/> object
        /// describing the asynchronous server operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/>: In this case the <see cref="DnsJob{TResponse}.Response"/>
        /// property contains a <see cref="DnsRecordsList"/> object containing the details of the added records.</item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="serviceName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="deviceResourceUri"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="recordConfigurations"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="serviceName"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="recordConfigurations"/> contains any <see langword="null"/> values.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/ReverseDNS-123457003.html">Add PTR Records (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<CreatePtrRecordsApiCall> PrepareCreatePtrRecordsAsync(PtrRecordsRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Update reverse DNS records for a cloud resource in the DNS service.
        /// </summary>
        /// <param name="serviceName">The name of the service which owns the cloud resource. This is obtained from <see cref="ServiceCatalog.Name"/>.</param>
        /// <param name="deviceResourceUri">The absolute URI of the cloud resource.</param>
        /// <param name="recordConfigurations">A collection of <see cref="DnsDomainRecordUpdateConfiguration"/> objects describing the updates to apply to domain records.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/></item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="serviceName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="deviceResourceUri"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="recordConfigurations"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="serviceName"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="recordConfigurations"/> contains any <see langword="null"/> values.</para>
        /// <para>-or-</para>
        /// <para>If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/ReverseDNS-123457004.html">Modify PTR Records (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<UpdatePtrRecordsApiCall> PrepareUpdatePtrRecordsAsync(PtrRecordsRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Removes one or more reverse DNS records from the DNS service.
        /// </summary>
        /// <param name="serviceName">The name of the service which owns the cloud resource. This is obtained from <see cref="ServiceCatalog.Name"/>.</param>
        /// <param name="deviceResourceUri">The absolute URI of the cloud resource.</param>
        /// <param name="ipAddress">The specific record to remove. If this is <see langword="null"/>, all reverse DNS records associated with the specified device are removed.</param>
        /// <param name="completionOption">Specifies when the <see cref="Task"/> representing the asynchronous server operation should be considered complete.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that the task will observe.</param>
        /// <param name="progress">An optional callback object to receive progress notifications, if <paramref name="completionOption"/> is <see cref="AsyncCompletionOption.RequestCompleted"/>. If this is <see langword="null"/>, no progress notifications are sent.</param>
        /// <returns>
        /// A <see cref="Task"/> object representing the asynchronous operation. When the task completes successfully,
        /// the <see cref="Task{TResult}.Result"/> property will return a <see cref="DnsJob"/> object
        /// describing the asynchronous server operation. If <paramref name="completionOption"/> is
        /// <see cref="AsyncCompletionOption.RequestCompleted"/>, the job will additionally be in one of the following
        /// states.
        ///
        /// <list type="bullet">
        /// <item><see cref="DnsJobStatus.Completed"/></item>
        /// <item><see cref="DnsJobStatus.Error"/>: In this case the <see cref="DnsJob.Error"/> property provides
        /// additional information about the error which occurred during the asynchronous server operation.</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="serviceName"/> is <see langword="null"/>.
        /// <para>-or-</para>
        /// <para>If <paramref name="deviceResourceUri"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="serviceName"/> is empty.
        /// <para>-or-</para>
        /// <para>If <paramref name="completionOption"/> is not a valid <see cref="AsyncCompletionOption"/>.</para>
        /// </exception>
        /// <exception cref="WebException">If the REST request does not return successfully.</exception>
        /// <seealso href="http://docs.rackspace.com/cdns/api/v1.0/cdns-devguide/content/ReverseDNS-123457005.html">Remove PTR Records (Rackspace Cloud DNS Developer Guide - API v1.0)</seealso>
        Task<RemovePtrRecordsApiCall> PrepareRemovePtrRecordsAsync(string serviceName, Uri deviceResourceUri, CancellationToken cancellationToken);

        #endregion
    }
}
