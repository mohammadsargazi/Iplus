using IPlusApp.Functionality;
using IPlusApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IPlusApp.Functionality;

namespace IPlusApp
{
    public partial class ExportPDF : Form
    {
        //private readonly ExportPdfFunctionality _exportPdfFunctionality = new ExportPdfFunctionality();
        private CommonFunctionality _commonFunctionality = new CommonFunctionality();
        private PatientProfileFunctionality _patientProfileFunctionality = new PatientProfileFunctionality();
        private readonly ProfileFunctionality _profileFunctionality = new ProfileFunctionality();
        private ExportPdfFunctionality _exportPdfFunctionality = new ExportPdfFunctionality();
        private SessionReportFunctionality _sessionReportFunctionality = new SessionReportFunctionality();
        private string moduleStepTwoResultPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ModuleStepTwoResult.txt");

        #region HelperMethod
        private string GetTime(DateTime dateTime)
        {
            return dateTime.ToString("hh:mm tt");
        }


        #endregion
        public ExportPDF()
        {
            InitializeComponent();
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            var patient = _patientProfileFunctionality.ReadPatientModel();
            var doctor = _profileFunctionality.Get();
            var stepTwoResult = _commonFunctionality.ReadModuleStepTwoResult(moduleStepTwoResultPath);
            var ipapArray = GetListInDay(stepTwoResult, "Ipap");
            var epapArray = GetListInDay(stepTwoResult, "Epap");
            var totalUseTime = stepTwoResult.Sum(x => x.Length) / 288000;
            var reportModel = new ReportModel
            {
                ChIPAP = chIPAP.Checked,
                ChEPAP = chEPAP.Checked,
                ChLEAK = chLEAK.Checked,
                ChTidalVolume = chTidalVolume.Checked,
                ChMinuteVentilation = chMinuteVentilation.Checked,
                ChRespiratoryRate = chRespiratoryRate.Checked,
                ChAHIAI = chAHIAI.Checked,
                ChUsage = chUsage.Checked,
                ChFlow = chFlow.Checked,
                ChPressure = chPressure.Checked,
                PatientName = patient.FullName,
                PatientId = patient.PersonalId,
                DateOfBirth = patient.BirthDay,
                Gender = patient.Gender,
                Address = patient.Address,
                Age = patient.Age,
                TelephoneNumbers = patient.PhoneNum,
                Email = patient.Email,
                InsuranceKind = patient.InsuranceKinde,
                InsuranceDate=patient.InsuranceDate,
                InsuranceId=patient.InsuranceId,
                IPAPMedian = stepTwoResult.Select(x => x.IPAP).ToArray().ToMedianAndPresentModel().Median.ToString(),
                IPAPMinimum = stepTwoResult.Select(x => x.IPAP).ToArray().ToMedianAndPresentModel().Minimum.ToString(),
                IPAPPercentile = stepTwoResult.Select(x => x.IPAP).ToArray().ToMedianAndPresentModel().Present.ToString(),
                LeakMedian = stepTwoResult.Select(x => x.Leak).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Median).ToArray().ToMedianAndPresentModel().Median.ToString(),
                LeakMinimum = stepTwoResult.Select(x => x.Leak).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Minimum).ToArray().ToMedianAndPresentModel().Minimum.ToString(),
                LeakPercentile = stepTwoResult.Select(x => x.Leak).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Present).ToArray().ToMedianAndPresentModel().Present.ToString(),
                TidalVolumeMedian = stepTwoResult.Select(x => x.tidal_volume).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Median).ToArray().ToMedianAndPresentModel().Median.ToString(),
                TidalVolumeMinimum = stepTwoResult.Select(x => x.tidal_volume).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Minimum).ToArray().ToMedianAndPresentModel().Minimum.ToString(),
                TidalVolumePercentile = stepTwoResult.Select(x => x.tidal_volume).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Present).ToArray().ToMedianAndPresentModel().Present.ToString(),
                MinuteVentilationMedian = stepTwoResult.Select(x => x.minute_v).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Median).ToArray().ToMedianAndPresentModel().Median.ToString(),
                MinuteVentilationPercentile = stepTwoResult.Select(x => x.minute_v).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Present).ToArray().ToMedianAndPresentModel().Present.ToString(),
                MinuteVentilationMinimum = stepTwoResult.Select(x => x.minute_v).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Minimum).ToArray().ToMedianAndPresentModel().Minimum.ToString(),
                RespiratoryRateMedian = stepTwoResult.Select(x => x.bpm).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Median).ToArray().ToMedianAndPresentModel().Median.ToString(),
                RespiratoryRateMinimum = stepTwoResult.Select(x => x.bpm).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Minimum).ToArray().ToMedianAndPresentModel().Minimum.ToString(),
                RespiratoryRatePercentile = stepTwoResult.Select(x => x.bpm).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Present).ToArray().ToMedianAndPresentModel().Present.ToString(),
                ApneaIndex = stepTwoResult.Sum(x => x.Appne).ToString(),
                HypopneaIndex = stepTwoResult.Sum(x => x.HApne).ToString(),
                AHI = (stepTwoResult.Sum(x => x.AppneAndHApnne) / totalUseTime).ToString(),
                IERatioMedian =(1/ (stepTwoResult.Select(x => x.E_ratioMedian).ToArray().ToMedianAndPresentModel().Median/ stepTwoResult.Select(x => x.I_ratioMedian).ToArray().ToMedianAndPresentModel().Median)).ToString(),
                //IERatioMinimum = (1 / (stepTwoResult.Select(x => x.E_ratioMinimum).ToArray().ToMedianAndPresentModel().Minimum / stepTwoResult.Select(x => x.I_ratioMinimum).ToArray().ToMedianAndPresentModel().Minimum)).ToString(),
                IERatioPercentile = (1 / (stepTwoResult.Select(x => x.E_ratioPresent).ToArray().ToMedianAndPresentModel().Present / stepTwoResult.Select(x => x.I_ratioPresent).ToArray().ToMedianAndPresentModel().Present)).ToString(),
                InspiratoryTimeMedian = stepTwoResult.Select(x => x.InspiratoryTimeArray).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Median).ToArray().ToMedianAndPresentModel().Median.ToString(),
                InspiratoryTimeMinimum = stepTwoResult.Select(x => x.InspiratoryTimeArray).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Minimum).ToArray().ToMedianAndPresentModel().Minimum.ToString(),
                InspiratoryTimePercentile = stepTwoResult.Select(x => x.InspiratoryTimeArray).Select(x => x.ToMedianAndPresentModel()).ToArray().Select(x => x.Present).ToArray().ToMedianAndPresentModel().Present.ToString(),
                AlveolarVentilationMedian = "",
                AlveolarVentilationMinimum = "",
                AlveolarVentilationPercentile = "",
                IpapMedianArray = ToStringModel(ipapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Median).ToList()),
                IpapPrecentileArray = ToStringModel(ipapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Present).ToList()),
                IpapMinimumArray = ToStringModel(ipapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Minimum).ToList()),
                EpapMedianArray = ToStringModel(epapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Median).ToList()),
                EpapPrecentileArray = ToStringModel(epapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Present).ToList()),
                EpapMinimumArray = ToStringModel(epapArray.Select(x => x.ToMedianAndPresentModel()).Select(y => y.Minimum).ToList()),
                ReportedBy = txtFullName.Text,
                Time = GetTime(DateTime.Now),
                LeakMinimumArray = ToStringModel(stepTwoResult.Select(x => x.Leak.ToMedianAndPresentModel()).Select(x => x.Minimum).ToList()),
                LeakMedianArray = ToStringModel(stepTwoResult.Select(x => x.Leak.ToMedianAndPresentModel()).Select(x => x.Median).ToList()),
                LeakPrecentileArray = ToStringModel(stepTwoResult.Select(x => x.Leak.ToMedianAndPresentModel()).Select(x => x.Present).ToList()),
                TidalVolumeMinimumArray = ToStringModel(stepTwoResult.Select(x => x.tidal_volume.ToMedianAndPresentModel()).Select(x => x.Minimum).ToList()),
                TidalVolumeMedianArray = ToStringModel(stepTwoResult.Select(x => x.tidal_volume.ToMedianAndPresentModel()).Select(x => x.Median).ToList()),
                TidalVolumePrecentileArray = ToStringModel(stepTwoResult.Select(x => x.tidal_volume.ToMedianAndPresentModel()).Select(x => x.Present).ToList()),
                MinuteVentilationMinimumArray = ToStringModel(stepTwoResult.Select(x => x.minute_vMinimum).ToList()),
                MinuteVentilationMedianArray = ToStringModel(stepTwoResult.Select(x => x.minute_vMedian).ToList()),
                MinuteVentilationPrecentileArray = ToStringModel(stepTwoResult.Select(x => x.minute_vPresent).ToList()),
                RespiratoryRateMinimumArray = ToStringModel(stepTwoResult.Select(x => x.bpmMinimum).ToList()),
                RespiratoryRateMedianArray = ToStringModel(stepTwoResult.Select(x => x.bpmMedian).ToList()),
                RespiratoryRatePrecentileArray = ToStringModel(stepTwoResult.Select(x => x.bpmPresent).ToList()),
                Ahi = ToAhiModel(stepTwoResult),
                IsAvaps = stepTwoResult.Last().Mode == 6,
                TotalUsage = ToStingModel(GetTottalUsage(stepTwoResult)),
                TotalDays = GetTottalUsage(stepTwoResult).Count().ToString(),// stepTwoResult.Count().ToString(),
                TotalHoursUsed = (Convert.ToDecimal(stepTwoResult.Sum(x => x.Length)) / Convert.ToDecimal(288000)).ToString("f2"),
                AverageDailyUsed = ((Convert.ToDecimal(stepTwoResult.Sum(x => x.Length)) / Convert.ToDecimal(288000)) / Convert.ToDecimal(GetTottalUsage(stepTwoResult).Count())).ToString("F2"),
                ERatioChart= ToStringModel(stepTwoResult.Select(x => x.E_ratioMedian).ToList()),
                IRatioChart= ToStringModel(stepTwoResult.Select(x => x.I_ratioMedian).ToList()),
                TreatingDoctor=doctor.FullName,
                Clinic=doctor.Clinic,


            };
            //  define  property and  call generatepdf for export pdf 
            _exportPdfFunctionality.GeneratePdf(reportModel);
        }

        private string ToStringModel(List<decimal> model)
        {
            var res = "";
            foreach (var item in model)
            {
                res = res + item + ",";
            }
            return res.Remove(res.Length - 1);
        }
        private string ToStingModel(List<TotalUsage> model)
        {
            var res = "";
            foreach (var item in model)
            {
                res = res + item.Date + "," + item.Count + ";";
            }
            return res.Remove(res.Length - 1);
        }
        private string ToAhiModel(List<ModuleStepTwoResult> model)
        {
            var res = "";
            var ahiList = new List<AhiModel>();
            foreach (var item in model)
            {
                if (!ahiList.Any(x => x.Date == item.Year + "/" + item.Month + "/" + item.Day))
                {
                    var date = item.Year + "/" + item.Month + "/" + item.Day;
                    res += date + "," + GetCount(model, date) + ";";
                }
            }
            return res.Remove(res.Length - 1);

        }
        private int GetCount(List<ModuleStepTwoResult> model, string date)
        {
            var count = 0;
            var face = 0;
            var director = 0;
            foreach (var item in model)
            {
                if (item.Year + "/" + item.Month + "/" + item.Day == date)
                {
                    var size = GetMinutes(item.EndTime) - GetMinutes(item.StrarTime);
                    var ahi = 0;
                    for (int i = 0; i < item.appnea_indexes?.Length; i++)
                    {
                        ahi += item.appnea_indexes[i];
                    }
                    face += ahi * size;
                    director += size;
                    count = face / director;
                }
                count = 0;
            }
            return count;
        }
        private int GetMinutes(string time)
        {
            var ts = new TimeSpan(Convert.ToInt32(time.Split(':')[0]), Convert.ToInt32(time.Split(':')[1]), 0);
            return Convert.ToInt32(ts.TotalMinutes);
        }
        private List<Int16[]> GetListInDay(List<ModuleStepTwoResult> model, string selectItem)
        {
            var res = new List<Int16[]>();
            var dateList = new List<string>();
            foreach (var item in model)
            {
                if (!dateList.Any(x => x == item.Year + "/" + item.Month + "/" + item.Day))
                {
                    var newDate = item.Year + "/" + item.Month + "/" + item.Day;
                    dateList.Add(newDate);
                    if (selectItem == "Ipap")
                    {
                        var ipapArray = model.Where(x => x.Year + "/" + x.Month + "/" + x.Day == newDate).Select(y => y.IPAP).ToArray();
                        res.Add(ipapArray);
                    }
                    else
                    {
                        var ipapArray = model.Where(x => x.Year + "/" + x.Month + "/" + x.Day == newDate).Select(y => y.EPAP).ToArray();
                        res.Add(ipapArray);
                    }

                }
            }
            return res;
        }
        private List<TotalUsage> GetTottalUsage(List<ModuleStepTwoResult> model)
        {
            var res = new List<TotalUsage>();
            var dateList = new List<string>();
            foreach (var item in model)
            {
                if (!dateList.Any(x => x == item.Year + "/" + item.Month + "/" + item.Day))
                {
                    var newDate = item.Year + "/" + item.Month + "/" + item.Day;
                    dateList.Add(newDate);

                    res.Add(new TotalUsage
                    {
                        Date = newDate,
                        Count = Convert.ToDecimal(Convert.ToDecimal(model.Where(x => x.Year + "/" + x.Month + "/" + x.Day == newDate).Sum(y => y.Length)) / Convert.ToDecimal(288000))
                    });

                }
            }
            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                //textBox1.Text = folderDlg.SelectedPath;

            }
        }
    }
}
