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
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void btnKaydol_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Tbl_Hastalar (HastaAd, HastaSoyad, HastaTC, HastaTelefon, HastaSifre, HastaCinsiyet) values(@p1,@p2,@p3,@p4,@p5,@p6)", bgl.Baglanti());
            cmd.Parameters.AddWithValue("@p1",txtAd.Text);
            cmd.Parameters.AddWithValue("@p2",txtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3",mskTc.Text);
            cmd.Parameters.AddWithValue("@p4",mskTelefon.Text);
            cmd.Parameters.AddWithValue("@p5",txtSifre.Text);
            cmd.Parameters.AddWithValue("@p6",cmbCinsiyet.Text);
            cmd.ExecuteNonQuery();
            bgl.Baglanti().Close();
            MessageBox.Show("Kaydınız Geçekleşmiştir, Şifreniz: "+txtSifre.Text,"Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
