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
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-DQHBK23;Initial Catalog=StokTakip;Integrated Security=True");
        public string kullaniciAdi { get; set; }
        public string KullaniciTuru { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int kategori = 0;
            string tip = "";
            if(txtAra.Text != "")
            {
                if(radioStokKodu.Checked == true || radioUrunAdi.Checked == true || checkBox1.Checked == true)
                {
                    if(radioStokKodu.Checked == true)
                    {
                        tip = "StokKodu";
                    }
                    else if(radioUrunAdi.Checked == true)
                    {
                        tip = "StokAdi";
                    }


                    if (checkBox1.Checked == true)
                    {

                        if (comboBox1.Text == "Bilgisayar")
                        {
                            kategori = 1;
                        }
                        else if (comboBox1.Text == "Telefon")
                        {
                            kategori = 2;
                        }
                        else if (comboBox1.Text == "Kitap")
                        {
                            kategori = 3;
                        }
                        else if (comboBox1.Text == "Tablet")
                        {
                            kategori = 4;
                        }

                        baglanti.Open();

                        string islem1 = "Select StokKodu As [Ürün Kodu], StokAdi As [Ürün Adı], KalanMiktar As [Kalan Miktar], SatisFiyati As [Fiyatı],KategoriNo As [Kategori No] From StokTabloSatici where " + tip + "='" + txtAra.Text + "' AND KategoriNo='" + kategori + "'";
                        SqlDataAdapter komut1 = new SqlDataAdapter(islem1, baglanti);
                        DataTable df1 = new DataTable();
                        komut1.Fill(df1);
                        dataGridView1.DataSource = df1;

                        baglanti.Close();

                    }
                    baglanti.Open();

                    string islem2 = "Select StokKodu As [Ürün Kodu], StokAdi As [Ürün Adı], KalanMiktar As [Kalan Miktar], SatisFiyati As [Fiyatı],KategoriNo As [Kategori No] From StokTabloSatici where " + tip + "='" + txtAra.Text + "'";
                    SqlDataAdapter komut2 = new SqlDataAdapter(islem2, baglanti);
                    DataTable df2 = new DataTable();
                    komut2.Fill(df2);
                    dataGridView1.DataSource = df2;

                    baglanti.Close();


                }
                else
                {
                    MessageBox.Show("Lütfen Bir Arama Kriteri Seçiniz !","Stok Takip",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }
        private void Temizle()
        {
            txtStokKod.Text = "";
            txtStokAd.Text = "";
            txtKalanMiktar.Text = "";
            txtKategoriNo.Text = "";
            txtSatisFiyati.Text = "";
            txtAlisFiyati.Text = "";
        }
        private void Yenile()
        {
            if (KullaniciTuru == "Admin")
            {
                button3.Visible = true;
                button2.Visible = true;
                baglanti.Open();
                string Select = "Select s1.StokKodu As [Ürün Kodu], s1.StokAdi As [Ürün Adı], s1.KalanMiktar As [Kalan Miktar], s1.AlisFiyati As [Alış Fiyatı], s1.SatisFiyati As [Fiyatı],s1.KategoriNo As [Kategori No],s2.IslemTarihi As [İşlem Tarihi],s2.Kar From StokTabloSatici s1 inner join SatisTablo s2 on s1.StokKodu = s2.StokKodu";
                SqlDataAdapter da = new SqlDataAdapter(Select, baglanti);
                DataTable df = new DataTable();
                da.Fill(df);
                dataGridView1.DataSource = df;
                baglanti.Close();
            }
            else if (KullaniciTuru == "Satıcı")
            {
                button2.Visible = true;
                baglanti.Open();
                string Select = "Select StokKodu As [Ürün Kodu],StokAdi As [Ürün Adı],KalanMiktar As [Kalan Miktar],AlisFiyati As [Alış Fiyatı],SatisFiyati As Fiyatı,KategoriNo As [Kategori No] From StokTabloSatici";
                SqlDataAdapter da = new SqlDataAdapter(Select, baglanti);
                DataTable df = new DataTable();
                da.Fill(df);
                dataGridView1.DataSource = df;
                baglanti.Close();
            }
            else if (KullaniciTuru == "Müşteri")
            {
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label6.Visible = false;
                txtAlisFiyati.Visible = false;

                baglanti.Open();
                string Select = "Select StokKodu As [Ürün Kodu],StokAdi As [Ürün Adı],KalanMiktar As [Kalan Miktar],SatisFiyati As Fiyatı,KategoriNo As [Kategori No] From StokTabloSatici";
                SqlDataAdapter da = new SqlDataAdapter(Select, baglanti);
                DataTable df = new DataTable();
                da.Fill(df);
                dataGridView1.DataSource = df;
                baglanti.Close();
            }
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            lblKisi.Text = kullaniciAdi;
            lblKisiTuru.Text = KullaniciTuru;

            Yenile();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }
        bool formTasiniyor = false;
        Point baslangicNoktasi = new Point(0, 0);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtStokKod.Text = dataGridView1.Rows[secilen].Cells["Ürün Kodu"].Value.ToString();
            txtStokAd.Text = dataGridView1.Rows[secilen].Cells["Ürün Adı"].Value.ToString();
            txtKalanMiktar.Text = dataGridView1.Rows[secilen].Cells["Kalan Miktar"].Value.ToString();
            txtKategoriNo.Text = dataGridView1.Rows[secilen].Cells["Kategori No"].Value.ToString();
            txtSatisFiyati.Text = dataGridView1.Rows[secilen].Cells["Fiyatı"].Value.ToString();
            if(KullaniciTuru == "Müşteri")
            {

            }else
            {
                txtAlisFiyati.Text = dataGridView1.Rows[secilen].Cells["Fiyatı"].Value.ToString();
            }
            
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if(txtStokKod.Text == "")
            {
                MessageBox.Show("lütfen Önce Listeden Satın Alacağınız Ürünü Seçiniz !", "Stok Takip", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dialog = new DialogResult();
                dialog = MessageBox.Show(txtStokAd.Text + " Ürünü'nü Alacaksınız Onaylıyor Musunuz ?","Stok Takip", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if(dialog == DialogResult.Yes)
                {
                    baglanti.Open();

                    string islem3 = "Update StokTabloSatici Set KalanMiktar = KalanMiktar-1 where StokKodu='"+ txtStokKod.Text+"'";
                    SqlCommand komut3 = new SqlCommand(islem3,baglanti);
                    komut3.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Satın Alma Başarılı !", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    //SaticiTablo ekleme alanı
                    baglanti.Open();
                    SqlCommand ata = new SqlCommand("Select StokKodu From StokTabloSatici where StokAdi = '"+ txtStokAd.Text +"'",baglanti);

                    SqlDataReader oku22 = ata.ExecuteReader();
                    string koddd = "";
                    if(oku22.Read())
                    {
                        koddd = oku22[0].ToString();
                    }
                        
                    baglanti.Close();

                    //arama durumunda ekleme
                    // Alis Fiyatı arama
                    string alisss = "";
                    baglanti.Open();
                    SqlCommand komut10 = new SqlCommand("Select AlisFiyati From StokTabloSatici Where StokKodu = '"+ txtStokKod.Text +"'",baglanti);
                    SqlDataReader book = komut10.ExecuteReader();
                    if (book.Read())
                    {
                        alisss = book[0].ToString();
                    }
                    baglanti.Close();
                    txtAlisFiyati.Text = alisss;
                    //

                    baglanti.Open();
                    string islem6 = "insert into SatisTablo(IslemTarihi,SatisFiyati,Kar,KalanUrun,StokKodu) Values(@a1,@a2,@a3,@a4,@a5)";
                    SqlCommand komut6 = new SqlCommand(islem6,baglanti);
                    komut6.Parameters.AddWithValue("@a1", DateTime.Now);
                    komut6.Parameters.AddWithValue("@a2", txtSatisFiyati.Text);
                    komut6.Parameters.AddWithValue("@a3", Convert.ToDouble(txtSatisFiyati.Text)-Convert.ToDouble(alisss));
                    komut6.Parameters.AddWithValue("@a4", txtKalanMiktar.Text);             
                    komut6.Parameters.AddWithValue("@a5", koddd);
                    komut6.ExecuteNonQuery();
                    baglanti.Close();


                    if(KullaniciTuru == "Satıcı" || KullaniciTuru == "Admin")
                    {
                        baglanti.Open();

                        string islem4 = "Select StokAdi From StokTabloSatici Where KalanMiktar <=5 AND StokKodu = '" + txtStokKod.Text + "'";
                        SqlCommand komut4 = new SqlCommand(islem4, baglanti);
                        SqlDataReader oku = komut4.ExecuteReader();

                        if (oku.Read())
                        {
                            MessageBox.Show(oku[0].ToString() + " Ürününden Stoğunuz 5 in Altına İnmiştir.","Stok Takip",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        baglanti.Close();
                    }
                    

                    Temizle();
                    Yenile();
                }
                else
                {
                    MessageBox.Show("Ürünü Almaktan Vazgeçtiniz. Sağlıklı Günler.","Stok Takip",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if(txtStokAd.Text == "" || txtKalanMiktar.Text == "" || txtKategoriNo.Text == "" || txtSatisFiyati.Text == "" || txtAlisFiyati.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Değerleri Eksiksiz Giriniz !","Stok Takip",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                baglanti.Open();

                string islem5 = "insert into StokTabloSatici(StokAdi,KalanMiktar,KategoriNo,SatisFiyati,AlisFiyati) Values(@a1,@a2,@a3,@a4,@a5)";
                SqlCommand komut5 = new SqlCommand(islem5,baglanti);
                komut5.Parameters.AddWithValue("@a1", txtStokAd.Text);
                komut5.Parameters.AddWithValue("@a2", txtKalanMiktar.Text);
                komut5.Parameters.AddWithValue("@a3", txtKategoriNo.Text);
                komut5.Parameters.AddWithValue("@a4", txtSatisFiyati.Text);
                komut5.Parameters.AddWithValue("@a5", txtAlisFiyati.Text);
                komut5.ExecuteNonQuery();
                
                MessageBox.Show(txtStokAd.Text + "  Stoklara Eklenmiştir.", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);

                baglanti.Close();
                Yenile();
                Temizle();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (txtStokAd.Text == "" || txtKalanMiktar.Text == "" || txtKategoriNo.Text == "" || txtSatisFiyati.Text == "" || txtAlisFiyati.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Değerleri Eksiksiz Giriniz !", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                baglanti.Open();
                string islem7 = "Update StokTabloSatici Set StokAdi = @a1, KalanMiktar = @a2, AlisFiyati = @a3, SatisFiyati = @a4, KategoriNo = @a5 where StokKodu = '"+ txtStokKod.Text +"'";
                SqlCommand komut7 = new SqlCommand(islem7, baglanti);
                komut7.Parameters.AddWithValue("@a1", txtStokAd.Text);
                komut7.Parameters.AddWithValue("@a2", txtKalanMiktar.Text);
                komut7.Parameters.AddWithValue("@a3", txtAlisFiyati.Text);
                komut7.Parameters.AddWithValue("@a4", txtSatisFiyati.Text);
                komut7.Parameters.AddWithValue("@a5", txtKategoriNo.Text);
                komut7.ExecuteNonQuery();

                MessageBox.Show(txtStokAd.Text + "  Ürünü Güncellenmiştir.", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);
                baglanti.Close();

                Yenile();
                Temizle();
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if(txtStokKod.Text =="")
            {
                MessageBox.Show("Lütfen Ürün Kodu Değerini Giriniz !", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                baglanti.Open();

                string islem9 = "Delete From StokTabloSatici where StokKodu = '"+ txtStokKod.Text +"'";
                SqlCommand komut9 = new SqlCommand(islem9,baglanti);
                komut9.ExecuteNonQuery();
                MessageBox.Show(txtStokAd.Text + "  Ürünü Başarıyla Silindi.", "Stok Takip", MessageBoxButtons.OK, MessageBoxIcon.Information);


                baglanti.Close();
                Yenile();
                Temizle();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            istatistikForm frm = new istatistikForm();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KullanicilarForm frm = new KullanicilarForm();
            frm.Show();
        }
    }
}
