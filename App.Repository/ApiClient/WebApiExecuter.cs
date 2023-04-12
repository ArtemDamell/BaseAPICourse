using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Core.StaticData;

namespace MyApp.Repository.ApiClient
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly ITokenRepository _tokenRepository;

        public WebApiExecuter(HttpClient httpClient)
        {
            _baseUrl = httpClient.BaseAddress.AbsoluteUri;
            _httpClient = httpClient;

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Invokes a GET request to the specified URI and returns the response as a generic type.
        /// </summary>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <param name="uri">The URI of the request.</param>
        /// <returns>The response of the request as a generic type.</returns>
        public Task<T> InvokeGet<T>(string uri) => _httpClient.GetFromJsonAsync<T>(GetUrl(uri));

        /// <summary>
        /// Invokes a POST request to the specified URI with the given object as the body.
        /// </summary>
        /// <typeparam name="T">The type of the object to be sent in the body of the request.</typeparam>
        /// <param name="uri">The URI of the endpoint to send the request to.</param>
        /// <param name="obj">The object to be sent in the body of the request.</param>
        /// <returns>The response from the endpoint.</returns>
        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            var token = await _tokenRepository.GetToken();

            var response = await _httpClient.PostAsJsonAsync(GetUrl(uri), obj);

            await HandleError(response);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        /// <summary>
        /// Invokes a POST request to the specified URL and returns the response as a string.
        /// </summary>
        /// <typeparam name="T">The type of the object to be sent in the request.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="obj">The object to be sent in the request.</param>
        /// <returns>The response from the request as a string.</returns>
        public async Task<string?> InvokePostReturnString<T>(string url, T? obj)
        {
            var token = await _tokenRepository.GetToken();

            var response = await _httpClient.PostAsJsonAsync(GetUrl(url), obj);
            await HandleError(response);

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Invokes a PUT request to the specified URI with the given object as the body.
        /// </summary>
        /// <typeparam name="T">The type of the object to be sent in the body.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="obj">The object to be sent in the body.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task InvokePut<T>(string uri, T obj)
        {
            var token = await _tokenRepository.GetToken();

            var response = await _httpClient.PutAsJsonAsync(GetUrl(uri), obj);
            await HandleError(response);
        }

        /// <summary>
        /// Invokes a DELETE request to the specified URI.
        /// </summary>
        /// <param name="uri">The URI of the request.</param>
        /// <returns>The response from the DELETE request.</returns>
        public async Task InvokeDelete(string uri)
        {
            var token = await _tokenRepository.GetToken();

            var response = await _httpClient.DeleteAsync(GetUrl(uri));
            await HandleError(response);
        }

        /// <summary>
        /// Gets the URL for the given URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The URL.</returns>
        private string GetUrl(string uri)
        {
            if (_baseUrl.EndsWith("/"))
                return $"{_baseUrl}{uri}";
            else
                return $"{_baseUrl}/{uri}";
        }

        /// <summary>
        /// Handles an error response from an HTTP request.
        /// </summary>
        /// <returns>
        /// Throws an HttpRequestException if the response is not successful.
        /// </returns>
        private static async Task HandleError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }
        }
    }
}
