/*
 * SmartThings API
 *
 * # Overview  This is the reference documentation for the SmartThings API.  The SmartThings API, a RESTful API, provides a method for your integration to communicate with the SmartThings Platform. The API is the core of the platform. It is used to control devices, create Automations, manage Locations, retrieve user and device information; if you want to communicate with the SmartThings platform, you’ll be using the SmartThings API. All responses are sent as [JSON](http://www.json.org/).  The SmartThings API consists of several endpoints, including Rules, Locations, Devices, and more. Even though each of these endpoints are not standalone APIs, you may hear them referred to as such. For example, the Rules API is used to build Automations.  # Authentication  Before using the SmartThings API, you’ll need to obtain an Authorization Token. All SmartThings resources are protected with [OAuth 2.0 Bearer Tokens](https://tools.ietf.org/html/rfc6750#section-2.1) sent on the request as an `Authorization: Bearer <TOKEN>` header. Operations require specific OAuth scopes that specify the exact permissions authorized by the user.  ## Authorization token types  There are two types of tokens:   * SmartApp tokens   * Personal access tokens (PAT).  ### SmartApp tokens  SmartApp tokens are used to communicate between third-party integrations, or SmartApps, and the SmartThings API. When a SmartApp is called by the SmartThings platform, it is sent an authorization token that can be used to interact with the SmartThings API.  ### Personal access tokens  Personal access tokens are used to authorize interaction with the API for non-SmartApp use cases. When creating personal access tokens, you can specifiy the permissions granted to the token. These permissions define the OAuth2 scopes for the personal access token.  Personal access tokesn can be created and managed on the [personal access tokens page](https://account.smartthings.com/tokens).  ## OAuth2 scopes  Operations may be protected by one or more OAuth security schemes, which specify the required permissions. Each scope for a given scheme is required. If multiple schemes are specified (uncommon), you may use either scheme.  SmartApp token scopes are derived from the permissions requested by the SmartApp and granted by the end-user during installation. Personal access token scopes are associated with the specific permissions authorized when the token is created.  Scopes are generally in the form `permission:entity-type:entity-id`.  **An `*` used for the `entity-id` specifies that the permission may be applied to all entities that the token type has access to, or may be replaced with a specific ID.**  For more information about authrization and permissions, visit the [Authorization section](https://developer-preview.smartthings.com/docs/advanced/authorization-and-permissions) in the SmartThings documentation.  <!- - ReDoc-Inject: <security-definitions> - ->  # Errors  The SmartThings API uses conventional HTTP response codes to indicate the success or failure of a request.  In general:  * A `2XX` response code indicates success * A `4XX` response code indicates an error given the inputs for the request * A `5XX` response code indicates a failure on the SmartThings platform  API errors will contain a JSON response body with more information about the error:  ```json {   \"requestId\": \"031fec1a-f19f-470a-a7da-710569082846\"   \"error\": {     \"code\": \"ConstraintViolationError\",     \"message\": \"Validation errors occurred while process your request.\",     \"details\": [       { \"code\": \"PatternError\", \"target\": \"latitude\", \"message\": \"Invalid format.\" },       { \"code\": \"SizeError\", \"target\": \"name\", \"message\": \"Too small.\" },       { \"code\": \"SizeError\", \"target\": \"description\", \"message\": \"Too big.\" }     ]   } } ```  ## Error Response Body  The error response attributes are:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | requestId | String | No | A request identifier that can be used to correlate an error to additional logging on the SmartThings servers. | error | Error | **Yes** | The Error object, documented below.  ## Error Object  The Error object contains the following attributes:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | code | String | **Yes** | A SmartThings-defined error code that serves as a more specific indicator of the error than the HTTP error code specified in the response. See [SmartThings Error Codes](#section/Errors/SmartThings-Error-Codes) for more information. | message | String | **Yes** | A description of the error, intended to aid debugging of error responses. | target | String | No | The target of the error. For example, this could be the name of the property that caused the error. | details | Error[] | No | An array of Error objects that typically represent distinct, related errors that occurred during the request. As an optional attribute, this may be null or an empty array.  ## Standard HTTP Error Codes  The following table lists the most common HTTP error responses:  | Code | Name | Description | | - -- | - -- | - -- | | 400 | Bad Request | The client has issued an invalid request. This is commonly used to specify validation errors in a request payload. | 401 | Unauthorized | Authorization for the API is required, but the request has not been authenticated. | 403 | Forbidden | The request has been authenticated but does not have appropriate permissions, or a requested resource is not found. | 404 | Not Found | The requested path does not exist. | 406 | Not Acceptable | The client has requested a MIME type via the Accept header for a value not supported by the server. | 415 | Unsupported Media Type | The client has defined a contentType header that is not supported by the server. | 422 | Unprocessable Entity | The client has made a valid request, but the server cannot process it. This is often used for APIs for which certain limits have been exceeded. | 429 | Too Many Requests | The client has exceeded the number of requests allowed for a given time window. | 500 | Internal Server Error | An unexpected error on the SmartThings servers has occurred. These errors are generally rare. | 501 | Not Implemented | The client request was valid and understood by the server, but the requested feature has yet to be implemented. These errors are generally rare.  ## SmartThings Error Codes  SmartThings specifies several standard custom error codes. These provide more information than the standard HTTP error response codes. The following table lists the standard SmartThings error codes and their descriptions:  | Code | Typical HTTP Status Codes | Description | | - -- | - -- | - -- | | PatternError | 400, 422 | The client has provided input that does not match the expected pattern. | ConstraintViolationError | 422 | The client has provided input that has violated one or more constraints. | NotNullError | 422 | The client has provided a null input for a field that is required to be non-null. | NullError | 422 | The client has provided an input for a field that is required to be null. | NotEmptyError | 422 | The client has provided an empty input for a field that is required to be non-empty. | SizeError | 400, 422 | The client has provided a value that does not meet size restrictions. | Unexpected Error | 500 | A non-recoverable error condition has occurred. A problem occurred on the SmartThings server that is no fault of the client. | UnprocessableEntityError | 422 | The client has sent a malformed request body. | TooManyRequestError | 429 | The client issued too many requests too quickly. | LimitError | 422 | The client has exceeded certain limits an API enforces. | UnsupportedOperationError | 400, 422 | The client has issued a request to a feature that currently isn't supported by the SmartThings platform. These errors are generally rare.  ## Custom Error Codes  An API may define its own error codes where appropriate. Custom error codes are documented in each API endpoint's documentation section.  # Warnings The SmartThings API issues warning messages via standard HTTP Warning headers. These messages do not represent a request failure, but provide additional information that the requester might want to act upon. For example, a warning will be issued if you are using an old API version.  # API Versions  The SmartThings API supports both path and header-based versioning. The following are equivalent:  - https://api.smartthings.com/v1/locations - https://api.smartthings.com/locations with header `Accept: application/vnd.smartthings+json;v=1`  Currently, only version 1 is available.  # Paging  Operations that return a list of objects return a paginated response. The `_links` object contains the items returned, and links to the next and previous result page, if applicable.  ```json {   \"items\": [     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f941de4fbf9\",       \"name\": \"Home\"     },     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f94d6g4fbf9\",       \"name\": \"Work\"     }     ....   ],   \"_links\": {     \"next\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=3\"     },     \"previous\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=1\"     }   } } ```  # Allowed Permissions  The response payload to a request for a SmartThings entity (e.g. a location or a device) may contain an `allowed` list property.  This list contains strings called **action identifiers** (such as `w:locations`) that provide information about what actions are permitted by the user's token on that entity at the time the request was processed.  The action identifiers are defined in the API documentation for the 200 response of the particular endpoint queried. For each documented action identifier (e.g. `w:locations`), you will find a description of the user action (e.g. \"edit the name of the location\") associated with that identifier. The endpoint documentation will contain a complete list of the action identifiers that may appear in the allowed list property.  If the `allowed` list property is present in the response payload: * the user action documented for an action identifier present in the list is permitted for the user on the entity at the time the request is processed * the user action documented for an action identifier **not** present in the list is **not** permitted to the user on the entity at the time the request is processed  If the `allowed` list property is **not** present or has a `null` value in the response payload, then the response provides **no information** about any user actions being permissible except that the user has permission to view the returned entity.  The response provides **no information** about the permissibility of user actions that are not specifically mentioned in the documentation for the particular endpoint.  The table below is a high-level guide to interpreting action identifiers.  It does not indicate that any given endpoint will document or return any of the action identifiers listed below. Remember that the endpoint API documentation is the final source of truth for interpreting action identifiers in a response payload.  | Action Identifier Format | Examples | Meaning | | - -- | - -- | - -- | | `w:grant:`\\<grant type> | `w:grant:share` on a location payload | User may **bestow** the grant type on the entity to another user (e.g. through an invitation.) | | `r:`\\<child type> | `r:devices` on a location payload | User may **list and view** child type entities of the returned entity.  NOTE: there may be finer-grained controls on the child type entities. | | `l:`\\<child type> | `l:devices` on a location payload | User may **list and summarize** child type entities of the returned entity.  NOTE: there may be finer-grained controls on the child type entities.  This is weaker than `r:`\\<child type> and rarely used. | | `w:`\\<child type> | `w:devices` on a location payload | User may **create** entities of the child type as children of the returned entity. | | `x:`\\<child type> | `x:devices` on a location payload | User may **execute commands** on child type entities of the returned entity.  NOTE: there may be finer-grained controls on the child type entities. | | `r:`\\<entity type> | `r:locations` on a location payload | This will only be returned in a list/summary response and only in a case when the list/summary is designed not to show all the details of the entity. | | `w:`\\<entity type> | `w:locations` on a location payload<br/>`w:devices` on a device payload | User may **edit** specificly-documented properties of the returned entity. | | `x:`\\<entity type> | `x:devices` on a device payload | user may **execute commands** on the returned entity.  NOTE: there may be finer-grained controls on the children of the entity. | | `d:`\\<entity type> | `d:locations` on a location payload | User may **delete** the returned entity. | | `r:`\\<entity type>`:`\\<attribute group> | `r:locations:currentMode` on a location payload | User may **view** the specified (and clearly-documented) attribute or attribute group of the returned entity. | | `w:`\\<entity type>`:`\\<attribute group> | `w:locations:geo` on a location payload | User may **edit** the specified (and clearly-documented) attribute or attribute group of the returned entity. | | `x:`\\<entity type>`:`\\<attribute group> | `x:devices:switch` on a device payload | User may **execute commands** on the specified (and clearly-documented) attribute or attribute group of the returned entity. |  # Localization  Some SmartThings APIs support localization. Specific information regarding localization endpoints are documented in the API itself. However, the following applies to all endpoints that support localization.  ## Fallback Patterns  When making a request with the `Accept-Language` header, the following fallback pattern is observed: 1. Response will be translated with exact locale tag. 2. If a translation does not exist for the requested language and region, the translation for the language will be returned. 3. If a translation does not exist for the language, English (en) will be returned. 4. Finally, an untranslated response will be returned in the absense of the above translations.  ## Accept-Language Header The format of the `Accept-Language` header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5)  ## Content-Language The `Content-Language` header should be set on the response from the server to indicate which translation was given back to the client. The absense of the header indicates that the server did not recieve a request with the `Accept-Language` header set. 
 *
 * The version of the OpenAPI document: 1.0-PREVIEW
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace SmartThingsNet.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ICoordinateApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// delete a subscription by installed app ID 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="isaId">installed app ID </param>
        /// <returns></returns>
        void DeleteSubscriptionByIsaId(string authorization, string locationId, string isaId);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// delete a subscription by installed app ID 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="isaId">installed app ID </param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteSubscriptionByIsaIdWithHttpInfo(string authorization, string locationId, string isaId);
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Delete a subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <returns>ServiceDeleteSubscription</returns>
        ServiceDeleteSubscription DeleteSubscriptionBySubscriptionId(string locationId, string subscriptionId, string authorization);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Delete a subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <returns>ApiResponse of ServiceDeleteSubscription</returns>
        ApiResponse<ServiceDeleteSubscription> DeleteSubscriptionBySubscriptionIdWithHttpInfo(string locationId, string subscriptionId, string authorization);
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Redirect to condent provider&#39;s alert detail link.  If no query given, coordinate of the location ID is used.  Otherwise the coordinate of geo place or subscription will be used. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTUnits">prefered units(metrics or empiral) of the response content, default to metrics (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="geoplaceId">The ID of the Geo Place (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="subscriptionId">The ID of the Nimbus subscription (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <returns>ErrorResponse</returns>
        ErrorResponse GetAlertLinkByLocationId(string locationId, string authorization, string xSTUnits = default(string), string acceptLanguage = default(string), string geoplaceId = default(string), string subscriptionId = default(string));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Redirect to condent provider&#39;s alert detail link.  If no query given, coordinate of the location ID is used.  Otherwise the coordinate of geo place or subscription will be used. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTUnits">prefered units(metrics or empiral) of the response content, default to metrics (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="geoplaceId">The ID of the Geo Place (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="subscriptionId">The ID of the Nimbus subscription (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <returns>ApiResponse of ErrorResponse</returns>
        ApiResponse<ErrorResponse> GetAlertLinkByLocationIdWithHttpInfo(string locationId, string authorization, string xSTUnits = default(string), string acceptLanguage = default(string), string geoplaceId = default(string), string subscriptionId = default(string));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Gets all capability data given a location ID
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="geoplaceId">Geo place ID of a specific alert.  To use of this query, query \&quot;name&#x3D;alert\&quot; is REQUIRED and not to combine with any other capabilty  (optional)</param>
        /// <param name="name">Comma separated string of capabilities (alert, weather, airQuality, forecast, airQualityForcast). For example, \&quot;weather,airQuality\&quot;  (optional)</param>
        /// <returns>ServiceCapabilityData</returns>
        ServiceCapabilityData GetCapabilityByLocationId(string locationId, string authorization, string geoplaceId = default(string), string name = default(string));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Gets all capability data given a location ID
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="geoplaceId">Geo place ID of a specific alert.  To use of this query, query \&quot;name&#x3D;alert\&quot; is REQUIRED and not to combine with any other capabilty  (optional)</param>
        /// <param name="name">Comma separated string of capabilities (alert, weather, airQuality, forecast, airQualityForcast). For example, \&quot;weather,airQuality\&quot;  (optional)</param>
        /// <returns>ApiResponse of ServiceCapabilityData</returns>
        ApiResponse<ServiceCapabilityData> GetCapabilityByLocationIdWithHttpInfo(string locationId, string authorization, string geoplaceId = default(string), string name = default(string));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get specified location ID information
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <returns>ServiceLocationInfo</returns>
        ServiceLocationInfo GetServiceLocationInfoByLocationId(string locationId, string authorization);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get specified location ID information
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <returns>ApiResponse of ServiceLocationInfo</returns>
        ApiResponse<ServiceLocationInfo> GetServiceLocationInfoByLocationIdWithHttpInfo(string locationId, string authorization);
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Create a new subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <returns>ServiceNewSubscription</returns>
        ServiceNewSubscription PostSubscriptionByLocationId(string authorization, string locationId, ServiceSubscriptionBody serviceSubscriptionBody);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Create a new subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <returns>ApiResponse of ServiceNewSubscription</returns>
        ApiResponse<ServiceNewSubscription> PostSubscriptionByLocationIdWithHttpInfo(string authorization, string locationId, ServiceSubscriptionBody serviceSubscriptionBody);
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Update a subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <returns></returns>
        void PutSubscriptionBySubscriptionId(string locationId, string subscriptionId, string authorization, ServiceSubscriptionBody serviceSubscriptionBody);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Update a subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> PutSubscriptionBySubscriptionIdWithHttpInfo(string locationId, string subscriptionId, string authorization, ServiceSubscriptionBody serviceSubscriptionBody);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ICoordinateApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// delete a subscription by installed app ID 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="isaId">installed app ID </param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteSubscriptionByIsaIdAsync(string authorization, string locationId, string isaId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// delete a subscription by installed app ID 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="isaId">installed app ID </param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteSubscriptionByIsaIdWithHttpInfoAsync(string authorization, string locationId, string isaId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Delete a subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ServiceDeleteSubscription</returns>
        System.Threading.Tasks.Task<ServiceDeleteSubscription> DeleteSubscriptionBySubscriptionIdAsync(string locationId, string subscriptionId, string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Delete a subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ServiceDeleteSubscription)</returns>
        System.Threading.Tasks.Task<ApiResponse<ServiceDeleteSubscription>> DeleteSubscriptionBySubscriptionIdWithHttpInfoAsync(string locationId, string subscriptionId, string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Redirect to condent provider&#39;s alert detail link.  If no query given, coordinate of the location ID is used.  Otherwise the coordinate of geo place or subscription will be used. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTUnits">prefered units(metrics or empiral) of the response content, default to metrics (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="geoplaceId">The ID of the Geo Place (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="subscriptionId">The ID of the Nimbus subscription (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ErrorResponse</returns>
        System.Threading.Tasks.Task<ErrorResponse> GetAlertLinkByLocationIdAsync(string locationId, string authorization, string xSTUnits = default(string), string acceptLanguage = default(string), string geoplaceId = default(string), string subscriptionId = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Redirect to condent provider&#39;s alert detail link.  If no query given, coordinate of the location ID is used.  Otherwise the coordinate of geo place or subscription will be used. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTUnits">prefered units(metrics or empiral) of the response content, default to metrics (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="geoplaceId">The ID of the Geo Place (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="subscriptionId">The ID of the Nimbus subscription (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ErrorResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<ErrorResponse>> GetAlertLinkByLocationIdWithHttpInfoAsync(string locationId, string authorization, string xSTUnits = default(string), string acceptLanguage = default(string), string geoplaceId = default(string), string subscriptionId = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Gets all capability data given a location ID
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="geoplaceId">Geo place ID of a specific alert.  To use of this query, query \&quot;name&#x3D;alert\&quot; is REQUIRED and not to combine with any other capabilty  (optional)</param>
        /// <param name="name">Comma separated string of capabilities (alert, weather, airQuality, forecast, airQualityForcast). For example, \&quot;weather,airQuality\&quot;  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ServiceCapabilityData</returns>
        System.Threading.Tasks.Task<ServiceCapabilityData> GetCapabilityByLocationIdAsync(string locationId, string authorization, string geoplaceId = default(string), string name = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Gets all capability data given a location ID
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="geoplaceId">Geo place ID of a specific alert.  To use of this query, query \&quot;name&#x3D;alert\&quot; is REQUIRED and not to combine with any other capabilty  (optional)</param>
        /// <param name="name">Comma separated string of capabilities (alert, weather, airQuality, forecast, airQualityForcast). For example, \&quot;weather,airQuality\&quot;  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ServiceCapabilityData)</returns>
        System.Threading.Tasks.Task<ApiResponse<ServiceCapabilityData>> GetCapabilityByLocationIdWithHttpInfoAsync(string locationId, string authorization, string geoplaceId = default(string), string name = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get specified location ID information
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ServiceLocationInfo</returns>
        System.Threading.Tasks.Task<ServiceLocationInfo> GetServiceLocationInfoByLocationIdAsync(string locationId, string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get specified location ID information
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ServiceLocationInfo)</returns>
        System.Threading.Tasks.Task<ApiResponse<ServiceLocationInfo>> GetServiceLocationInfoByLocationIdWithHttpInfoAsync(string locationId, string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Create a new subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ServiceNewSubscription</returns>
        System.Threading.Tasks.Task<ServiceNewSubscription> PostSubscriptionByLocationIdAsync(string authorization, string locationId, ServiceSubscriptionBody serviceSubscriptionBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Create a new subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ServiceNewSubscription)</returns>
        System.Threading.Tasks.Task<ApiResponse<ServiceNewSubscription>> PostSubscriptionByLocationIdWithHttpInfoAsync(string authorization, string locationId, ServiceSubscriptionBody serviceSubscriptionBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Update a subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task PutSubscriptionBySubscriptionIdAsync(string locationId, string subscriptionId, string authorization, ServiceSubscriptionBody serviceSubscriptionBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Update a subscription 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> PutSubscriptionBySubscriptionIdWithHttpInfoAsync(string locationId, string subscriptionId, string authorization, ServiceSubscriptionBody serviceSubscriptionBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ICoordinateApi : ICoordinateApiSync, ICoordinateApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class CoordinateApi : ICoordinateApi
    {
        private SmartThingsNet.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateApi"/> class.
        /// </summary>
        /// <returns></returns>
        public CoordinateApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateApi"/> class.
        /// </summary>
        /// <returns></returns>
        public CoordinateApi(string basePath)
        {
            this.Configuration = SmartThingsNet.Client.Configuration.MergeConfigurations(
                SmartThingsNet.Client.GlobalConfiguration.Instance,
                new SmartThingsNet.Client.Configuration { BasePath = basePath }
            );
            this.Client = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = SmartThingsNet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public CoordinateApi(SmartThingsNet.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = SmartThingsNet.Client.Configuration.MergeConfigurations(
                SmartThingsNet.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = SmartThingsNet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public CoordinateApi(SmartThingsNet.Client.ISynchronousClient client, SmartThingsNet.Client.IAsynchronousClient asyncClient, SmartThingsNet.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = SmartThingsNet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public SmartThingsNet.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public SmartThingsNet.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public SmartThingsNet.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public SmartThingsNet.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        ///  delete a subscription by installed app ID 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="isaId">installed app ID </param>
        /// <returns></returns>
        public void DeleteSubscriptionByIsaId(string authorization, string locationId, string isaId)
        {
            DeleteSubscriptionByIsaIdWithHttpInfo(authorization, locationId, isaId);
        }

        /// <summary>
        ///  delete a subscription by installed app ID 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="isaId">installed app ID </param>
        /// <returns>ApiResponse of Object(void)</returns>
        public SmartThingsNet.Client.ApiResponse<Object> DeleteSubscriptionByIsaIdWithHttpInfo(string authorization, string locationId, string isaId)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->DeleteSubscriptionByIsaId");
            }

            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->DeleteSubscriptionByIsaId");
            }

            // verify the required parameter 'isaId' is set
            if (isaId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'isaId' when calling CoordinateApi->DeleteSubscriptionByIsaId");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "isaId", isaId));
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/services/coordinate/locations/{locationId}/subscriptions", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteSubscriptionByIsaId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  delete a subscription by installed app ID 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="isaId">installed app ID </param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteSubscriptionByIsaIdAsync(string authorization, string locationId, string isaId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteSubscriptionByIsaIdWithHttpInfoAsync(authorization, locationId, isaId, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        ///  delete a subscription by installed app ID 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="isaId">installed app ID </param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> DeleteSubscriptionByIsaIdWithHttpInfoAsync(string authorization, string locationId, string isaId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->DeleteSubscriptionByIsaId");
            }

            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->DeleteSubscriptionByIsaId");
            }

            // verify the required parameter 'isaId' is set
            if (isaId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'isaId' when calling CoordinateApi->DeleteSubscriptionByIsaId");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "isaId", isaId));
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/services/coordinate/locations/{locationId}/subscriptions", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteSubscriptionByIsaId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Delete a subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <returns>ServiceDeleteSubscription</returns>
        public ServiceDeleteSubscription DeleteSubscriptionBySubscriptionId(string locationId, string subscriptionId, string authorization)
        {
            SmartThingsNet.Client.ApiResponse<ServiceDeleteSubscription> localVarResponse = DeleteSubscriptionBySubscriptionIdWithHttpInfo(locationId, subscriptionId, authorization);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Delete a subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <returns>ApiResponse of ServiceDeleteSubscription</returns>
        public SmartThingsNet.Client.ApiResponse<ServiceDeleteSubscription> DeleteSubscriptionBySubscriptionIdWithHttpInfo(string locationId, string subscriptionId, string authorization)
        {
            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->DeleteSubscriptionBySubscriptionId");
            }

            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'subscriptionId' when calling CoordinateApi->DeleteSubscriptionBySubscriptionId");
            }

            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->DeleteSubscriptionBySubscriptionId");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            localVarRequestOptions.PathParameters.Add("subscriptionId", SmartThingsNet.Client.ClientUtils.ParameterToString(subscriptionId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<ServiceDeleteSubscription>("/services/coordinate/locations/{locationId}/subscriptions/{subscriptionId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteSubscriptionBySubscriptionId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Delete a subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ServiceDeleteSubscription</returns>
        public async System.Threading.Tasks.Task<ServiceDeleteSubscription> DeleteSubscriptionBySubscriptionIdAsync(string locationId, string subscriptionId, string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<ServiceDeleteSubscription> localVarResponse = await DeleteSubscriptionBySubscriptionIdWithHttpInfoAsync(locationId, subscriptionId, authorization, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Delete a subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ServiceDeleteSubscription)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<ServiceDeleteSubscription>> DeleteSubscriptionBySubscriptionIdWithHttpInfoAsync(string locationId, string subscriptionId, string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->DeleteSubscriptionBySubscriptionId");
            }

            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'subscriptionId' when calling CoordinateApi->DeleteSubscriptionBySubscriptionId");
            }

            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->DeleteSubscriptionBySubscriptionId");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            localVarRequestOptions.PathParameters.Add("subscriptionId", SmartThingsNet.Client.ClientUtils.ParameterToString(subscriptionId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<ServiceDeleteSubscription>("/services/coordinate/locations/{locationId}/subscriptions/{subscriptionId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteSubscriptionBySubscriptionId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Redirect to condent provider&#39;s alert detail link.  If no query given, coordinate of the location ID is used.  Otherwise the coordinate of geo place or subscription will be used. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTUnits">prefered units(metrics or empiral) of the response content, default to metrics (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="geoplaceId">The ID of the Geo Place (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="subscriptionId">The ID of the Nimbus subscription (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <returns>ErrorResponse</returns>
        public ErrorResponse GetAlertLinkByLocationId(string locationId, string authorization, string xSTUnits = default(string), string acceptLanguage = default(string), string geoplaceId = default(string), string subscriptionId = default(string))
        {
            SmartThingsNet.Client.ApiResponse<ErrorResponse> localVarResponse = GetAlertLinkByLocationIdWithHttpInfo(locationId, authorization, xSTUnits, acceptLanguage, geoplaceId, subscriptionId);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Redirect to condent provider&#39;s alert detail link.  If no query given, coordinate of the location ID is used.  Otherwise the coordinate of geo place or subscription will be used. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTUnits">prefered units(metrics or empiral) of the response content, default to metrics (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="geoplaceId">The ID of the Geo Place (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="subscriptionId">The ID of the Nimbus subscription (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <returns>ApiResponse of ErrorResponse</returns>
        public SmartThingsNet.Client.ApiResponse<ErrorResponse> GetAlertLinkByLocationIdWithHttpInfo(string locationId, string authorization, string xSTUnits = default(string), string acceptLanguage = default(string), string geoplaceId = default(string), string subscriptionId = default(string))
        {
            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->GetAlertLinkByLocationId");
            }

            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->GetAlertLinkByLocationId");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            if (geoplaceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "geoplaceId", geoplaceId));
            }
            if (subscriptionId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "subscriptionId", subscriptionId));
            }
            if (xSTUnits != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Units", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTUnits)); // header parameter
            }
            if (acceptLanguage != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept-Language", SmartThingsNet.Client.ClientUtils.ParameterToString(acceptLanguage)); // header parameter
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ErrorResponse>("/services/coordinate/locations/{locationId}/link/alert", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetAlertLinkByLocationId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Redirect to condent provider&#39;s alert detail link.  If no query given, coordinate of the location ID is used.  Otherwise the coordinate of geo place or subscription will be used. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTUnits">prefered units(metrics or empiral) of the response content, default to metrics (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="geoplaceId">The ID of the Geo Place (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="subscriptionId">The ID of the Nimbus subscription (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ErrorResponse</returns>
        public async System.Threading.Tasks.Task<ErrorResponse> GetAlertLinkByLocationIdAsync(string locationId, string authorization, string xSTUnits = default(string), string acceptLanguage = default(string), string geoplaceId = default(string), string subscriptionId = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<ErrorResponse> localVarResponse = await GetAlertLinkByLocationIdWithHttpInfoAsync(locationId, authorization, xSTUnits, acceptLanguage, geoplaceId, subscriptionId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Redirect to condent provider&#39;s alert detail link.  If no query given, coordinate of the location ID is used.  Otherwise the coordinate of geo place or subscription will be used. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTUnits">prefered units(metrics or empiral) of the response content, default to metrics (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="geoplaceId">The ID of the Geo Place (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="subscriptionId">The ID of the Nimbus subscription (geoplaceId and subscirptionId cannot be combined) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ErrorResponse)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<ErrorResponse>> GetAlertLinkByLocationIdWithHttpInfoAsync(string locationId, string authorization, string xSTUnits = default(string), string acceptLanguage = default(string), string geoplaceId = default(string), string subscriptionId = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->GetAlertLinkByLocationId");
            }

            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->GetAlertLinkByLocationId");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            if (geoplaceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "geoplaceId", geoplaceId));
            }
            if (subscriptionId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "subscriptionId", subscriptionId));
            }
            if (xSTUnits != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Units", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTUnits)); // header parameter
            }
            if (acceptLanguage != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept-Language", SmartThingsNet.Client.ClientUtils.ParameterToString(acceptLanguage)); // header parameter
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ErrorResponse>("/services/coordinate/locations/{locationId}/link/alert", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetAlertLinkByLocationId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Gets all capability data given a location ID
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="geoplaceId">Geo place ID of a specific alert.  To use of this query, query \&quot;name&#x3D;alert\&quot; is REQUIRED and not to combine with any other capabilty  (optional)</param>
        /// <param name="name">Comma separated string of capabilities (alert, weather, airQuality, forecast, airQualityForcast). For example, \&quot;weather,airQuality\&quot;  (optional)</param>
        /// <returns>ServiceCapabilityData</returns>
        public ServiceCapabilityData GetCapabilityByLocationId(string locationId, string authorization, string geoplaceId = default(string), string name = default(string))
        {
            SmartThingsNet.Client.ApiResponse<ServiceCapabilityData> localVarResponse = GetCapabilityByLocationIdWithHttpInfo(locationId, authorization, geoplaceId, name);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Gets all capability data given a location ID
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="geoplaceId">Geo place ID of a specific alert.  To use of this query, query \&quot;name&#x3D;alert\&quot; is REQUIRED and not to combine with any other capabilty  (optional)</param>
        /// <param name="name">Comma separated string of capabilities (alert, weather, airQuality, forecast, airQualityForcast). For example, \&quot;weather,airQuality\&quot;  (optional)</param>
        /// <returns>ApiResponse of ServiceCapabilityData</returns>
        public SmartThingsNet.Client.ApiResponse<ServiceCapabilityData> GetCapabilityByLocationIdWithHttpInfo(string locationId, string authorization, string geoplaceId = default(string), string name = default(string))
        {
            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->GetCapabilityByLocationId");
            }

            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->GetCapabilityByLocationId");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            if (geoplaceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "geoplaceId", geoplaceId));
            }
            if (name != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "name", name));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ServiceCapabilityData>("/services/coordinate/locations/{locationId}/capabilities", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetCapabilityByLocationId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Gets all capability data given a location ID
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="geoplaceId">Geo place ID of a specific alert.  To use of this query, query \&quot;name&#x3D;alert\&quot; is REQUIRED and not to combine with any other capabilty  (optional)</param>
        /// <param name="name">Comma separated string of capabilities (alert, weather, airQuality, forecast, airQualityForcast). For example, \&quot;weather,airQuality\&quot;  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ServiceCapabilityData</returns>
        public async System.Threading.Tasks.Task<ServiceCapabilityData> GetCapabilityByLocationIdAsync(string locationId, string authorization, string geoplaceId = default(string), string name = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<ServiceCapabilityData> localVarResponse = await GetCapabilityByLocationIdWithHttpInfoAsync(locationId, authorization, geoplaceId, name, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Gets all capability data given a location ID
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="geoplaceId">Geo place ID of a specific alert.  To use of this query, query \&quot;name&#x3D;alert\&quot; is REQUIRED and not to combine with any other capabilty  (optional)</param>
        /// <param name="name">Comma separated string of capabilities (alert, weather, airQuality, forecast, airQualityForcast). For example, \&quot;weather,airQuality\&quot;  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ServiceCapabilityData)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<ServiceCapabilityData>> GetCapabilityByLocationIdWithHttpInfoAsync(string locationId, string authorization, string geoplaceId = default(string), string name = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->GetCapabilityByLocationId");
            }

            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->GetCapabilityByLocationId");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            if (geoplaceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "geoplaceId", geoplaceId));
            }
            if (name != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "name", name));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ServiceCapabilityData>("/services/coordinate/locations/{locationId}/capabilities", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetCapabilityByLocationId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Get specified location ID information
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <returns>ServiceLocationInfo</returns>
        public ServiceLocationInfo GetServiceLocationInfoByLocationId(string locationId, string authorization)
        {
            SmartThingsNet.Client.ApiResponse<ServiceLocationInfo> localVarResponse = GetServiceLocationInfoByLocationIdWithHttpInfo(locationId, authorization);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Get specified location ID information
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <returns>ApiResponse of ServiceLocationInfo</returns>
        public SmartThingsNet.Client.ApiResponse<ServiceLocationInfo> GetServiceLocationInfoByLocationIdWithHttpInfo(string locationId, string authorization)
        {
            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->GetServiceLocationInfoByLocationId");
            }

            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->GetServiceLocationInfoByLocationId");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ServiceLocationInfo>("/services/coordinate/locations/{locationId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetServiceLocationInfoByLocationId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Get specified location ID information
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ServiceLocationInfo</returns>
        public async System.Threading.Tasks.Task<ServiceLocationInfo> GetServiceLocationInfoByLocationIdAsync(string locationId, string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<ServiceLocationInfo> localVarResponse = await GetServiceLocationInfoByLocationIdWithHttpInfoAsync(locationId, authorization, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Get specified location ID information
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ServiceLocationInfo)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<ServiceLocationInfo>> GetServiceLocationInfoByLocationIdWithHttpInfoAsync(string locationId, string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->GetServiceLocationInfoByLocationId");
            }

            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->GetServiceLocationInfoByLocationId");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ServiceLocationInfo>("/services/coordinate/locations/{locationId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetServiceLocationInfoByLocationId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Create a new subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <returns>ServiceNewSubscription</returns>
        public ServiceNewSubscription PostSubscriptionByLocationId(string authorization, string locationId, ServiceSubscriptionBody serviceSubscriptionBody)
        {
            SmartThingsNet.Client.ApiResponse<ServiceNewSubscription> localVarResponse = PostSubscriptionByLocationIdWithHttpInfo(authorization, locationId, serviceSubscriptionBody);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Create a new subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <returns>ApiResponse of ServiceNewSubscription</returns>
        public SmartThingsNet.Client.ApiResponse<ServiceNewSubscription> PostSubscriptionByLocationIdWithHttpInfo(string authorization, string locationId, ServiceSubscriptionBody serviceSubscriptionBody)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->PostSubscriptionByLocationId");
            }

            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->PostSubscriptionByLocationId");
            }

            // verify the required parameter 'serviceSubscriptionBody' is set
            if (serviceSubscriptionBody == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'serviceSubscriptionBody' when calling CoordinateApi->PostSubscriptionByLocationId");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            
            localVarRequestOptions.Data = serviceSubscriptionBody;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<ServiceNewSubscription>("/services/coordinate/locations/{locationId}/subscriptions", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PostSubscriptionByLocationId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Create a new subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ServiceNewSubscription</returns>
        public async System.Threading.Tasks.Task<ServiceNewSubscription> PostSubscriptionByLocationIdAsync(string authorization, string locationId, ServiceSubscriptionBody serviceSubscriptionBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<ServiceNewSubscription> localVarResponse = await PostSubscriptionByLocationIdWithHttpInfoAsync(authorization, locationId, serviceSubscriptionBody, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Create a new subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ServiceNewSubscription)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<ServiceNewSubscription>> PostSubscriptionByLocationIdWithHttpInfoAsync(string authorization, string locationId, ServiceSubscriptionBody serviceSubscriptionBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->PostSubscriptionByLocationId");
            }

            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->PostSubscriptionByLocationId");
            }

            // verify the required parameter 'serviceSubscriptionBody' is set
            if (serviceSubscriptionBody == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'serviceSubscriptionBody' when calling CoordinateApi->PostSubscriptionByLocationId");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            
            localVarRequestOptions.Data = serviceSubscriptionBody;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<ServiceNewSubscription>("/services/coordinate/locations/{locationId}/subscriptions", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PostSubscriptionByLocationId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Update a subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <returns></returns>
        public void PutSubscriptionBySubscriptionId(string locationId, string subscriptionId, string authorization, ServiceSubscriptionBody serviceSubscriptionBody)
        {
            PutSubscriptionBySubscriptionIdWithHttpInfo(locationId, subscriptionId, authorization, serviceSubscriptionBody);
        }

        /// <summary>
        ///  Update a subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <returns>ApiResponse of Object(void)</returns>
        public SmartThingsNet.Client.ApiResponse<Object> PutSubscriptionBySubscriptionIdWithHttpInfo(string locationId, string subscriptionId, string authorization, ServiceSubscriptionBody serviceSubscriptionBody)
        {
            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->PutSubscriptionBySubscriptionId");
            }

            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'subscriptionId' when calling CoordinateApi->PutSubscriptionBySubscriptionId");
            }

            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->PutSubscriptionBySubscriptionId");
            }

            // verify the required parameter 'serviceSubscriptionBody' is set
            if (serviceSubscriptionBody == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'serviceSubscriptionBody' when calling CoordinateApi->PutSubscriptionBySubscriptionId");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            localVarRequestOptions.PathParameters.Add("subscriptionId", SmartThingsNet.Client.ClientUtils.ParameterToString(subscriptionId)); // path parameter
            
            localVarRequestOptions.Data = serviceSubscriptionBody;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<Object>("/services/coordinate/locations/{locationId}/subscriptions/{subscriptionId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PutSubscriptionBySubscriptionId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Update a subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task PutSubscriptionBySubscriptionIdAsync(string locationId, string subscriptionId, string authorization, ServiceSubscriptionBody serviceSubscriptionBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await PutSubscriptionBySubscriptionIdWithHttpInfoAsync(locationId, subscriptionId, authorization, serviceSubscriptionBody, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        ///  Update a subscription 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="locationId">The ID of the Location.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <param name="authorization">OAuth token</param>
        /// <param name="serviceSubscriptionBody"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> PutSubscriptionBySubscriptionIdWithHttpInfoAsync(string locationId, string subscriptionId, string authorization, ServiceSubscriptionBody serviceSubscriptionBody, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'locationId' is set
            if (locationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locationId' when calling CoordinateApi->PutSubscriptionBySubscriptionId");
            }

            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'subscriptionId' when calling CoordinateApi->PutSubscriptionBySubscriptionId");
            }

            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CoordinateApi->PutSubscriptionBySubscriptionId");
            }

            // verify the required parameter 'serviceSubscriptionBody' is set
            if (serviceSubscriptionBody == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'serviceSubscriptionBody' when calling CoordinateApi->PutSubscriptionBySubscriptionId");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("locationId", SmartThingsNet.Client.ClientUtils.ParameterToString(locationId)); // path parameter
            localVarRequestOptions.PathParameters.Add("subscriptionId", SmartThingsNet.Client.ClientUtils.ParameterToString(subscriptionId)); // path parameter
            
            localVarRequestOptions.Data = serviceSubscriptionBody;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<Object>("/services/coordinate/locations/{locationId}/subscriptions/{subscriptionId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PutSubscriptionBySubscriptionId", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
