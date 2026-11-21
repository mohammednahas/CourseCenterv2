
using CourseCenterv2.ExternalServices;
using CourseCenterv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenterv2.Controllers;

public class PostsController : Controller
{
    private readonly JsonPlaceholderService _postsService;

    public PostsController(JsonPlaceholderService postsService)
    {
        _postsService = postsService;
    }

    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        try
        {
            List<ApiPost> posts =
                await _postsService.GetPostsAsync(cancellationToken);

            return View(posts);
        }
        catch (HttpRequestException)
        {
            ViewBag.ErrorMessage =
                "تعذر الاتصال بالـ Test API. حاول مرة أخرى.";

            return View(new List<ApiPost>());
        }
        catch (TaskCanceledException)
        {
            ViewBag.ErrorMessage =
                "انتهت مهلة الاتصال أو تم إلغاء الطلب.";

            return View(new List<ApiPost>());
        }
    }
}
