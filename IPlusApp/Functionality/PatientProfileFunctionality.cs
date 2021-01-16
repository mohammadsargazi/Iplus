using IPlusApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPlusApp.Functionality
{
    public class PatientProfileFunctionality
    {
        //private string patientPath = AppDomain.CurrentDomain.BaseDirectory + "\\Patient.txt";
        private string patientPath = Path.Combine(
   Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
   "Patient.txt");
        public void WritePatientModel(PatientModel model)
        {
            if (!File.Exists(patientPath))
                File.Create(patientPath).Dispose();
            using (TextWriter tw = new StreamWriter(patientPath))
            {
                var json = JsonConvert.SerializeObject(model);
                tw.WriteLine(json);
            }
        }
        public PatientModel ReadPatientModel()
        {
            if (!File.Exists(patientPath))
                File.Create(patientPath).Dispose();
            using (StreamReader sr = new StreamReader(patientPath))
            {
                var json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<PatientModel>(json);
            }
        }
    }
}
