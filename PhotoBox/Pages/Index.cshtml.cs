using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhotoBox.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    public IList<string> FileList { get; set; }
    public void OnGet()
    {
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Pics");

        FileList = Directory.GetFiles(path).OrderBy(f=> f).Select(f=> new FileInfo(f).Name).ToList();
    }
}