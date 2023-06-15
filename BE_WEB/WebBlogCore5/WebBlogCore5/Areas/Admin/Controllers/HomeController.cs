using Microsoft.AspNetCore.Mvc;

namespace WebBlogCore5.Areas.Admin.Controllers
{
    [Area("Admin")] //Những Controller nào mà ở trong area admin thì thêm cái này vào để định nghĩa khu vực
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
