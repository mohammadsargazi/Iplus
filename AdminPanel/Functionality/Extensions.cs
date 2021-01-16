using Bipap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdminPanel.Functionality
{
    public static class Extensions
    {
        #region HelperMethod
        public static string PersianToEnglish(this string persianStr)
        {
            Dictionary<string, string> LettersDictionary = new Dictionary<string, string>
            {
                ["۰"] = "0",
                ["۱"] = "1",
                ["۲"] = "2",
                ["۳"] = "3",
                ["۴"] = "4",
                ["۵"] = "5",
                ["۶"] = "6",
                ["۷"] = "7",
                ["۸"] = "8",
                ["۹"] = "9"
            };
            return LettersDictionary.Aggregate(persianStr, (current, item) =>
                         current.Replace(item.Key, item.Value));
        }
        public static DateTime ToGregorianDate(this string value)
        {
            value = value.PersianToEnglish();
            //value = value + " 12:00:00";
            //var dt = System.DateTime.Parse(value, new CultureInfo("fa-IR"));
            //return dt.ToUniversalTime();
            var year = Convert.ToInt32(value.Split('/')[0]);
            var month = Convert.ToInt32(value.Split('/')[1]);
            var day = Convert.ToInt32(value.Split('/')[2]);
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(year, month, day, pc);
            return dt;
        }
        #endregion
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties  
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table   
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows  
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
 
        public static List<Device> ToDeviceModel(this DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return null;
            var deviceList = new List<Device>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var device = new Device();
                device.SerialNumber= dt.Rows[i]["SerialNumber"].ToString();
                device.RegistrationDate = dt.Rows[i]["SerialNumber"].ToString().ToGregorianDate();
                device.DeviceTypeId = Convert.ToInt32(dt.Rows[i]["SerialNumber"].ToString());
                deviceList.Add(device);
            }
            return deviceList;
        }
    }
}
