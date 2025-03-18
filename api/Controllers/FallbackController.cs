using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class FallbackController : Controller
{
    public ActionResult Index()
    {
        // Add cache control headers to prevent caching
        Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
        Response.Headers.Append("Pragma", "no-cache");
        Response.Headers.Append("Expires", "0");
        
        return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot", "index.html"), "text/HTML");
    }
}