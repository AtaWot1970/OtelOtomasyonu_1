using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelOtomasyonu_1
{
    public partial class frmYetkiliGiris : Form
    {
        public frmYetkiliGiris(string username = "", string password = "")
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                txtUsername.Text = username;
                txtPassword.Text = password;
            }
        }



        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            frmYetkiEkleme yetkiEkleme = new frmYetkiEkleme();
            yetkiEkleme.Show();
            this.Hide();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=LAPTOP-1GVNMEQK\\SQLEXPRESS;Initial Catalog=musteridb;Integrated Security=True"; using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); string query = "SELECT COUNT(*) FROM yetkilidb WHERE Username=@Username AND Password=@Password"; SqlCommand cmd = new SqlCommand(query, con); cmd.Parameters.AddWithValue("@Username", txtUsername.Text); cmd.Parameters.AddWithValue("@Password", txtPassword.Text); int count = (int)cmd.ExecuteScalar(); if (count > 0)
                    {
                        frmYetkili2 yetkili2 = new frmYetkili2(txtUsername.Text);
                        // Kullanıcı adını parametre olarak geçiyoruz
                        yetkili2.Show(); this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz kullanıcı adı veya şifre.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
    }
}
