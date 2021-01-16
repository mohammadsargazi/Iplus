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
    public partial class Main : Form
    {
        private CommonFunctionality _commonFunctionality = new CommonFunctionality();
        public Main()
        {
            InitializeComponent();
        }

        private void ProfileForm_Click(object sender, EventArgs e)
        {
            new Profile().Show();
            this.Hide();
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            var sessionStepOneModels = new List<SessionStepOneModel>();
            var openFileDialog = _commonFunctionality.GetFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var fileName in openFileDialog.FileNames)
                {
                    var fileLength = new System.IO.FileInfo(fileName).Length;
                    if (fileLength < 2000)
                        continue;
                    var sessionstepOneModel = _commonFunctionality.GetSessionStepOneModel(fileName);
                    sessionStepOneModels.Add(sessionstepOneModel);
                }
            }
            _commonFunctionality.WriteSessionStepOneModels(sessionStepOneModels);
            new PatientProfile().Show();
            this.Hide();
        }

        private void ProfileForm_Click_1(object sender, EventArgs e)
        {
            new Profile().Show();
            this.Hide();
        }

        private void PatientForm_Click(object sender, EventArgs e)
        {
            new PatientProfile().Show();
            this.Hide();
        }
    }
}
