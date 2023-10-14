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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        public string docTC;

        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTc.Text = docTC;

            //bransları doldur
            SqlCommand cmd2 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.Baglanti());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.Baglanti().Close();

            SqlCommand cmd = new SqlCommand("select * from Tbl_Doktorlar where DoktorTc = @p1", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1", mskTc.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                cmbBrans.Text = dr[3].ToString();
                txtSifre.Text = dr[5].ToString();
            }
            bgl.Baglanti().Close();
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update Tbl_Doktorlar set DoktorAd = @p1, DoktorSoyad = @p2, DoktorBrans = @p3, DoktorSifre = @p4 where DoktorTc = @p5", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1",txtAd.Text);
            cmd.Parameters.AddWithValue("@p2",txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3",cmbBrans.Text);
            cmd.Parameters.AddWithValue("@p4",txtSifre.Text);
            cmd.Parameters.AddWithValue("@p5",mskTc.Text);
            cmd.ExecuteNonQuery();
            bgl.Baglanti().Close();
            MessageBox.Show("Kayıt Düzenlendi");
            this.Close();
        }
    }
}
