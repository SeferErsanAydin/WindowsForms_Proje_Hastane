using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    internal class SqlBaglantisi
    {
        public SqlConnection Baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-F5HD02D\\SQLEXPRESS;Initial Catalog=HastaneProjesi;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
