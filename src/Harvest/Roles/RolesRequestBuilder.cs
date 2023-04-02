namespace Harvest.Roles;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Common.Requests;
using Models;

/// <summary>
/// Defines the builder for operations to manage roles.
/// </summary>
public class RolesRequestBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RolesRequestBuilder"/> class with the specified path parameters and request adapter.
    /// </summary>
    /// <param name="pathParameters">The default path parameters to use to build the request URL.</param>
    /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="pathParameters"/> or <paramref name="requestAdapter"/> is <see langword="null"/>.</exception>
    public RolesRequestBuilder(Dictionary<string, object> pathParameters, HarvestRequestAdapter requestAdapter)
    {
        _ = pathParameters ?? throw new ArgumentNullException(nameof(pathParameters));
        _ = requestAdapter ?? throw new ArgumentNullException(nameof(requestAdapter));

        this.UrlTemplate = "{+baseurl}/roles{?page,per_page}";
        this.PathParameters = new Dictionary<string, object>(pathParameters);
        this.RequestAdapter = requestAdapter;
    }

    /// <summary>
    /// Gets the path parameters to use to build the request URL.
    /// </summary>
    private Dictionary<string, object> PathParameters { get; }

    /// <summary>
    /// Gets the request adapter to use to execute the requests.
    /// </summary>
    private HarvestRequestAdapter RequestAdapter { get; }

    /// <summary>
    /// Gets the URL template to use to build the request URL.
    /// </summary>
    private string UrlTemplate { get; }

    /// <summary>
    /// Gets the builder for operations to manage a specific role.
    /// </summary>
    /// <param name="roleId">The ID of the role.</param>
    /// <returns>A builder for operations to manage a specific role.</returns>
    public RoleRequestBuilder this[long roleId]
    {
        get
        {
            var urlTemplateParams = new Dictionary<string, object>(this.PathParameters) { { "roleid", roleId } };
            return new RoleRequestBuilder(urlTemplateParams, this.RequestAdapter);
        }
    }

    /// <summary>
    /// Retrieves a list of roles.
    /// </summary>
    /// <remarks>
    /// For more information: https://help.getharvest.com/api-v2/roles-api/roles/roles/#list-all-roles
    /// </remarks>
    /// <param name="requestConfiguration">The configuration for the request such as headers.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>A collection of roles.</returns>
    /// <exception cref="HttpRequestException">Thrown when the request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
    public async Task<RolesResponse> GetAsync(
        Action<RolesRequestBuilderGetRequestConfiguration> requestConfiguration = default,
        CancellationToken cancellationToken = default)
    {
        RequestInformation requestInfo = this.ToGetRequestInformation(requestConfiguration);
        return await this.RequestAdapter.SendAsync<RolesResponse>(requestInfo, cancellationToken);
    }

    /// <summary>
    /// Creates a new role.
    /// </summary>
    /// <remarks>
    /// For more information: https://help.getharvest.com/api-v2/roles-api/roles/roles/#create-a-role
    /// </remarks>
    /// <param name="body">The role to create.</param>
    /// <param name="requestConfiguration">The configuration for the request such as headers.</param>
    /// <param name="cancellationToken">The optional cancellation token.</param>
    /// <returns>The created role.</returns>
    /// <exception cref="HttpRequestException">Thrown when the request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="body"/> is <see langword="null"/>.</exception>
    public async Task<Role> PostAsync(
        CreateRole body,
        Action<RolesRequestBuilderPostRequestConfiguration> requestConfiguration = default,
        CancellationToken cancellationToken = default)
    {
        RequestInformation requestInfo = this.ToPostRequestInformation(body, requestConfiguration);
        return await this.RequestAdapter.SendAsync<Role>(requestInfo, cancellationToken);
    }

    /// <summary>
    /// Builds the request to retrieve a list of roles.
    /// </summary>
    /// <param name="requestConfiguration">The configuration for the request such as headers.</param>
    /// <returns>A request information object.</returns>
    public RequestInformation ToGetRequestInformation(Action<RolesRequestBuilderGetRequestConfiguration> requestConfiguration)
    {
        var requestInfo = new RequestInformation
        {
            HttpMethod = Method.GET,
            UrlTemplate = this.UrlTemplate,
            PathParameters = this.PathParameters,
        };

        requestInfo.Headers.Add("User-Agent", "HarvestDotnetSdk");
        requestInfo.Headers.Add("Accept", "application/json");

        if (requestConfiguration == null)
        {
            return requestInfo;
        }

        var requestConfig = new RolesRequestBuilderGetRequestConfiguration();
        requestConfiguration.Invoke(requestConfig);
        requestInfo.AddQueryParameters(requestConfig.QueryParameters);
        requestInfo.AddHeaders(requestConfig.Headers);

        return requestInfo;
    }

    /// <summary>
    /// Builds the request to create a role.
    /// </summary>
    /// <param name="body">The request body.</param>
    /// <param name="requestConfiguration">The configuration for the request such as headers.</param>
    /// <returns>A request information object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="body"/> is <see langword="null"/>.</exception>
    public RequestInformation ToPostRequestInformation(CreateRole body, Action<RolesRequestBuilderPostRequestConfiguration> requestConfiguration)
    {
        _ = body ?? throw new ArgumentNullException(nameof(body));
        var requestInfo = new RequestInformation
        {
            HttpMethod = Method.POST,
            UrlTemplate = this.UrlTemplate,
            PathParameters = this.PathParameters
        };

        requestInfo.Headers.Add("User-Agent", "HarvestDotnetSdk");
        requestInfo.Headers.Add("Accept", "application/json");

        requestInfo.SetJsonContent(body);

        if (requestConfiguration == null)
        {
            return requestInfo;
        }

        var requestConfig = new RolesRequestBuilderPostRequestConfiguration();
        requestConfiguration.Invoke(requestConfig);
        requestInfo.AddHeaders(requestConfig.Headers);

        return requestInfo;
    }

    /// <summary>
    /// Defines the configuration for the request to retrieve a list of roles.
    /// </summary>
    public class RolesRequestBuilderGetRequestConfiguration : RequestConfiguration
    {
        /// <summary>
        /// Gets or sets the query parameters for the request.
        /// </summary>
        public RolesRequestBuilderGetQueryParameters QueryParameters { get; set; } = new();
    }

    /// <summary>
    /// Defines the configuration for the request to create a role.
    /// </summary>
    public class RolesRequestBuilderPostRequestConfiguration : RequestConfiguration
    {
    }

    /// <summary>
    /// Defines the query parameters for the request to retrieve a list of roles.
    /// </summary>
    public class RolesRequestBuilderGetQueryParameters : PaginatedQueryParameters
    {
    }
}