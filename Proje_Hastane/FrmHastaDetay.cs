using Microsoft.SqlServer.Server;
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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string hastaTC;
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = hastaTC;

            //Ad Soyad Çekme
            SqlCommand cmd = new SqlCommand("Select HastaAd, HastaSoyad From Tbl_Hastalar Where HastaTC = @p1", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1", hastaTC);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.Baglanti().Close();

            //Randevu Geçmişi
            RandevuGecmisiGetir();

            //Branşları Çekme
            SqlCommand cmd2 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.Baglanti());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.Baglanti().Close();
        }

        private void RandevuGecmisiGetir()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular Where HastaTC =" + hastaTC, bgl.Baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Doktor Çekme
            cmbDoktor.Items.Clear();
            SqlCommand cmd = new SqlCommand("Select DoktorAd, DoktorSoyad From Tbl_Doktorlar Where DoktorBrans = @p1", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.Baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            AktifRandevuGetir();
        }

        private void AktifRandevuGetir()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Tbl_Randevular Where RandevuBrans = '" + cmbBrans.Text + "'" + " and RandevuDoktor = '" + cmbDoktor.Text + "' and RandevuDurum = 0", bgl.Baglanti());
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.hastaTC = lblTc.Text;
            fr.Show();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update Tbl_Randevular Set RandevuDurum = 1, HastaTC = @p1, HastaSikayet = @p2 where RandevuID = @p3", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1", lblTc.Text);
            cmd.Parameters.AddWithValue("@p2", rchSikayet.Text);
            cmd.Parameters.AddWithValue("@p3", txtID.Text);
            cmd.ExecuteNonQuery();
            bgl.Baglanti().Close();
            MessageBox.Show("Randevu Alındı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            RandevuGecmisiGetir();
            AktifRandevuGetir();
            cmbBrans.Text = "Lütfen Brans Seçiniz";
            cmbDoktor.Text = "";
            txtID.Text = "";
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }
    }
}
