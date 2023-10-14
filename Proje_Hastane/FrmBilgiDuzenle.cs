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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string hastaTC;

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTc.Text = hastaTC;
            SqlCommand cmd = new SqlCommand("Select * From Tbl_Hastalar Where HastaTC = @p1", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1", mskTc.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                mskTelefon.Text = dr[4].ToString();
                txtSifre.Text = dr[5].ToString();
                cmbCinsiyet.Text = dr[6].ToString();
            }
            bgl.Baglanti().Close();
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Tbl_Hastalar set HastaAd=@p1, HastaSoyad=@p2, HastaTelefon=@p3, HastaSifre=@p4, HastaCinsiyet=@p5 Where HastaTC=@p6", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1", txtAd.Text);
            cmd.Parameters.AddWithValue("@p2", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", mskTelefon.Text);
            cmd.Parameters.AddWithValue("@p4", txtSifre.Text);
            cmd.Parameters.AddWithValue("@p5", cmbCinsiyet.Text);
            cmd.Parameters.AddWithValue("@p6", mskTc.Text);
            cmd.ExecuteNonQuery();
            bgl.Baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
