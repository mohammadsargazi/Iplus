using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Bipap.DAL.Models;
using Bipap.DAL;

namespace AdminPanel.Controllers
{
    public class EndOfTreatmentStatusController : GenericController<EndOfTreatmentStatus, BipapDbContext>
    {
        public EndOfTreatmentStatusController(BipapDbContext context, IConfiguration config, IHostingEnvironment hostingEnvironment) : base(context, config, hostingEnvironment) { }
    }
}
