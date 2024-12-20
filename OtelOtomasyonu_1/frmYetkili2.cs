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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace OtelOtomasyonu_1
{
    public partial class frmYetkili2 : Form
    {
        public frmYetkili2(string user)
        {
            InitializeComponent();
            username = user;
            lblYetkiliAd.Text = $"Yetkili: {username}";
            GosterKayitlar();

            // comboBox1 için cinsiyet seçeneklerini doldur
            comboBox1.Items.AddRange(new object[] { "Tümü", "Erkek", "Kadın" });
            comboBox1.SelectedIndex = 0; // Varsayılan olarak "Tümü" seçili olsun

            // comboBox2 için diğer sıralama seçeneklerini doldur
            comboBox2.Items.AddRange(new object[] { "TcNo", "FirstName", "LastName", "Phone", "Room" });
            comboBox2.SelectedIndex = 0; // Varsayılan olarak ilk seçenek seçili olsun

            // rbArtan seçili olacak
            rbArtan.Checked = true;
        }

        private string username;

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

        private void GosterKayitlar()
        {
            string connectionString = "Data Source=LAPTOP-1GVNMEQK\\SQLEXPRESS;Initial Catalog=musteridb;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open(); string query = "SELECT * FROM Customers WHERE CreatedBy=@Username";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(); da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string genderFilter = comboBox1.SelectedItem.ToString();
            string sortOrder = comboBox2.SelectedItem.ToString();
            string sortDirection = rbArtan.Checked ? "ASC" : "DESC";

            string query = "SELECT * FROM Customers";

            // Cinsiyet filtresi ekle
            if (genderFilter != "Tümü")
            {
                query += $" WHERE Gender = '{genderFilter}'";
            }

            // Sıralama ekle
            query += $" ORDER BY {sortOrder} {sortDirection}";

            string connectionString = "Data Source=LAPTOP-1GVNMEQK\\SQLEXPRESS;Initial Catalog=musteridb;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(); da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
    }
}
