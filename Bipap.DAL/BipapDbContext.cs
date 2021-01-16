using Bipap.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Bipap.DAL
{
    public class BipapDbContext: DbContext, ICustomDbContext
    {
        #region DbSets
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceStatus> DeviceStatuses { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<DeviceTypeInformation> DeviceTypeInformations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<EndOfTreatment> EndOfTreatments { get; set; }
        public DbSet<EndOfTreatmentStatus> EndOfTreatmentStatus { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FileUploadType> FileUploadTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionStatus> PrescriptionStatuses { get; set; }
        public DbSet<SettelmentStatus> SettelmentStatuses { get; set; }
        public DbSet<StepOneModule> StepOneModules { get; set; }
        public DbSet<SupportUser> SupportUsers { get; set; }
        //public DbSet<SupportUserDevice> SupportUserDevicees { get; set; }
        public DbSet<SupportUserOrder> SupportUserOrders { get; set; }
        #endregion
        public BipapDbContext(DbContextOptions<BipapDbContext> options)
             : base(options)
        { }

        /// <summary>
        /// Get a dictionary of a table values with the database Key property and Value as the representation string of the class
        /// </summary>
        /// <param name="type">Type of the requested Table</param>
        /// <returns></returns>
        public List<KeyValuePair<object, string>> GetTable(Type type)
        {
            //Get the DbContext Type
            var ttype = GetType();
            //The DbContext properties
            var props = ttype.GetProperties().ToList();
            // The DbSet property with base type @type
            var prop = props.Where(i => i.PropertyType.GenericTypeArguments.Any() && i.PropertyType.GenericTypeArguments.First() == type).FirstOrDefault();

            //The DbSet instance
            var pvalue = prop?.GetValue(this);

            // Dictionary to return
            var l = new Dictionary<object, string>();

            var pv = (IEnumerable<object>)pvalue;

            //The entity Key property
            var keyprop = type.GetProperties().First(i => i.CustomAttributes.Any(j => j.AttributeType == typeof(KeyAttribute)));

            //Fills the dictionary
            foreach (BaseModel item in pv)
            {
                //with the key and the ToString() entity result
                l.Add(keyprop.GetValue(item), item.ToString());
            }
            return l.ToList();
        }

        /// <summary>
        /// Get a table casted to Objects
        /// </summary>
        /// <param name="type">Type of the requested Table</param>
        /// <param name="cast">Only to generate a different method signature</param>
        /// <returns></returns>
        public IEnumerable<object> GetTable(Type type, bool cast = true)
        {
            //Get the DbContext Type
            var ttype = GetType();
            //The DbContext properties
            var props = ttype.GetProperties().ToList();
            // The DbSet property with base type @type
            var prop = props.Where(i => i.PropertyType.GenericTypeArguments.Any() && i.PropertyType.GenericTypeArguments.First() == type).FirstOrDefault();

            //The DbSet instance
            var pvalue = prop?.GetValue(this);

            // Dictionary to return
            var l = new Dictionary<object, string>();

            var pv = (IEnumerable<object>)pvalue;

            return pv;
        }
    }
}
