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
using System.Text.RegularExpressions;

namespace Final_Resturant
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Page
    {
        public Customer()
        {
            InitializeComponent();
            disp_data();
            auto();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FS3EMK1\SQL;Initial Catalog=King_Taste_Restaurant;Integrated Security=True");
        SqlCommand cmd;
        public void auto()
        {
            SqlCommand cmd = new SqlCommand(); SqlDataReader srn = null; cmd.Connection = con; cmd.CommandText = "Select top(1) Cus_ID  from Customer order by Cus_ID desc "; con.Open(); srn = cmd.ExecuteReader(); if (srn.Read())
            {
                string str = srn.GetValue(0).ToString(); string digits = new string(str.Where(char.IsDigit).ToArray()); string letters = new string(str.Where(char.IsLetter).ToArray()); int number; if (!int.TryParse(digits, out number)) ;
                string newStr = letters + (++number).ToString("");
                txt_cid.Text = newStr.ToString();
            }
            con.Close();
        }

        private void btn_new_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_cid.Text.Length == 0)
                {
                    error.Text = "* Customer ID cannot be blank";
                    txt_cid.Focus();
                }
                else if (txt_cname.Text.Length == 0)
                {
                    error.Text = "* Customer Name cannot be blank";
                    txt_cname.Focus();
                }
                else if (txt_cname.Text.Any(char.IsDigit))
                {
                    error.Text = "* Customer name cannot include numbers";
                    txt_cname.Focus();
                }
                else if (txt_caddress.Text.Length == 0)
                {
                    error.Text = "* Address  cannot be blank";
                    txt_caddress.Focus();
                }

                else if (txt_ccontact.Text.Length == 0)
                {
                    error.Text = "*Contact cannot be blank";
                    txt_ccontact.Focus();
                }
                else if (!Regex.IsMatch(txt_ccontact.Text, @"^(?:7|0|(?:\+94))[0-9]{8,9}$"))
                {
                    error.Text = "* Please enter a valid Mobile No";
                    txt_ccontact.Focus();
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(txt_ccontact.Text, "[^0-9]"))
                {
                    error.Text = "*Contact cannot include letters";
                    txt_ccontact.Focus();
                }
                else if (txt_caddress.Text.Length == 0)
                {
                    error.Text = "* Address cannot be blank";
                    txt_caddress.Focus();
                }


                else
                {

                    error.Text = "";




                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Customer(Cus_ID,Cus_Name,Address,Contact_N0)values(@id,@n,@a,@c ) ";
                    cmd.Parameters.AddWithValue("@id", txt_cid.Text);
                    cmd.Parameters.AddWithValue("@n", txt_cname.Text);
                    cmd.Parameters.AddWithValue("@a", txt_caddress.Text);
                    cmd.Parameters.AddWithValue("@c", txt_ccontact.Text);


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
                txt_cid.Clear();
                txt_cname.Clear();
                txt_caddress.Clear();
                txt_ccontact.Clear();

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
                cmd.CommandText = "select Cus_ID,Cus_Name,Address,Contact_N0 from Customer";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                DataGridCustomer.ItemsSource = dt.DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Customer where  Cus_ID = '" + txt_cid.Text.ToString() + "'";
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

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Customer set Cus_ID = '" + txt_cid.Text.ToString() + "' ,Cus_Name ='" + txt_cname.Text.ToString() + "',Address ='" + txt_caddress.Text.ToString() + "',Contact_N0='" + txt_ccontact.Text.ToString() + "' where Cus_ID = '" + txt_cid.Text.ToString() + "'  ", con);
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

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            string sql = "select * from Customer    where Cus_ID  = '" + txt_cid.Text + "'   ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())

            {


                string Cus_Name = myreader.GetString(1);

                string Address = myreader.GetString(2);
                string Customer = myreader.GetString(3);




                txt_cname.Text = Cus_Name;

                txt_caddress.Text = Address;
                txt_ccontact.Text = Customer;



            }
            con.Close();
        }
    }
}
