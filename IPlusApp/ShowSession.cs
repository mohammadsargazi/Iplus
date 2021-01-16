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

namespace IPlusApp
{
    public partial class ShowSession : Form
    {
        private readonly ShowSessionFunctionality _showSessionFunctionality = new ShowSessionFunctionality();
        private readonly CommonFunctionality _commonFunctionality = new CommonFunctionality();
        private readonly PatientProfileFunctionality _patientProfileFunctionality = new PatientProfileFunctionality();
        
        public ShowSession()
        {
            InitializeComponent();
        }

        

        private void ProfileForm_Click(object sender, EventArgs e)
        {
            new Profile().Show();
            this.Hide();
        }

        private void lblShowSelectedSession_Click(object sender, EventArgs e)
        {
            //write to another file,all session that checked
            var sessionSelected = new List<string>();
            foreach (Control c in PanelCheckBoxList.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked == true)
                    {
                        sessionSelected.Add(cb.Name);
                    }
                }
            }
            if (sessionSelected.Count() == 0)
            {
                MessageBox.Show("هیچ سشنی برای گزارش انتخاب نشده است.");
                return;
            }
                
            _showSessionFunctionality.WriteSelectedSessions(sessionSelected);
            new SessionReport().Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            new PatientProfile().Show();
            this.Hide();
        }

        private void SelectAllSession_Click(object sender, EventArgs e)
        {
            
        }

       

        private void ShowSession_Load(object sender, EventArgs e)
        {
          
            var sessionList = _commonFunctionality.ReadSessionStepOneModels();
           _showSessionFunctionality.DrawPanelCheckBoxList(PanelCheckBoxList, sessionList);
           _showSessionFunctionality.DrawPanelCalenderBtn(panelCalender, sessionList, PanelCheckBoxList);
            var patient = _patientProfileFunctionality.ReadPatientModel();
            lblfullName.Text = patient.FullName;
            lblBirthday.Text = patient.BirthDay;
            lblPhoneNum.Text = patient.PhoneNum;
            lblPeriodTime.Text = sessionList.Count().ToString();
            //PanelCheckBoxList.VerticalScroll.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control c in PanelCheckBoxList.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked == false)
                    {
                        cb.Checked = true;
                    }
                }
            }
            //for (int i = 0; i < PanelCheckBoxList.Items.Count; i++)
            //    checkedListBox.SetItemChecked(i, true);
            //foreach (Control c in Controls)
            //{
            //    if (c is CheckBox)
            //    {
            //        CheckBox cb = (CheckBox)c;
            //        if (cb.Checked == false)
            //        {
            //            cb.Checked = true;
            //        }
            //    }
            //}
        }

       

        private void SelectAllCheckBox_Click(object sender, EventArgs e)
        {
            foreach (Control c in PanelCheckBoxList.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked == false)
                    {
                        cb.Checked = true;
                    }
                }
            }
        }

        private void DeSelectAllCheckBox_Click(object sender, EventArgs e)
        {
            foreach (Control c in PanelCheckBoxList.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked == true)
                    {
                        cb.Checked = false;
                    }
                }
            }
        }

        private void btnExitPatient_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Hide();
        }
    }
}
