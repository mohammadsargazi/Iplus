using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Bipap.DAL.Models;
using Bipap.DAL;

namespace AdminPanel.Controllers
{
    public class GenderController : GenericController<Gender, BipapDbContext>
    {
        public GenderController(BipapDbContext context, IConfiguration config, IHostingEnvironment hostingEnvironment) : base(context, config, hostingEnvironment) { }
    }
}
