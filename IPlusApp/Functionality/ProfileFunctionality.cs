using IPlusApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPlusApp.Functionality
{
    public class ProfileFunctionality
    {
        private string path = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "Doctor.txt");
        public void Update(DoctorModel model)
        {
            if (!File.Exists(path))
                File.Create(path).Dispose();
            using (TextWriter tw = new StreamWriter(path))
            {
                var json = JsonConvert.SerializeObject(model);
                tw.WriteLine(json);
            }
        }
        public DoctorModel Get()
        {
            if (!File.Exists(path))
                File.Create(path).Dispose();
            using (StreamReader sr = new StreamReader(path))
            {
                var json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<DoctorModel>(json);
            }
        }
    }
}
