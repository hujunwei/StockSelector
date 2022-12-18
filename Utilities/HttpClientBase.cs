namespace Utilities
{
    public abstract class HttpClientBase : IDisposable
    {
        private readonly HttpClient _client;

        protected HttpClientBase(HttpClient client)
        {
            _client = client;
        }

        protected async Task<HttpResponseMessage> SendAsync(
            HttpMethod method,
            string requestUri,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> queryParams = null,
            HttpContent content = null)
        {
            using var request = new HttpRequestMessage(method, requestUri);

            // Add headers
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            // Add query parameters
            if (queryParams != null)
            {
                var queryString = new FormUrlEncodedContent(queryParams);
                request.RequestUri = new Uri($"{request.RequestUri}?{queryString}");
            }

            // Add content
            if (content != null)
            {
                request.Content = content;
            }

            return await _client.SendAsync(request);
        }

        public void Dispose()
        {
            if (_client == null) return;

            _client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}