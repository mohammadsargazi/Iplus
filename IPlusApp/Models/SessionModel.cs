using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPlusApp.Models
{
    public class SessionModel
    {
        public string Date { get; set; }
        public string Time { get; set; }
    }

    public class SessionStepOneModel
    {
        public string Name { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Hour { get; set; }
        public string Minutes { get; set; }
        public string StrarTime { get; set; }
        public string EndTime { get; set; }
        public string SessionLength { get; set; }
        public float[] Parameters { get; set; }
        public Int16[] Pressure { get; set; }
        public Int16[] Flow { get; set; }
        public Int16[] Heater { get; set; }
        public Int16[] Leak { get; set; }
        public long Length { get; set; }
    }
    public class MoqData
    {
        public List<SessionModel> GetSessionModels()
        {
            var list = new List<SessionModel>();
            for (int i = 0; i < 50; i++)
            {
                list.Add(new SessionModel { Date = DateTime.Now.ToShortDateString(), Time = (i + 1).ToString() });
            }
            return list;
        }
    }

    public class ModuleStepTwoResult
    {
        public Int16 FlagStart { get; set; }
        public Int16 Initial_CPAP { get; set; }
        public Int16 Pressure_CPAP { get; set; }
        public Int16 Start_Ramp { get; set; }
        public Int16 Flag_Epr { get; set; }
        public Int16 EPR { get; set; }
        public Int16 EPR_Ramp { get; set; }
        public Int16 Mode { get; set; }
        public Int16 Delta_T_P { get; set; }
        public Int16 Delta_T_N { get; set; }
        public Int16 Delta_P { get; set; }
        public Int16 Apap_Min { get; set; }
        public Int16 Apap_Max { get; set; }
        public Int16 Flag_Apnea { get; set; }
        public Int16 IPAP { get; set; }
        public Int16 EPAP { get; set; }
        public Int16 SENS_I { get; set; }
        public Int16 SENS_E { get; set; }
        public Int16 Slope { get; set; }
        public Int16 ARAMP { get; set; }
        public Int16 Bti { get; set; }
        public Int16 Bstart_Delay { get; set; }
        public Int16 RR { get; set; }
        public float Ti { get; set; }
        public float Ti_Min { get; set; }
        public float Ti_Max { get; set; }
        public Int16 IPAP_Min { get; set; }
        public Int16 IPAP_Max { get; set; }
        public Int16 VT { get; set; }
        public Int16 IE { get; set; }
        public Int16 ST { get; set; }
        public Int16 VT_Only_Read { get; set; }
        public Int16 Toatal_Leak { get; set; }
        public Int16 Mask_leak { get; set; }
        public Int16 Minute_Ventilation { get; set; }
        public float LastTI { get; set; }
        public float LastTE { get; set; }
        public Int16 HeaterSetPoint { get; set; }
        public Int16 HeaterTemp { get; set; }
        public Int16 Motor { get; set; }
        public Int16 Comfort { get; set; }
        public Int16 UID0 { get; set; }
        public Int16 UID1 { get; set; }
        public Int16 UID2 { get; set; }
        public Int16 UID3 { get; set; }
        public Int16 UID4 { get; set; }
        public Int16 UID5 { get; set; }
        public Int16 Version { get; set; }
        public Int16 UUID0 { get; set; }
        public Int16 UUID1 { get; set; }
        public Int16 UUID2 { get; set; }
        public Int16 UUID3 { get; set; }
        public Int16 UUID4 { get; set; }
        public Int16 UUID5 { get; set; }
        public Int16 UVesrion { get; set; }
        public Int16 RTCH { get; set; }
        public Int16 RTCL { get; set; }
        public Int16 RTC_OFS_GRE_H { get; set; }
        public Int16 RTC_OFS_GRE_L { get; set; }

        //public int appnea { get; set; }
        //public int h_appnea { get; set; }
        //public int noa { get; set; }
        public int[] minute_v { get; set; }
        public decimal minute_vMedian { get; set; }
        public decimal minute_vPresent { get; set; }
        public decimal minute_vMinimum { get; set; }
        public int[] bpm { get; set; }
        public decimal bpmMedian { get; set; }
        public decimal bpmPresent { get; set; }
        public decimal bpmMinimum { get; set; }
        public int[] tidal_volume { get; set; }
        public decimal tidal_volumeMedian { get; set; }
        public decimal tidal_volumePresent { get; set; }
        public decimal tidal_volumeMinimum { get; set; }
        public int[] appnea_indexes { get; set; }

        public decimal appnea_indexesMedian { get; set; }
        public decimal appnea_indexesPresent { get; set; }
        public decimal appnea_indexesMinimum { get; set; }


        public decimal I_ratioMedian { get; set; }
        public decimal I_ratioPresent { get; set; }
        public decimal I_ratioMinimum { get; set; }

        public decimal E_ratioMedian { get; set; }
        public decimal E_ratioPresent { get; set; }
        public decimal E_ratioMinimum { get; set; }
        public Int16[] Parameters { get; set; }
        public Int16[] Pressure { get; set; }
        public Int16[] Flow { get; set; }
        public Int16[] Heater { get; set; }
        public decimal HeaterMinimum { get; set; }
        public decimal HeaterMedian { get; set; }
        public decimal HeaterPresent { get; set; }
        public Int16[] Leak { get; set; }
        public decimal LeakMinimum { get; set; }
        public decimal LeakMedian { get; set; }
        public decimal LeakPresent { get; set; }
        public long Length { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string FileLength { get; set; }
        public string StrarTime { get; set; }
        public string EndTime { get; set; }
        public float AppneAndHApnne { get; set; }
        public int Appne { get; set; }
        public int HApne { get; set; }
        public int[] InspiratoryTimeArray { get; set; }
    }

    public class AhiModel
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }
    public class TotalUsage
    {
        public string Date { get; set; }
        public decimal Count { get; set; }
    }
}
