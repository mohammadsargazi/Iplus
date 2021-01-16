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
    public partial class Profile : Form
    {
        private readonly ProfileFunctionality _profileFunctionality = new ProfileFunctionality();
        public Profile()
        {
            InitializeComponent();
            this.Size = new Size(1090, 700);
            SetTextBoxValue();
        }

        private void BtnUpdateProfile_Click(object sender, EventArgs e)
        {
            var model = new DoctorModel
            {
                FullName = fullNametxt.Text,
                Emial = emailtxt.Text,
                DoctorId = doctorIdtxt.Text,
                Clinic = txtClinic.Text
            };
            _profileFunctionality.Update(model);
            new Main().Show();
            this.Hide();
        }
        private void SetTextBoxValue()
        {
            var doctorModel = _profileFunctionality.Get();
            if (doctorModel == null)
                return;
            fullNametxt.Text = doctorModel.FullName;
            emailtxt.Text = doctorModel.Emial;
            doctorIdtxt.Text = doctorModel.DoctorId;
            txtClinic.Text = doctorModel.Clinic;
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
    }
}
