using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Bipap.DAL.Models;
using Bipap.DAL;

namespace AdminPanel.Controllers
{
    public class PatientController : GenericController<Patient, BipapDbContext>
    {
        public PatientController(BipapDbContext context, IConfiguration config, IHostingEnvironment hostingEnvironment) : base(context, config, hostingEnvironment) { }
    }
}
