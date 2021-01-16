using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Bipap.DAL.Models;
using Bipap.DAL;

namespace AdminPanel.Controllers
{
    public class DoctorController : GenericController<Doctor, BipapDbContext>
    {
        public DoctorController(BipapDbContext context, IConfiguration config, IHostingEnvironment hostingEnvironment) : base(context, config, hostingEnvironment) { }
    }
}
