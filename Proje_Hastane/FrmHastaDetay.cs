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
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular Where HastaTC =" + hastaTC, bgl.Baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branşları Çekme
            SqlCommand cmd2 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.Baglanti());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.Baglanti().Close();
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
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular Where RandevuBrans='" + cmbBrans.Text + "'", bgl.Baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.hastaTC = lblTc.Text;
            fr.Show();
        }
    }
}
