using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Bipap.DAL.Models;
using Bipap.DAL;

namespace AdminPanel.Controllers
{
    public class SupportUserOrderController : GenericController<SupportUserOrder, BipapDbContext>
    {
        public SupportUserOrderController(BipapDbContext context, IConfiguration config, IHostingEnvironment hostingEnvironment) : base(context, config, hostingEnvironment) { }
    }
}
