using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.VisualStyles;

namespace Stok_Takip
{
    public partial class Form2 : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-DQHBK23;Initial Catalog=StokTakip;Integrated Security=True");
        bool formTasiniyor = false;
        Point baslangicNoktasi = new Point(0, 0);
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tur = "";
            baglanti.Open();

            string komut = "Select *From Kullanicilar where (KullaniciAdi = '"+ txtAd.Text +"' OR Email = '" + txtAd.Text + "') AND Sifre = '"+ txtSifre.Text +"'";
            SqlCommand islem = new SqlCommand(komut, baglanti);
           
            SqlDataReader oku = islem.ExecuteReader();
            
            if (oku.Read())
            {
                baglanti.Close();
                MessageBox.Show("Sisteme Hoşgeldiniz Sayın " + txtAd.Text,"Stok Takip",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Form1 frm = new Form1();

                frm.kullaniciAdi = txtAd.Text;
                baglanti.Open();
                string kod = "Select KullaniciTuru From Kullanicilar where KullaniciAdi='"+ txtAd.Text +"' OR Email = '"+ txtAd.Text +"'";
                SqlCommand komut2 = new SqlCommand(kod,baglanti);
                SqlDataReader reading = komut2.ExecuteReader();
                while (reading.Read())
                {
                    tur = reading[0].ToString();
                    break;
                }
                
                baglanti.Close();
                frm.KullaniciTuru = tur;
                this.Hide();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı/E-Mail Veya Şifreniz Hatalı !!!","Stok Takip",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }



            baglanti.Close();
        }
       

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kod2 = "Select KullaniciAdi From Kullanicilar where KullaniciAdi ='"+ txtKadi.Text +"'";
            SqlCommand comand = new SqlCommand(kod2,baglanti);
            SqlDataReader oku = comand.ExecuteReader();

            if(oku.Read())
            {
                baglanti.Close();
                MessageBox.Show(txtKadi.Text + " Kullanıcı Adı Alınmış Lütfen Başka bir Kullanıcı Adı Giriniz !","Stok Takip",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                baglanti.Close();
                if (txtName.Text == "" || txtSoyad.Text == "" || txtKadi.Text == "" || txtMail.Text == "" || comboBox1.Text == "" || txtPassword.Text == "" || txtSirket.Text == "")
                {
                    MessageBox.Show("Lütfen Gerekli Tüm Alanları Doldurunuz !", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    baglanti.Open();
                    String islem = "insert into Kullanicilar(Ad,Soyad,Cinsiyet,KullaniciTuru,KullaniciAdi,Email,Sifre,Sirket) values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8)";
                    SqlCommand komut = new SqlCommand(islem, baglanti);

                    komut.Parameters.AddWithValue("@a1", txtName.Text);
                    komut.Parameters.AddWithValue("@a2", txtSoyad.Text);
                    if (radioErkek.Checked == true)
                    {
                        komut.Parameters.AddWithValue("@a3", 1); // The Man
                    }
                    else if (radioBayan.Checked == true)
                    {
                        komut.Parameters.AddWithValue("@a3", 0); // The Women
                    }

                    komut.Parameters.AddWithValue("@a4", comboBox1.Text);
                    komut.Parameters.AddWithValue("@a5", txtKadi.Text);
                    komut.Parameters.AddWithValue("@a6", txtMail.Text);
                    komut.Parameters.AddWithValue("@a7", txtPassword.Text);
                    komut.Parameters.AddWithValue("@a8", txtSirket.Text);

                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Kaydetme İşlemi Başarılı !", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            
       
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor = false;
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi.X, p.Y - this.baslangicNoktasi.Y);
            }
        }
    }
}
