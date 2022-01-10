using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Final_Resturant
{
    /// <summary>
    /// Interaction logic for food_item.xaml
    /// </summary>
    public partial class food_item : Page
    {
        public food_item()
        {
            InitializeComponent();
            disp_data();
            auto();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FS3EMK1\SQL;Initial Catalog=King_Taste_Restaurant;Integrated Security=True");
        SqlCommand cmd;
        public void auto()
        {
            SqlCommand cmd = new SqlCommand(); SqlDataReader srn = null; cmd.Connection = con; cmd.CommandText = "Select top(1) Food_ID  from Food_Item order by Food_ID desc "; con.Open(); srn = cmd.ExecuteReader(); if (srn.Read())
            {
                string str = srn.GetValue(0).ToString(); string digits = new string(str.Where(char.IsDigit).ToArray()); string letters = new string(str.Where(char.IsLetter).ToArray()); int number; if (!int.TryParse(digits, out number)) ;
                string newStr = letters + (++number).ToString("");
                txt_fid.Text = newStr.ToString();
            }
            con.Close();
        }
        private void fnew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_fid.Text.Length == 0)
                {
                    error.Text = "* Food ID cannot be blank";
                    txt_fid.Focus();
                }
                else if (txt_fname.Text.Length == 0)
                {
                    error.Text = "* Food Name cannot be blank";
                    txt_fname.Focus();
                }
                else if (txt_fname.Text.Any(char.IsDigit))
                {
                    error.Text = "* Food Name cannot include numbers";
                    txt_fname.Focus();
                }
                else if (cmb_categary.Text.Length == 0)
                {
                    error.Text = "* Categary cannot be blank";
                }
                else if (txt_price.Text.Length == 0)
                {
                    error.Text = "* Price cannot be blank";
                    txt_price.Focus();
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(txt_price.Text, "[^0-9]"))
                {
                    error.Text = "*Price cannot include letters";
                    txt_price.Focus();
                }
                else if (txt_fdiscount.Text.Length == 0)
                {
                    error.Text = "* Discount cannot be blank";
                    txt_fdiscount.Focus();
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(txt_fdiscount.Text, "[^0-9]"))
                {
                    error.Text = "*Discount cannot include letters";
                    txt_fdiscount.Focus();
                }
                else if (txt_fdiscription.Text.Length == 0)
                {
                    error.Text = "* Discription cannot be blank";
                    txt_fdiscription.Focus();
                }
                else
                {
                    error.Text = "";


                    Microsoft.Win32.OpenFileDialog dlg =
        new Microsoft.Win32.OpenFileDialog();
                    dlg.ShowDialog();

                    FileStream fs = new FileStream(dlg.FileName, FileMode.Open,
        FileAccess.Read);

                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, System.Convert.ToInt32(fs.Length));

                    fs.Close();




                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into  Food_Item(Food_ID,Food_Name,Categary,Price,Discount,Description,images)values(@id,@n,@c,@p,@d,@de,@images ) ";
                    cmd.Parameters.AddWithValue("@id", txt_fid.Text);
                    cmd.Parameters.AddWithValue("@n", txt_fname.Text);
                    cmd.Parameters.AddWithValue("@c", cmb_categary.Text);
                    cmd.Parameters.AddWithValue("@p", txt_price.Text);
                    cmd.Parameters.AddWithValue("@d", txt_fdiscount.Text);
                    cmd.Parameters.AddWithValue("@de", txt_fdiscription.Text);
                    cmd.Parameters.AddWithValue("@images", data);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("record insetrted succesfully");
                    disp_data();
                    clearData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }





        }
        public void clearData()
        {
            try
            {
                txt_fid.Clear();
                txt_fname.Clear();
                txt_price.Clear();
                txt_fdiscount.Clear();
                txt_fdiscription.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void disp_data()
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Food_ID,Food_Name,Categary,Price,Description  from Food_Item";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                fDatagrid.ItemsSource = dt.DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_fupdate_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Food_Item set Food_ID = '" + txt_fid.Text.ToString() + "' ,Food_Name ='" + txt_fname.Text.ToString() + "',Categary = '" + cmb_categary.Text.ToString() + "',Price ='" + txt_price.Text.ToString() + "',Discount='" + txt_fdiscount.Text.ToString() + "',Description ='" + txt_fdiscription.Text.ToString() + "' where Food_ID = '" + txt_fid.Text.ToString() + "'  ", con);
            try
            {

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been updated succesfully", "updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                clearData();
                disp_data();


            }
        }

        private void btn_fdelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Food_Item where Food_ID  = '" + txt_fid.Text.ToString() + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                disp_data();
                MessageBox.Show("data deleteded succesfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_fsearch_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            string sql = "select * from Food_Item    where Food_ID  = '" + txt_fid.Text + "'   ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())

            {


                string Food_Name = myreader.GetString(1);

                string Description = myreader.GetString(4);
                int Discount = myreader.GetInt32(3);
                int Price = myreader.GetInt32(2);


                txt_fname.Text = Food_Name;

                txt_fdiscription.Text = Description;
                txt_fdiscount.Text = Discount.ToString();
                txt_price.Text = Price.ToString();


            }
            con.Close();
        }
    }
}
