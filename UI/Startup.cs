using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bipap.DAL;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Repository.Repositories;
using Bipap.Service.IServices;
using Bipap.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("CookieAuthentication")
                 .AddCookie("CookieAuthentication", config =>
                 {
                     config.ExpireTimeSpan = TimeSpan.FromDays(30);
                     config.Cookie.Name = "UserLoginCookie";
                     config.LoginPath = "/Account/Login";
                 });
            services.AddControllersWithViews();
            services.AddDbContext<BipapDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("Bipap.DAL")));
            services.AddScoped<DbContext, BipapDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IPrescriptionRepository, PrescriptionRepository>();
            services.AddTransient<IPrescriptionService, PrescriptionService>();

            services.AddTransient<IDeviceTypeRepository, DeviceTypeRepository>();
            services.AddTransient<IDeviceTypeService, DeviceTypeService>();

            services.AddTransient<IDeviceTypeInformationRepository, DeviceTypeInformationRepository>();
            services.AddTransient<IDeviceTypeInformationService, DeviceTypeInformationService>();

            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IStepOneModuleRepository, StepOneModuleRepository>();
            services.AddTransient<IStepOneModuleService, StepOneModuleService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Patient}/{action=List}/{id?}");
            });
        }
    }
}
