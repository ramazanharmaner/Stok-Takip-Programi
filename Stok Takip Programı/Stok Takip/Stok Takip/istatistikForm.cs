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
    public partial class istatistikForm : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-DQHBK23;Initial Catalog=StokTakip;Integrated Security=True");
        bool formTasiniyor = false;
        Point baslangicNoktasi = new Point(0, 0);
        public istatistikForm()
        {
            InitializeComponent();
        }

        private void istatistikForm_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("EXEC ToplamKazanc", baglanti);
            SqlDataReader oku = komut1.ExecuteReader();

            if (oku.Read())
            {
                lblToplamKazanc.Text = oku[0].ToString();
            }
            baglanti.Close();
            ///////////////////////////////////////
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("EXEC KalanUrunOrtalamasi", baglanti);
            SqlDataReader oku2 = komut2.ExecuteReader();

            if (oku2.Read())
            {
                lblKalanStok.Text = oku2[0].ToString();
            }
            baglanti.Close();
            ///////////////////////////////////////////
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("EXEC OrtalamaKar", baglanti);
            SqlDataReader oku3 = komut3.ExecuteReader();

            if (oku3.Read())
            {
                lblOrtalamaKar.Text = oku3[0].ToString();
            }
            baglanti.Close();
            ///////////////////////////////////////////
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("EXEC ToplamKategori", baglanti);
            SqlDataReader oku4 = komut4.ExecuteReader();

            if (oku4.Read())
            {
                lblToplamKategori.Text = oku4[0].ToString();
            }
            baglanti.Close();
            ///////////////////////////////////////////
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("EXEC ToplamKullanici", baglanti);
            SqlDataReader oku5 = komut5.ExecuteReader();

            if (oku5.Read())
            {
                lblToplamKullanici.Text = oku5[0].ToString();
            }
            baglanti.Close();
            ///////////////////////////////////////////
            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("EXEC AzalanStok", baglanti);
            SqlDataReader oku6 = komut6.ExecuteReader();

            if (oku6.Read())
            {
                lblAzalanStok.Text = oku6[0].ToString();
            }
            baglanti.Close();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi.X, p.Y - this.baslangicNoktasi.Y);
            }
        }
    }
}
