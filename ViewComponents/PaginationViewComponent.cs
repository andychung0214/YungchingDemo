using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace YungchingDemo.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        protected readonly IHostingEnvironment Env;
        protected readonly YungchingDemo.DataLayer.NorthWind.NorthwindContext northWindContext;

        public PaginationViewComponent(IHostingEnvironment env, YungchingDemo.DataLayer.NorthWind.NorthwindContext _northWindContext)
        {
            Env = env;
            northWindContext = _northWindContext;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
