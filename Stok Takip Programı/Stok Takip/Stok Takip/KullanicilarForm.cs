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

namespace Stok_Takip
{
    public partial class KullanicilarForm : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-DQHBK23;Initial Catalog=StokTakip;Integrated Security=True");
        public KullanicilarForm()
        {
            InitializeComponent();
        }

        private void KullanicilarForm_Load(object sender, EventArgs e)
        {
            Yenile();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtKullaniciId.Text = dataGridView1.Rows[secilen].Cells["KullaniciId"].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells["Ad"].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells["Soyad"].Value.ToString();
            txtKullaniciTuru.Text = dataGridView1.Rows[secilen].Cells["KullaniciTuru"].Value.ToString();
            txtKullaniciAdi.Text = dataGridView1.Rows[secilen].Cells["KullaniciAdi"].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[secilen].Cells["Email"].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells["Sifre"].Value.ToString();
            txtSirket.Text = dataGridView1.Rows[secilen].Cells["Sirket"].Value.ToString();

            if(dataGridView1.Rows[secilen].Cells["Cinsiyet"].Value.ToString() == "True")
            {
                radioErkek.Checked = true;
            }
            else
            {
                radioBayan.Checked = true;
            }
        }
        private void Yenile()
        {
            baglanti.Open();
            string Select = "Select *From Kullanicilar";
            SqlDataAdapter da = new SqlDataAdapter(Select, baglanti);
            DataTable df = new DataTable();
            da.Fill(df);
            dataGridView1.DataSource = df;
            baglanti.Close();
        }
        private void Temizle()
        {
            txtKullaniciId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtKullaniciTuru.Text = "";
            txtKullaniciAdi.Text = "";
            txtEmail.Text = "";
            txtSifre.Text = "";
            txtSirket.Text = "";
            radioBayan.Checked = false;
            radioErkek.Checked = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private bool KullaniciAdiKontrol()
        {
            baglanti.Open();
            string kod2 = "Select KullaniciAdi From Kullanicilar where KullaniciAdi ='" + txtKullaniciAdi.Text + "'";
            SqlCommand comand = new SqlCommand(kod2, baglanti);
            SqlDataReader oku = comand.ExecuteReader();

            if (oku.Read())
            {
                baglanti.Close();
                MessageBox.Show(txtKullaniciAdi.Text + " Kullanıcı Adı Alınmış Lütfen Başka bir Kullanıcı Adı Giriniz !","StokTakip",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            else
            {
                baglanti.Close();
                return true;
            }
            baglanti.Close();
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtKullaniciTuru.Text == "" || txtKullaniciAdi.Text == "" || txtEmail.Text == "" || txtSifre.Text == "" || txtSirket.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Değerleri Eksiksiz Giriniz !", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if(KullaniciAdiKontrol() == false)
                {

                }
                else
                {
                    baglanti.Open();

                    string kod1 = "insert into Kullanicilar(Ad,Soyad,KullaniciTuru,KullaniciAdi,Email,Sifre,Sirket,Cinsiyet) Values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8)";
                    SqlCommand komut1 = new SqlCommand(kod1,baglanti);
                    komut1.Parameters.AddWithValue("@a1", txtAd.Text);
                    komut1.Parameters.AddWithValue("@a2", txtSoyad.Text);
                    komut1.Parameters.AddWithValue("@a3", txtKullaniciTuru.Text);
                    komut1.Parameters.AddWithValue("@a4", txtKullaniciAdi.Text);
                    komut1.Parameters.AddWithValue("@a5", txtEmail.Text);
                    komut1.Parameters.AddWithValue("@a6", txtSifre.Text);
                    komut1.Parameters.AddWithValue("@a7", txtSirket.Text);
                    if(radioErkek.Checked == true)
                    {
                        komut1.Parameters.AddWithValue("@a8", 1);
                    }
                    else
                    {
                        komut1.Parameters.AddWithValue("@a8", 0);
                    }
                    komut1.ExecuteNonQuery();

                    MessageBox.Show(txtAd.Text + "  Kullanıcısı Başarıyla Kaydedilmiştir.", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    baglanti.Close();
                    Temizle();
                    Yenile();
                    
                }
               
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtKullaniciId.Text == "")
            {
                MessageBox.Show("Lütfen Kullanıcı Id Değerini Giriniz !", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                baglanti.Open();

                string islem9 = "Delete From Kullanicilar where KullaniciId = '" + txtKullaniciId.Text + "'";
                SqlCommand komut9 = new SqlCommand(islem9, baglanti);
                komut9.ExecuteNonQuery();
                MessageBox.Show(txtKullaniciAdi.Text + "  Kullanıcısı Başarıyla Silindi.", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);

                baglanti.Close();
                Yenile();
                Temizle();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtKullaniciTuru.Text == "" || txtKullaniciAdi.Text == "" || txtEmail.Text == "" || txtSifre.Text == "" || txtSirket.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Değerleri Eksiksiz Giriniz !", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ////////////

                baglanti.Open();
                string kod2 = "Select KullaniciAdi From Kullanicilar where KullaniciId != '"+ txtKullaniciId.Text +"' AND KullaniciAdi ='" + txtKullaniciAdi.Text + "'";
                SqlCommand comand = new SqlCommand(kod2, baglanti);
                SqlDataReader oku = comand.ExecuteReader();

                if (oku.Read())
                {
                    baglanti.Close();
                    MessageBox.Show(txtKullaniciAdi.Text + " Kullanıcı Adı Alınmış Lütfen Başka bir Kullanıcı Adı Giriniz !","Stok Takip",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    baglanti.Close();
                    // güncelleme
                    baglanti.Open();

                    string kod = "Update Kullanicilar Set Ad = @a1, Soyad = @a2, KullaniciTuru = @a3, KullaniciAdi = @a4, Email = @a5, Sifre = @a6, Sirket = @a7, Cinsiyet = @a8 where KullaniciId = '" + txtKullaniciId.Text + "'";
                    SqlCommand komut = new SqlCommand(kod,baglanti);

                    komut.Parameters.AddWithValue("@a1", txtAd.Text);
                    komut.Parameters.AddWithValue("@a2", txtSoyad.Text);
                    komut.Parameters.AddWithValue("@a3", txtKullaniciTuru.Text);
                    komut.Parameters.AddWithValue("@a4", txtKullaniciAdi.Text);
                    komut.Parameters.AddWithValue("@a5", txtEmail.Text);
                    komut.Parameters.AddWithValue("@a6", txtSifre.Text);
                    komut.Parameters.AddWithValue("@a7", txtSirket.Text);

                    if (radioErkek.Checked == true)
                    {
                        komut.Parameters.AddWithValue("@a8", 1);
                    }
                    else
                    {
                        komut.Parameters.AddWithValue("@a8", 0);
                    }
                    komut.ExecuteNonQuery();

                    MessageBox.Show(txtKullaniciAdi.Text + "  Kullanıcısı Başarıyla Güncellenmiştir.", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    baglanti.Close();
                    Yenile();
                    Temizle();

                }
                baglanti.Close();


                /////////////

               
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }
        bool formTasiniyor = false;
        Point baslangicNoktasi = new Point(0, 0);
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi.X, p.Y - this.baslangicNoktasi.Y);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }
    }
}
