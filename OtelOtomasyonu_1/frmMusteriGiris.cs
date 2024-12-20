using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelOtomasyonu_1
{
    public partial class frmMusteriGiris : Form
    {
        public frmMusteriGiris()
        {
            InitializeComponent();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOda1_Click(object sender, EventArgs e)
        {
            //OpenCustomerDetailsForm("SS KİNG VİLLA");
        }

        private void btnOda2_Click(object sender, EventArgs e)
        {
            //OpenCustomerDetailsForm("EŞSİZ DENİZ MANZARASI GRAND SUİTE");
        }

        private void btnOda3_Click(object sender, EventArgs e)
        {
            //OpenCustomerDetailsForm("DELUXE ODA ORMAN MANZARALI");
        }

        private void btnOda4_Click(object sender, EventArgs e)
        {
            //OpenCustomerDetailsForm("TEMALI DELUXE AİLE ODASI ORMAN MANZARALI");
        }


        private void OpenCustomerDetailsForm(string room)
        {
            frmKayit detailsForm = new frmKayit(room); // Oda bilgisini parametre olarak geçiyoruz
            detailsForm.FormClosed += (s, args) => this.Show(); // Ana formu tekrar göster
            this.Hide(); // Ana formu gizle
            detailsForm.Show();
        }

        private void btnOda4_Click_1(object sender, EventArgs e)
        {
            OpenCustomerDetailsForm("TEMALI DELUXE AİLE ODASI ORMAN MANZARALI");
        }

        private void btnOda1_Click_1(object sender, EventArgs e)
        {
            OpenCustomerDetailsForm("SS KİNG VİLLA");
        }

        private void btnOda2_Click_1(object sender, EventArgs e)
        {
            OpenCustomerDetailsForm("EŞSİZ DENİZ MANZARASI GRAND SUİTE");
        }

        private void btnOda3_Click_1(object sender, EventArgs e)
        {
            OpenCustomerDetailsForm("DELUXE ODA ORMAN MANZARALI");
        }
    }
}
