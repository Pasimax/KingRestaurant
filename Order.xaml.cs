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
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Final_Resturant
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        public static string pass;
        public Order()
        {
            InitializeComponent();
            fillcombobox();
            auto();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FS3EMK1\SQL;Initial Catalog=King_Taste_Restaurant;Integrated Security=True");
        public void auto()
        {
            SqlCommand cmd = new SqlCommand(); SqlDataReader srn = null; cmd.Connection = con; cmd.CommandText = "Select top(1) Order_ID  from Orderr order by Order_ID desc "; con.Open(); srn = cmd.ExecuteReader(); if (srn.Read())
            {
                string str = srn.GetValue(0).ToString(); string digits = new string(str.Where(char.IsDigit).ToArray()); string letters = new string(str.Where(char.IsLetter).ToArray()); int number; if (!int.TryParse(digits, out number)) ;
                string newStr = letters + (++number).ToString("");
                txt_oid.Text = newStr.ToString();
            }
            con.Close();
        }
        public void fillcombobox()
        {
            try
            {
                string sql = "select * from Food_Item";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader myreader;


                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string Food_Name = myreader.GetString(1);
                    oname.Items.Add(Food_Name);
                }


                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void clearData()
        {
            try
            {
                txt_oid.Clear();

                txt_oquantity.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void oname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                con.Open();
                string sql = "select * from Food_Item    where Food_Name  = '" + oname.Text + "'   ";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())

                {

                    string Food_ID = myreader.GetString(0);


                    txt_fid.Text = Food_ID;


                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void oname_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "select * from Food_Item    where Food_Name  = '" + oname.Text + "'   ";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())

                {

                    string Food_ID = myreader.GetString(0);
                    int Price = myreader.GetInt32(2);
                    int Discount = myreader.GetInt32(3);


                    txt_fid.Text = Food_ID;
                    txt_oprice.Text = Price.ToString();
                    odiscount.Text = Discount.ToString();



                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void oadd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (oname.Text.Length == 0)
                {
                    error.Text = "* Food name cannot be blank";
                    oname.Focus();
                }

                else if (txt_oquantity.Text.Length == 0)
                {
                    error.Text = "* Quantity cannot be blank";
                    txt_oquantity.Focus();
                }
                else
                {
                    error.Text = "";


                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into  Orderr(Order_ID,Date,qty,Total_Amount,Fd_ID )values(@oid,@d,@q,@t,@fid ) ";
                    cmd.Parameters.AddWithValue("@oid", txt_oid.Text);
                    cmd.Parameters.AddWithValue("@d", txt_odate.Text);
                    cmd.Parameters.AddWithValue("@q", txt_oquantity.Text);
                    cmd.Parameters.AddWithValue("@fid", txt_fid.Text);

                    cmd.Parameters.AddWithValue("@t", txt_total.Text);





                    cmd.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Food added succesfully");



                    con.Open();
                    cmd.CommandText = "select Food_Name,Price,qty ,Total_Amount from Food_Item inner join Orderr on Orderr.Fd_ID = Food_Item.Food_ID where Order_ID = '" + txt_oid.Text + "' ";
                    cmd.Connection = con;
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Food_Item");
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    con.Close();
                    ogrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void total_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32 val1 = Convert.ToInt32(txt_oquantity.Text);
                Int32 val2 = Convert.ToInt32(txt_oprice.Text);
                Int32 val4 = Convert.ToInt32(odiscount.Text);
                // Int32  val5 = val2 - val4;
                Int32 val3 = (val2 - val4) * val1;
                txt_total.Text = val3.ToString();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        SqlCommand cmd;
        MainWindow main = new MainWindow();
        private void ocheckout_Click(object sender, RoutedEventArgs e)
        {
            pass = tot.Text;
           


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            var query = "SELECT SUM (Total_Amount) FROM Orderr where Order_ID = '" + txt_oid.Text + "'";
            using (var cmd = new SqlCommand(query, con))
            {
                tot.Text = cmd.ExecuteScalar().ToString();
                con.Close();
            }
        }
    }
}
