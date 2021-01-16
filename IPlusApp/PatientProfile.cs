using IPlusApp.Functionality;
using IPlusApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPlusApp
{
    public partial class PatientProfile : Form
    {
        private CommonFunctionality _commonFunctionality = new CommonFunctionality();
        private PatientProfileFunctionality _PatientProfileFunctionality = new PatientProfileFunctionality();
        public PatientProfile()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture =
           new System.Globalization.CultureInfo("fa-IR");
        }

        private void picPatientPhoto_Click(object sender, EventArgs e)
        {
            var openFileDialog = _commonFunctionality.GetFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image image = Bitmap.FromFile(openFileDialog.FileName);
                    picPatientPhoto.Image = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("آپلود تصویر با مشکل مواجه شده است.");
                }
            }
        }

        private void btnSavePatientProfile_Click(object sender, EventArgs e)
        {
            var personalId = txtPersonalID.Text;
            if (personalId.Length > 10)
            {
                MessageBox.Show("کد ملی را به درستی وارد نمایید");
                return;
            }

            var patient = new PatientModel
            {
                Address = txtAddress.Text,
                BirthDay = txtBirthDay.Text,
                FullName = txtFullName.Text,
                Gender = txtGender.Text,
                InsuranceDate = txtInsuranceDate.Text,
                InsuranceId = txtInsuranceID.Text,
                InsuranceKinde = txtInsuranceKinde.Text,
                PersonalId = txtPersonalID.Text,
                PhoneNum = txtPhonNum.Text,
                Weight = txtWeigh.Text,
                Age = txtAge.Text,
                Email = lblEmail.Text
            };
            _PatientProfileFunctionality.WritePatientModel(patient);
            new ShowSession().Show();
            this.Hide();
        }

        private void ProfileForm_Click(object sender, EventArgs e)
        {
            new Profile().Show();
            this.Hide();
        }

        private void Patientform_Click(object sender, EventArgs e)
        {
            new PatientProfile().Show();
            this.Hide();
        }

        private void lblBrows_Click(object sender, EventArgs e)
        {
            var openFileDialog = _commonFunctionality.GetFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image image = Bitmap.FromFile(openFileDialog.FileName);
                    picPatientPhoto.Image = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("آپلود تصویر با مشکا مواجه شده است.");
                }
            }
        }

        private void txtWeigh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtPersonalID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
