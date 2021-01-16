using System;
using System.Collections.Generic;
using System.Text;

namespace Bipap.Service.Functionality
{
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
        public string ModeStr { get; set; }
    }
    public class MedianAndPresentModel
    {
        public decimal Median { get; set; }
        public decimal Present { get; set; }
        public decimal Minimum { get; set; }

    }
    public class ReportModel
    {
        public bool IsAvaps { get; set; }

        #region Header
        public string ReportedBy { get; set; }
        public string Time { get; set; }
        #endregion

        #region IsChecked
        public bool ChIPAP { get; set; }
        public bool ChEPAP { get; set; }
        public bool ChLEAK { get; set; }
        public bool ChTidalVolume { get; set; }
        public bool ChMinuteVentilation { get; set; }
        public bool ChRespiratoryRate { get; set; }
        public bool ChAHIAI { get; set; }
        public bool ChUsage { get; set; }
        public bool ChPressure { get; set; }
        public bool ChFlow { get; set; }

        #endregion

        #region PatientInformation
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string RefrenceId { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        #endregion

        #region ContactDetails
        public string Address { get; set; }
        public string TelephoneNumbers { get; set; }
        public string Email { get; set; }
        #endregion

        #region Insurance
        public string InsuranceKind { get; set; }
        public string InsuranceId { get; set; }
        public string InsuranceDate { get; set; }
        #endregion

        #region Doctor
        public string TreatingDoctor { get; set; }
        public string Clinic { get; set; }
        #endregion

        #region Equipment Information
        public string FlowGenerator { get; set; }
        public string FlowGeneratorSoftwareVersion { get; set; }
        public string FlowGeneratorSerialNumber { get; set; }
        public string HumidifierSerialNumber { get; set; }

        #endregion

        #region Statics
        #region DeviceSettings
        public string TherapyMode { get; set; }
        public string RampDownEnable { get; set; }
        public string TiMin { get; set; }
        public string TargetAlveolarVentilation { get; set; }
        public string EPAP { get; set; }
        public string RampEnable { get; set; }
        public string Essentials { get; set; }
        public string MaxPS { get; set; }
        public string Height { get; set; }
        public string StartEPAP { get; set; }
        public string RampTime { get; set; }
        public string TiMax { get; set; }
        public string MinPS { get; set; }
        public string TargetPatientRate { get; set; }
        public string ODIThreshold { get; set; }
        #endregion
        #region IPAP
        public string IPAPMedian { get; set; }
        public string IPAPPercentile { get; set; }
        public string IPAPMinimum { get; set; }
        #endregion
        #region Leak
        public string LeakMedian { get; set; }
        public string LeakPercentile { get; set; }
        public string LeakMinimum { get; set; }
        #endregion
        #region TidalVolume
        public string TidalVolumeMedian { get; set; }
        public string TidalVolumePercentile { get; set; }
        public string TidalVolumeMinimum { get; set; }
        #endregion
        #region MinuteVentilation
        public string MinuteVentilationMedian { get; set; }
        public string MinuteVentilationPercentile { get; set; }
        public string MinuteVentilationMinimum { get; set; }
        #endregion
        #region RespiratoryRate
        public string RespiratoryRateMedian { get; set; }
        public string RespiratoryRatePercentile { get; set; }
        public string RespiratoryRateMinimum { get; set; }
        #endregion
        #region RespiratoryIndices
        public string ApneaIndex { get; set; }
        public string HypopneaIndex { get; set; }
        public string AHI { get; set; }
        #endregion
        #region TotalUsage
        public string TotalHoursUsed { get; set; }
        public string MedianDailyUsed { get; set; }
        public string AverageDailyUsed { get; set; }
        public string TotalDays { get; set; }

        #endregion
        #region IERatio
        public string IERatioMedian { get; set; }
        public string IERatioPercentile { get; set; }
        public string IERatioMinimum { get; set; }
        #endregion
        #region IERatioChart
        public string IERatioMedianChart { get; set; }
        public string IERatioPercentileChart { get; set; }
        public string IERatioMinimumChart { get; set; }
        #endregion
        #region InspiratoryTime
        public string InspiratoryTimeMedian { get; set; }
        public string InspiratoryTimePercentile { get; set; }
        public string InspiratoryTimeMinimum { get; set; }
        #endregion
        #region AlveolarVentilation
        public string AlveolarVentilationMedian { get; set; }
        public string AlveolarVentilationPercentile { get; set; }
        public string AlveolarVentilationMinimum { get; set; }
        #endregion
        #region Ipap
        public string IpapMedianArray { get; set; }
        public string IpapPrecentileArray { get; set; }
        public string IpapMinimumArray { get; set; }
        #endregion
        #region Epap
        public string EpapMedianArray { get; set; }
        public string EpapPrecentileArray { get; set; }
        public string EpapMinimumArray { get; set; }
        #endregion
        #region Leak
        public string LeakMinimumArray { get; set; }
        public string LeakMedianArray { get; set; }
        public string LeakPrecentileArray { get; set; }
        #endregion
        #region TidalVolume
        public string TidalVolumeMinimumArray { get; set; }
        public string TidalVolumeMedianArray { get; set; }
        public string TidalVolumePrecentileArray { get; set; }
        #endregion
        #region MinuteVentilation
        public string MinuteVentilationMinimumArray { get; set; }
        public string MinuteVentilationMedianArray { get; set; }
        public string MinuteVentilationPrecentileArray { get; set; }
        #endregion
        #region RespiratoryRate
        public string RespiratoryRateMinimumArray { get; set; }
        public string RespiratoryRateMedianArray { get; set; }
        public string RespiratoryRatePrecentileArray { get; set; }
        #endregion
        #region AHI&TotalUsage
        public string Ahi { get; set; }
        public string TotalUsage { get; set; }
        public string ERatioChart { get; set; }
        public string IRatioChart { get; set; }
        #endregion



        #endregion




    }
    public class TotalUsage
    {
        public string Date { get; set; }
        public decimal Count { get; set; }
    }
    public class AhiModel
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }

}
