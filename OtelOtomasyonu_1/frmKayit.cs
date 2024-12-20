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
    public partial class frmKayit : Form
    {
        public frmKayit(string room)
        {
            InitializeComponent();
            txtDogumTarihi.KeyPress += new KeyPressEventHandler(txtDogumTarihi_KeyPress);
            txtDogumTarihi.TextChanged += new EventHandler(txtDogumTarihi_TextChanged);
            txtTcNo.KeyPress += new KeyPressEventHandler(txtTcNo_KeyPress);
            txtTcNo.TextChanged += new EventHandler(txtTcNo_TextChanged);
            //comboBox1.Items.AddRange(new object[] { "Oda 1", "Oda 2", "Oda 3","Oda 4" });
            selectedRoom = room;
            lblSelectedRoom.Text = $"Seçilen Oda: {room}";
        }

        private string selectedRoom;

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmMusteriGiris musteriGiris = new frmMusteriGiris();
            musteriGiris.Show();
            this.Hide();
        }

        private void txtIsim_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }

        private void txtSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }

        private void txtTcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtDogumTarihi_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Yalnızca sayı ve nokta karakterlerini kabul et
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDogumTarihi_TextChanged(object sender, EventArgs e)
        {
            /*
            // Textbox'ın maksimum uzunluğunu kontrol et
            if (txtDogumTarihi.Text.Length > 10)
            {
                txtDogumTarihi.Text = txtDogumTarihi.Text.Substring(0, 10);
                txtDogumTarihi.SelectionStart = txtDogumTarihi.Text.Length;
                // İmleci sonuna taşır
            } 
            // Format kontrolü (örnek: 12.12.2004)
            string text = txtDogumTarihi.Text;
            if (text.Length == 2 || text.Length == 5)
            {
                if (text[text.Length - 1] != '.')
                {
                    txtDogumTarihi.Text = text.Substring(0, text.Length - 1) + "." + text[text.Length - 1];
                    txtDogumTarihi.SelectionStart = txtDogumTarihi.Text.Length;
                    // İmleci sonuna taşır
                } 
            }
            */
            if (txtDogumTarihi.Text.Length > 10)
            {
                txtDogumTarihi.Text = txtDogumTarihi.Text.Substring(0, 10);
                txtDogumTarihi.SelectionStart = txtDogumTarihi.Text.Length;
                // İmleci sonuna taşır
            }
        }

        private void txtTcNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTcNo.Text.Length > 11)
            {
                txtTcNo.Text = txtTcNo.Text.Substring(0, 11);
                txtTcNo.SelectionStart = txtTcNo.Text.Length;
                // İmleci sonuna taşır
            }
            if (txtTcNo.Text.Length == 11)
            {
                // TC Kimlik No'nun geçerliliğini burada kontrol edebilirsiniz
                if (!IsValidTcKimlik(txtTcNo.Text))
                {
                    MessageBox.Show("Geçersiz TC Kimlik Numarası. Lütfen doğru giriniz.");
                }
            }
        }

        private bool IsValidTcKimlik(string tcKimlik)
        {
            // Bu metot, TC Kimlik No'nun geçerli olup olmadığını kontrol eder
            // Basit bir örnek kontrol, gerçek dünya uygulamaları için daha gelişmiş kontroller yapılabilir
            return tcKimlik.Length == 11 && long.TryParse(tcKimlik, out _);
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtAdres.Text = "";
            txtMail.Text = "";
            txtSoyad.Text = "";
            txtIsim.Text = "";
            txtDogumTarihi.Text = "";
            txtTcNo.Text = "";
            mtxtTelNo.Text = "";
            lblCinsiyetKontrol.Text = "ERKEK";
            rbErkek.Checked = true;
            rbKadin.Checked = false;
            txtTcNo.Focus();
            comboBox1.SelectedIndex = -1;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=LAPTOP-1GVNMEQK\\SQLEXPRESS;Initial Catalog=musteridb;Integrated Security=True";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO Customers (TcNo, FirstName, LastName, BirthDate, Phone, Email, Address, Room, Gender) VALUES (@TcNo, @FirstName, @LastName, @BirthDate, @Phone, @Email, @Address, @Room, @Gender)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@TcNo", txtTcNo.Text);
                    cmd.Parameters.AddWithValue("@FirstName", txtIsim.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtSoyad.Text);
                    cmd.Parameters.AddWithValue("@BirthDate", txtDogumTarihi.Text);
                    cmd.Parameters.AddWithValue("@Phone", mtxtTelNo.Text);
                    cmd.Parameters.AddWithValue("@Email", txtMail.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAdres.Text);
                    cmd.Parameters.AddWithValue("@Room", comboBox1.Text);

                    string gender = rbErkek.Checked ? "Erkek" : "Kadın";
                    cmd.Parameters.AddWithValue("@Gender", gender);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} müşteri kaydedildi.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void rbErkek_CheckedChanged(object sender, EventArgs e)
        {
            lblCinsiyetKontrol.Text = "ERKEK";
        }

        private void rbKadin_CheckedChanged(object sender, EventArgs e)
        {
            lblCinsiyetKontrol.Text = "KADIN";
        }
    }
}
