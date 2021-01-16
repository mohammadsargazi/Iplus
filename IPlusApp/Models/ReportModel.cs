using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace IPlusApp.Models
{
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
        public string ERatioChart { get;  set; }
        public string IRatioChart { get; set; }
        #endregion

    

        #endregion




    }
}
