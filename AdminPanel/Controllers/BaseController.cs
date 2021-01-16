using System.Linq;
using Bipap.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AdminPanel.Controllers
{
    public class BaseController<T> : Controller where T : DbContext, ICustomDbContext
    {
        protected T _context;
        protected IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;
        public BaseController(T context, IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _config = config;
            _hostingEnvironment = hostingEnvironment;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            //Get the current assembly
            var assembly = typeof(BaseController<T>).Assembly;
            var types = assembly.GetTypes();
            //load types of Base type GenericController
            var types1 = types.Where(i => i.BaseType != null && i.BaseType.Name.Contains("GenericController")).ToList();
            ViewBag.LControllers = types1;
            ViewBag.Host = (Request.IsHttps ? "https://" : "http://") + Request.Host;
            ViewBag.Controller = RouteData.Values["controller"];
            ViewBag.Action = RouteData.Values["action"];

        }
    }
}
