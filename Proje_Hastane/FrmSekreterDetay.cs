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

namespace Proje_Hastane
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string sekreterTC;
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = sekreterTC;

            //Sekreter Ad Soyad
            SqlCommand cmd = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter Where SekreterTC=@p1", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0].ToString();
            }
            bgl.Baglanti().Close();

            //DataGrid1 e Branş Aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * From Tbl_Branslar", bgl.Baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //DataGrid2 ye Doktor Bilgilerini Aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as 'Doktor İsmi', DoktorBrans as 'Branşı' From Tbl_Doktorlar", bgl.Baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşları getirme (randevu paneli)
            SqlCommand cmd2 = new SqlCommand("Select BransAd from Tbl_Branslar", bgl.Baglanti());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0].ToString());
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Randevu Ekleme
            SqlCommand cmd = new SqlCommand("Insert Into Tbl_Randevular (RandevuTarih, RandevuSaat, RandevuBrans, RandevuDoktor) values (@p1, @p2, @p3, @p4)", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1", mskTarih.Text);
            cmd.Parameters.AddWithValue("@p2", mskSaat.Text);
            cmd.Parameters.AddWithValue("@p3", cmbBrans.Text);
            cmd.Parameters.AddWithValue("@p4", cmbDoktor.Text);
            cmd.ExecuteNonQuery();
            bgl.Baglanti().Close();
            MessageBox.Show($" {mskTarih.Text} Tarihli, Saat: {mskSaat.Text}'de {cmbBrans.Text} bölümü Doktoru {cmbDoktor.Text} için Randevu Olusturulmuştur");
            Temizle();

        }
        void Temizle()
        {
            txtID.Text = "";
            mskSaat.Text = "";
            mskTarih.Text = "";
            mskTC.Text = "";
            cmbBrans.Text = "Branş Seçiniz";
            cmbDoktor.Text = "Doktor Seçiniz";
            
        }
        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Doktorları Getirme (randevu paneli)
            cmbDoktor.Items.Clear();
            SqlCommand cmd = new SqlCommand("select DoktorAd, DoktorSoyad from Tbl_Doktorlar where DoktorBrans = @p1", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.Baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@p1)", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1", rchDuyuru.Text);
            cmd.ExecuteNonQuery();
            bgl.Baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu.");
        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frm = new FrmDoktorPaneli();
            frm.Show();
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frm = new FrmBrans();
            frm.Show();
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frm = new FrmRandevuListesi();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyurular = new FrmDuyurular();
            frmDuyurular.Show();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
