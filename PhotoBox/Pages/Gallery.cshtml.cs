using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhotoBox.Pages;

public class GalleryModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GalleryModel(ILogger<IndexModel> logger, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    public IList<PhotoViewModel> FileList { get; set; }
    public void OnGet()
    {
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Pics");

        var files = Directory.GetFiles(path).OrderBy(f => f);//.Select(f => new FileInfo(f).Name).ToList();

        FileList=new List<PhotoViewModel>();

        const string imageBase64 = "data:image/png;base64,";

        foreach (var file in files)
        {
            FileList.Add(new PhotoViewModel
            {
                FileName=new FileInfo(file).Name,
                FileContent= imageBase64+Convert.ToBase64String(System.IO.File.ReadAllBytes(file)),
            });
        }
    }
}


public class PhotoViewModel
{
    public string FileName { get; set; }
    public string FileContent { get; set; }
}
