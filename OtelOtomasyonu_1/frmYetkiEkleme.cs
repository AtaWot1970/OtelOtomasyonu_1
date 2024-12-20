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
    public partial class frmYetkiEkleme : Form
    {
        public frmYetkiEkleme()
        {
            InitializeComponent();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmYetkiliGiris frmYetkiliGiris = new frmYetkiliGiris();
            frmYetkiliGiris.Show();
            this.Hide();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=LAPTOP-1GVNMEQK\\SQLEXPRESS;Initial Catalog=musteridb;Integrated Security=True"; using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); string query = "INSERT INTO yetkilidb (Username, Password) VALUES (@Username, @Password)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yeni yetkili başarıyla eklendi.");
                    // frmYetkiliGiris formuna geçiş
                    frmYetkiliGiris yetkiliGiris = new frmYetkiliGiris();
                    yetkiliGiris.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
    }
}
