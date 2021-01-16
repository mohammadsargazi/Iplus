using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bipap.DAL;
using Bipap.DAL.UnitOfWork;
using Bipap.Repository.IReposirories;
using Bipap.Repository.Repositories;
using Bipap.Service.IServices;
using Bipap.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MobileService.Functionality;
using MobileService.Model;


namespace MobileService
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
            services.AddControllers();
            services.AddDbContext<BipapDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("Bipap.DAL")));
            services.AddScoped<DbContext, BipapDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IPrescriptionRepository, PrescriptionRepository>();
            services.AddTransient<IPrescriptionService, PrescriptionService>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IStepOneModuleRepository, StepOneModuleRepository>();
            services.AddTransient<IStepOneModuleService, StepOneModuleService>();
            services.AddTransient<ISupportUserRepository, SupportUserRepository>();
            services.AddTransient<ISupportUserService, SupportUserService>();
            services.AddTransient<ISupportUserOrderRepository, SupportUserOrderRepository>();
            services.AddTransient<ISupportUserOrderService, SupportUserOrderService>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IStepOneModuleRepository, StepOneModuleRepository>();
            services.AddTransient<IStepOneModuleService, StepOneModuleService>();
            services.AddTransient<IDeviceTypeRepository, DeviceTypeRepository>();
            services.AddTransient<IDeviceTypeService, DeviceTypeService>();
            services.AddTransient<IDeviceTypeInformationRepository, DeviceTypeInformationRepository>();
            services.AddTransient<IDeviceTypeInformationService, DeviceTypeInformationService>();
            services.AddTransient<IEndOfTreatmentRepository, EndOfTreatmentRepository>();
            services.AddTransient<IEndOfTreatmentService, EndOfTreatmentService>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "IPLUS", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IPLUS V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
