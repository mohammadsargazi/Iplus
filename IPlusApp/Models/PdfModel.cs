using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPlusApp.Models
{
    public class PdfModel
    {
        #region PatientInformation
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string RefrenceId { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        #endregion

        #region ContactDetails
        public string Address { get; set; }
        public string TelephoneNumbers { get; set; }
        public string Email { get; set; }
        #endregion

        #region Insurance
        public string InsuranceCarrier { get; set; }
        public string MembershipNumber { get; set; }
        public string MemberSince { get; set; }
        #endregion

        #region Doctor
        public string TreatingDoctor { get; set; }
        public string ReferingDoctor { get; set; }
        #endregion

        #region Equipment Information
        public string FlowGenerator { get; set; }
        public string FlowGeneratorSoftwareVersion { get; set; }
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
        #endregion
        #region IERatio
        public string IERatioMedian { get; set; }
        public string IERatioPercentile { get; set; }
        public string IERatioMinimum { get; set; }
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
        #endregion


    }
}
