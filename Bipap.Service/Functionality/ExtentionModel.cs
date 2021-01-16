using Bipap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bipap.Service.Functionality
{
    public static class ExtentionModel
    {
        public static ModuleStepTwoResult ToModel(this SessionStepOneModel model)
        {
            if (model == null)
                return null;
            return new ModuleStepTwoResult
            {
                FlagStart = (Int16)model.Parameters[0],
                Initial_CPAP = (Int16)model.Parameters[1],
                Pressure_CPAP = (Int16)(model.Parameters[2] / 10),
                Start_Ramp = (Int16)(model.Parameters[3] / 10),
                Flag_Epr = (Int16)model.Parameters[4],
                EPR = (Int16)(model.Parameters[5] / 10),
                EPR_Ramp = (Int16)(model.Parameters[6] / 60000),
                Mode = (Int16)model.Parameters[7],
                Delta_T_P = (Int16)(model.Parameters[8] / 10),
                Delta_T_N = (Int16)(model.Parameters[9] / 10),
                Delta_P = (Int16)(model.Parameters[10] / 10),
                Apap_Min = (Int16)(model.Parameters[11] / 10),
                Apap_Max = (Int16)(model.Parameters[12] / 10),
                Flag_Apnea = (Int16)model.Parameters[13],
                IPAP = (Int16)(model.Parameters[14] / 10),
                EPAP = (Int16)(model.Parameters[15] / 10),
                SENS_I = (Int16)model.Parameters[16],
                SENS_E = (Int16)model.Parameters[17],
                Slope = (Int16)model.Parameters[18],
                ARAMP = (Int16)model.Parameters[19],
                Bti = (Int16)(model.Parameters[20] / 10),
                Bstart_Delay = (Int16)(model.Parameters[21] / 10),
                RR = (Int16)model.Parameters[22],
                Ti = (Int16)(model.Parameters[23] / 10),
                Ti_Min = (Int16)(model.Parameters[24] / 10),
                Ti_Max = (Int16)(model.Parameters[25] / 10),
                IPAP_Min = (Int16)(model.Parameters[26] / 10),
                IPAP_Max = (Int16)(model.Parameters[27] / 10),
                VT = (Int16)model.Parameters[28],
                IE = (Int16)model.Parameters[29],
                ST = (Int16)model.Parameters[30],
                VT_Only_Read = (Int16)model.Parameters[31],
                Toatal_Leak = (Int16)(model.Parameters[32] * 0.02),
                Mask_leak = (Int16)(model.Parameters[33] * 0.02),
                Minute_Ventilation = (Int16)(model.Parameters[34] / 10),
                LastTI = (Int16)(model.Parameters[35] / 100),
                LastTE = (Int16)(model.Parameters[36] / 100),
                HeaterSetPoint = (Int16)(model.Parameters[37] / 100),
                HeaterTemp = (Int16)(model.Parameters[38] / 100),
                Motor = (Int16)model.Parameters[39],
                Comfort = (Int16)model.Parameters[40],
                UID0 = (Int16)model.Parameters[41],
                UID1 = (Int16)model.Parameters[42],
                UID2 = (Int16)model.Parameters[43],
                UID3 = (Int16)model.Parameters[44],
                UID4 = (Int16)model.Parameters[45],
                UID5 = (Int16)model.Parameters[46],
                Version = (Int16)model.Parameters[47],
                UUID0 = (Int16)model.Parameters[48],
                UUID1 = (Int16)model.Parameters[49],
                UUID2 = (Int16)model.Parameters[50],
                UUID3 = (Int16)model.Parameters[51],
                UUID4 = (Int16)model.Parameters[52],
                UUID5 = (Int16)model.Parameters[53],
                UVesrion = (Int16)model.Parameters[54],
                RTCH = (Int16)model.Parameters[55],
                RTCL = (Int16)model.Parameters[56],
                RTC_OFS_GRE_H = (Int16)model.Parameters[57],
                RTC_OFS_GRE_L = (Int16)model.Parameters[58],
                Pressure = model.Pressure,
                Flow = model.Flow,
                Heater = model.Heater,
                Leak = model.Leak,
                Year = model.Year,
                Month = model.Month,
                Day = model.Day,
                StrarTime = model.StrarTime,
                EndTime = model.EndTime,
                Length = model.Length
            };
        }
        public static SessionStepOneModel ToModel(this StepOneModule model)
        {
            if (model == null)
                return null;
            return new SessionStepOneModel
            {
                Day = model.Day,
                EndTime = model.EndTime,
                Pressure = model.Pressure.Split(',').Select(x => Convert.ToInt16(x)).ToArray(),// model.Pressure,
                Flow = model.Flow.Split(',').Select(x => Convert.ToInt16(x)).ToArray(),
                Heater = model.Heater.Split(',').Select(x => Convert.ToInt16(x)).ToArray(),
                Leak = model.Leak.Split(',').Select(x => Convert.ToInt16(x)).ToArray(),
                Hour = model.Hour,
                Length = model.Length,
                Minutes = model.Minutes,
                Month = model.Month,
                Name = model.Name,
                Parameters = model.Parameters.Split(',').Select(x => float.Parse(x)).ToArray(),
                SessionLength = model.SessionLength,
                StrarTime = model.StrarTime,
                Year = model.Year,
            };
        }

        public static MedianAndPresentModel ToMedianAndPresentModel(this int[] array)
        {
            Array.Sort(array);

            decimal[] median_95percent = new decimal[2];

            if (array.Length >= 1)
            {
                if (array.Length % 2 == 1)
                    median_95percent[0] = (decimal)array[array.Length / 2];


                else
                    median_95percent[0] = (decimal)((array[array.Length / 2] + array[(array.Length / 2) - 1]) / 2);
                int index = (int)Math.Round((double)(array.Length * 0.95));
                median_95percent[1] = array[index - 1];
            }


            return new MedianAndPresentModel
            {
                Median = median_95percent[0],
                Present = median_95percent[1]
            };
        }
        public static MedianAndPresentModel ToMedianAndPresentModel(this Int16[] array)
        {
            Array.Sort(array);

            decimal[] median_95percent = new decimal[2];

            if (array.Length >= 1)
            {
                if (array.Length % 2 == 1)
                    median_95percent[0] = (decimal)array[array.Length / 2];


                else
                    median_95percent[0] = (decimal)((array[array.Length / 2] + array[(array.Length / 2) - 1]) / 2);
                int index = (int)Math.Round((double)(array.Length * 0.95));
                median_95percent[1] = array[index - 1];
            }


            return new MedianAndPresentModel
            {
                Median = median_95percent[0],
                Present = median_95percent[1],
                Minimum = array.Min()
            };
        }
        public static MedianAndPresentModel ToMedianAndPresentModel(this decimal[] array)
        {
            Array.Sort(array);

            decimal[] median_95percent = new decimal[2];

            if (array.Length >= 1)
            {
                if (array.Length % 2 == 1)
                    median_95percent[0] = (decimal)array[array.Length / 2];


                else
                    median_95percent[0] = (decimal)((array[array.Length / 2] + array[(array.Length / 2) - 1]) / 2);
                int index = (int)Math.Round((double)(array.Length * 0.95));
                median_95percent[1] = array[index - 1];
            }


            return new MedianAndPresentModel
            {
                Median = median_95percent[0],
                Present = median_95percent[1],
                Minimum = array.Min()
            };
        }
        public static ReportModel ToReportModel(this List<ModuleStepTwoResult> stepTwoResult, Patient patient,Doctor doctor)
        {
            if (stepTwoResult.Count() == 0)
                return null;
            var ipapArray = GetListInDay(stepTwoResult, "Ipap");
            var epapArray = GetListInDay(stepTwoResult, "Epap");
            var totalUseTime = stepTwoResult.Sum(x => x.Length) / 288000;
            return new ReportModel
            {
                ChIPAP = true,
                ChEPAP = true,
                ChLEAK = true,
                ChTidalVolume = true,
                ChMinuteVentilation = true,
                ChRespiratoryRate = true,
                ChAHIAI = true,
                ChUsage = true,
                ChFlow = true,
                ChPressure = true,
                PatientName = patient.FullName,
                PatientId = patient?.PersonalId,
                DateOfBirth = patient?.DateOfBirth.ToString(),
                Gender = patient.Gender?.Title.ToString(),
                Address = patient?.Address,
                Age = patient.Age.ToString(),
                TelephoneNumbers = patient.Mobile,
                Email = patient.Email,
                InsuranceKind = patient.InsuranceKinde,
                InsuranceDate = patient.InsuranceDate.ToString(),
                InsuranceId = patient.InsuranceId.ToString(),
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
                IERatioMedian = (1 / (stepTwoResult.Select(x => x.E_ratioMedian).ToArray().ToMedianAndPresentModel().Median / stepTwoResult.Select(x => x.I_ratioMedian).ToArray().ToMedianAndPresentModel().Median)).ToString(),
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
                ReportedBy = "",
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
                ERatioChart = ToStringModel(stepTwoResult.Select(x => x.E_ratioMedian).ToList()),
                IRatioChart = ToStringModel(stepTwoResult.Select(x => x.I_ratioMedian).ToList()),
                TreatingDoctor = doctor.FirstName + " "+doctor.LastName ,
                Clinic = doctor.Clinic,

            };
        }
        private static List<TotalUsage> GetTottalUsage(List<ModuleStepTwoResult> model)
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
       
        private static List<Int16[]> GetListInDay(List<ModuleStepTwoResult> model, string selectItem)
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

        private static string GetTime(DateTime dateTime)
        {
            return dateTime.ToString("hh:mm tt");
        }
        private static string ToStringModel(List<decimal> model)
        {
            var res = "";
            foreach (var item in model)
            {
                res = res + item + ",";
            }
            return res.Remove(res.Length - 1);
        }
        private static string GetModel(this short inputModel)
        {
            if (inputModel == 6)
                return "Avaps";
            if (inputModel == 5)
                return "Bipap ST";
            if (inputModel == 4)
                return "Bipap T";
            if (inputModel == 3)
                return "Bipap S";
            if (inputModel == 2)
                return "Apap";
            if (inputModel == 1)
                return "Cpap";
            return "";
        }
        private static int GetMinutes(string time)
        {
            var ts = new TimeSpan(Convert.ToInt32(time.Split(':')[0]), Convert.ToInt32(time.Split(':')[1]), 0);
            return Convert.ToInt32(ts.TotalMinutes);
        }

        private static int GetCount(List<ModuleStepTwoResult> model, string date)
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
        private static string ToAhiModel(List<ModuleStepTwoResult> model)
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
        private static string ToStingModel(List<TotalUsage> model)
        {
            var res = "";
            foreach (var item in model)
            {
                res = res + item.Date + "," + item.Count + ";";
            }
            return res.Remove(res.Length - 1);
        }
        public static int GetDayCountFromStepOneModules(this List<StepOneModule> stepOneModules)
        {
            if (stepOneModules.Count() == 0)
                return 0;
            var dayCount = 0;
            var sessionGroupYear = stepOneModules.GroupBy(x => x.Year).OrderBy(x => x.Key).ToList();
            foreach (var sesstionYear in sessionGroupYear)
            {
                var sessionGroupMonth = sesstionYear.GroupBy(x => x.Month).OrderBy(x => x.Key).ToList();
                foreach (var sessionMonth in sessionGroupMonth)
                {
                    var sessionGroupDay = sessionMonth.GroupBy(x => x.Day).ToList();
                    //isFirstLabel = true;
                    foreach (var sessions in sessionGroupDay)
                    {
                        dayCount++;
                    }
                }
            }
            return dayCount;
        }
    }
}
