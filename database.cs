using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Final_Resturant
{
   
    class database
    {
        public SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter da;

        public database()
        {
            con = new SqlConnection(@"Data Source=DESKTOP-FS3EMK1\SQL;Initial Catalog=King_Taste_Restaurant;Integrated Security=True");
        }
        public void openConnection()
        {
            con.Open();
        }
        public void closeConnection()
        {
            con.Close();
        }
        public string getValue(string a)
        {
            openConnection();
            cmd = new SqlCommand(a, con);
            string x = Convert.ToString(cmd.ExecuteScalar());
            cmd.Dispose();
            closeConnection();
            return x;
        }
    }
}
