using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace PhotoBox.Pages;

public class GalleryModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMemoryCache _memoryCache;

    public GalleryModel(ILogger<IndexModel> logger, IWebHostEnvironment webHostEnvironment, IMemoryCache memoryCache)
    {
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        FileList = new List<PhotoViewModel>();
        _memoryCache = memoryCache;
    }

    public IList<PhotoViewModel> FileList { get; set; }
    public void OnGet()
    {
        if (_memoryCache.TryGetValue("photos",out IList<PhotoViewModel>? cachedFileList)==false)
        {
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Pics");

            var files = Directory.GetFiles(path).OrderBy(f => f);//.Select(f => new FileInfo(f).Name).ToList();

            const string imageBase64 = "data:image/png;base64,";

            foreach (var file in files)
            {
                FileList.Add(new PhotoViewModel
                {
                    FileName = new FileInfo(file).Name,
                    FileContent = imageBase64 + Convert.ToBase64String(System.IO.File.ReadAllBytes(file)),
                });
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
           .SetSlidingExpiration(TimeSpan.FromSeconds(30));

            _memoryCache.Set("photos", FileList, cacheEntryOptions);
        }
        else
        {
            FileList = cachedFileList;

        }





    }
}


public record PhotoViewModel
{
    public required string FileName { get; init; }
    public required string FileContent { get; init; }
}
