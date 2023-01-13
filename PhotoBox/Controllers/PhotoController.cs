using Microsoft.AspNetCore.Mvc;

namespace PhotoBox.Controllers;

public class PhotoController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PhotoController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [Route("api/photo/{**filename}")]
    public IActionResult Get(string fileName)
    {
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Pics",fileName);

        var content=System.IO.File.ReadAllBytes(path);

        return File(content,"image/jpeg");
    }
}
