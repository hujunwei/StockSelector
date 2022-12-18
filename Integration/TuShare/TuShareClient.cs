using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Utilities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Integration;

public class TuShareClient : HttpClientBase
{
    public TuShareClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<ResponseData> ListAllStocks()
    {
        var @params = new ListAllStockParamsPayload
        {
            ListStatus = "L"
        };

        var requestPayload = new ListAllStockPayload
        {
            ApiName = "stock_basic",
            Token = "660219f456532cb8a231f4366ccf0e2572883a89499824c3dc9812f6",
            Params = @params,
            Fields = ""
        };

        var content = new ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes(requestPayload));
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await SendAsync(
            HttpMethod.Post,
            "http://api.tushare.pro",
            headers: new Dictionary<string, string>(),
            queryParams: new Dictionary<string, string>(),
            content: content);

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<ResponseData>(responseContent)!;
    }

    private class ListAllStockPayload
    {
        [JsonPropertyName("api_name")]
        public string ApiName { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("params")]
        public ListAllStockParamsPayload Params { get; set; }

        [JsonPropertyName("fields")]
        public string Fields { get; set; }
    }

    private class ListAllStockParamsPayload
    {
        [JsonPropertyName("list_status")]
        public string ListStatus { get; set; }
    }
}

public class DataModel
{
    [JsonPropertyName("fields")]
    public IEnumerable<string> Fields { get; set; }

    [JsonPropertyName("items")]
    public IEnumerable<IEnumerable<string>> Items { get; set; }
}

public class ResponseData
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("msg")]
    public string Message { get; set; }

    [JsonPropertyName("data")]
    public DataModel Data { get; set; }
}

