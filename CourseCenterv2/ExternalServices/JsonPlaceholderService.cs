
using System.Net.Http.Json;
using CourseCenterv2.Models;

namespace CourseCenterv2.ExternalServices;

public class JsonPlaceholderService
{
    private readonly HttpClient _httpClient;

    public JsonPlaceholderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ApiPost>> GetPostsAsync(
        CancellationToken cancellationToken = default)
    {
        var posts = await _httpClient.GetFromJsonAsync<List<ApiPost>>(
            "posts?_limit=5",
            cancellationToken);

        return posts ?? new List<ApiPost>();
    }
}
