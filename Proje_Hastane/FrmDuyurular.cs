﻿using System;
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
    public partial class FrmDuyurular : Form
    {
        public FrmDuyurular()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmDuyurular_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Tbl_Duyurular", bgl.Baglanti());
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
