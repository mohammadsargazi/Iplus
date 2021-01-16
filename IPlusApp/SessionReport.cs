using IPlusApp.Functionality;
using IPlusApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace IPlusApp
{
    public partial class SessionReport : Form
    {
        private readonly SessionReportFunctionality _sessionReportFunctionality = new SessionReportFunctionality();
        private List<ModuleStepTwoResult> _selectedSessionList = new List<ModuleStepTwoResult>();
        private readonly CommonFunctionality _commonFunctionality = new CommonFunctionality();
        private string moduleStepTwoResultPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ModuleStepTwoResult.txt");
        int _sessionCount = 0;
        int _selectedSessionCount = -1;

        public SessionReport()
        {

            InitializeComponent();

        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            new ShowSession().Show();
            this.Hide();
        }

        private void ProfileForm_Click(object sender, EventArgs e)
        {
            new Profile().Show();
            this.Hide();
        }

        private void PatientForm_Click(object sender, EventArgs e)
        {
            new PatientProfile().Show();
            this.Hide();
        }
        private double f(int i)
        {
            var f1 = 59894 - (8128 * i) + (262 * i * i) - (1.6 * i * i * i);
            return f1;
        }

        private void SessionReport_Load(object sender, EventArgs e)
        {

            plChart.Controls.Clear();
            plChart.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            // call analize method that write by sina
            _selectedSessionList = _sessionReportFunctionality.AnalizeSelectedSessions();
            _commonFunctionality.WriteToFile(moduleStepTwoResultPath, _selectedSessionList);
            _selectedSessionCount = _selectedSessionList.Count();
            var locationY = 25;
            var plChartWidth = plChart.Size.Width;
            var session = _selectedSessionList[_sessionCount];
            _sessionReportFunctionality.DrawChart(plChart, session.Pressure, "Pressure", locationY, plChartWidth);
            locationY += 220;
            _sessionReportFunctionality.DrawChart(plChart, session.tidal_volume, "TidalVolume", locationY, plChartWidth);
            locationY += 220;
            _sessionReportFunctionality.DrawChart(plChart, session.Leak, "Leak", locationY, plChartWidth);
            locationY += 220;
            _sessionReportFunctionality.DrawChart(plChart, session.Flow, "Flow", locationY, plChartWidth);
            lblTherapyMode.Text = GetModel(session.Mode.ToString());
            lbllblStartRamp.Text = session.Start_Ramp.ToString() + "  s";
            lblAPAPMax.Text = session.Apap_Max.ToString() + "  cmH2O";
            lblIPAP.Text = session.IPAP.ToString() + "  cmH2O";
            lblEPR.Text = session.EPR.ToString() + "  mmH2O";
            lblAPAPMin.Text = session.Apap_Min.ToString() + "  cmH2O";
            lblEPAP.Text = session.EPAP.ToString() + "  cmH2O";
            lblEPRRamp.Text = session.EPR_Ramp.ToString() + "  min";
            lblSenseI.Text = session.SENS_I.ToString() + "  L/Min";
            lblInitialCpap.Text = session.Initial_CPAP.ToString() + "  cmH2O";
            lblVT.Text = session.VT.ToString() + "  ml";
            lblSenseE.Text = session.SENS_E.ToString() + "%   Peak Flow";
            lblPressureCpap.Text = session.Pressure_CPAP.ToString() + "  cmH2O";
            lblSlope.Text = session.Slope.ToString() + "  ms";
            lblRR.Text = session.RR.ToString() + "  BPM";
            lblTIMax.Text = session.Ti_Max.ToString() + "  s";
            lblIPAPMax.Text = session.IPAP_Max.ToString() + "  cmH2O";
            lblHeaterSetPoint.Text = session.HeaterSetPoint.ToString() + "  °C";
            lblTIMin.Text = session.Ti_Min.ToString() + "  s";
            lblIPAPMin.Text = session.IPAP_Min.ToString() + "  cmH2O";

            lbllblTidalVolumeMedian.Text = session.tidal_volumeMedian.ToString() + "  ml";
            lbllblTidalVolumePetcentiale.Text = session.tidal_volumePresent.ToString() + "  ml";
            lbllblTidalVolumeMinimum.Text = session.tidal_volumeMinimum.ToString() + "  ml";

            lblMinuteMentilationMedian.Text = session.minute_vMedian.ToString() + "  ml";
            lblMinuteMentilationPetcentile.Text = session.minute_vPresent.ToString() + "  ml";
            lblMinuteMentilationMinimum.Text = session.minute_vMinimum.ToString() + "  ml";

            lbllblBpmMedian.Text = session.bpmMedian.ToString() + "  BPM";
            lbllblBpmPetcentile.Text = session.bpmPresent.ToString() + "  BPM";
            lbllblBpmMinimum.Text = session.bpmMinimum.ToString() + "  BPM";

            //lbllblAhiMedian.Text = session.appnea_indexesMedian.ToString();
            //lbllblAhiMinimum.Text = session.appnea_indexesMinimum.ToString();
            //lbllblAhiPetcentile.Text = session.appnea_indexesPresent.ToString();

            lbllblAhiMedian.Text = "-";
            lbllblAhiMinimum.Text = "-";
            lbllblAhiPetcentile.Text = "-";

            lbllbllblLeakageMedian.Text = session.LeakMedian.ToString() + "  L/Min";
            lbllbllblLeakageMinimum.Text = session.LeakMinimum.ToString() + "  L/Min";
            lbllbllblLeakagePetcentile.Text = session.LeakPresent.ToString() + "  L/Min";

            lbllblHeaterMedian.Text = session.HeaterMedian.ToString() + "  °C";
            lbllblHeaterMinimum.Text = session.HeaterMinimum.ToString() + "  °C";
            lbllblHeaterPetcentile.Text = session.HeaterPresent.ToString() + "  °C";

            lblAHI.Text = (session.AppneAndHApnne / (session.Length / 288000)).ToString();
            SetDataValue(session);
        }
        private void SetDataValue(ModuleStepTwoResult model)
        {
            if (model.Mode == 6)
            {
                lbllblStartRamp.Text = "-";
                lblAPAPMax.Text = "-";
                lblAPAPMin.Text = "-";
                lblIPAP.Text = "-";
                lblEPRRamp.Text = "-";
                lblInitialCpap.Text = "-";
                lblPressureCpap.Text = "-";
            }
            if (model.Mode == 5 || model.Mode == 4 || model.Mode == 3)
            {
                lbllblStartRamp.Text = "-";
                lblAPAPMax.Text = "-";
                lblAPAPMin.Text = "-";
                lblEPRRamp.Text = "-";
                lblInitialCpap.Text = "-";
                lblPressureCpap.Text = "-";
                lblVT.Text = "-";
                lblIPAPMax.Text = "-";
                lblIPAPMin.Text = "-";
            }
            if (model.Mode == 2)
            {
                lblEPAP.Text = "-";
                lblInitialCpap.Text = "-";
                lblVT.Text = "-";
                lblPressureCpap.Text = "-";
            }
            if (model.Mode == 1)
            {
                lblAPAPMax.Text = "-";
                lblAPAPMin.Text = "-";
                lblTIMax.Text = "-";
                lblTIMin.Text = "-";
                lblIPAPMax.Text = "-";
                lblIPAPMin.Text = "-";
                lblVT.Text = "-";
                lblSlope.Text = "-";
                lblIPAP.Text = "-";
                lblEPAP.Text = "-";
                lblSenseI.Text = "-";
                lblSenseE.Text = "-";
            }

        }
        private string GetModel(string inputModel)
        {
            if (inputModel == "6")
                return "Avaps";
            if (inputModel == "5")
                return "Bipap ST";
            if (inputModel == "4")
                return "Bipap T";
            if (inputModel == "3")
                return "Bipap S";
            if (inputModel == "2")
                return "Apap";
            if (inputModel == "1")
                return "Cpap";
            return "";
        }
        private void lblExportPdf_Click(object sender, EventArgs e)
        {
            new ExportPDF().Show();
        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            lblPreview.Enabled = true;
            _sessionCount += 1;
            SessionReport_Load(sender, e);
            Resizechart();
            if (_sessionCount == _selectedSessionCount - 1)
                lblNext.Enabled = false;
        }

        private void lblPreview_Click(object sender, EventArgs e)
        {
            lblNext.Enabled = true;
            _sessionCount -= 1;
            SessionReport_Load(sender, e);
            Resizechart();
            if (_sessionCount == 0)
                lblPreview.Enabled = false;
        }
        public void Resizechart()
        {
            var plChartWidth = plChart.Size.Width;
            foreach (Control c in plChart.Controls)
            {
                if (c is Chart)
                {
                    c.Size = new Size(plChartWidth - 50, 210);
                    c.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);

                }
            }
            plChart.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
        }
    }
}
