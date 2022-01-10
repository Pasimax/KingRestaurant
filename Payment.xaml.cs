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

namespace Final_Resturant
{
    /// <summary>
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Page
    {
        public Payment()
        {
            InitializeComponent();
            disp_data();
            txt_pamount.Text = Order.pass;
            auto();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FS3EMK1\SQL;Initial Catalog=King_Taste_Restaurant;Integrated Security=True");
        public void auto()
        {
            SqlCommand cmd = new SqlCommand(); SqlDataReader srn = null; cmd.Connection = con; cmd.CommandText = "Select top(1) Payment_ID  from Payment order by Payment_ID desc "; con.Open(); srn = cmd.ExecuteReader(); if (srn.Read())
            {
                string str = srn.GetValue(0).ToString(); string digits = new string(str.Where(char.IsDigit).ToArray()); string letters = new string(str.Where(char.IsLetter).ToArray()); int number; if (!int.TryParse(digits, out number)) ;
                string newStr = letters + (++number).ToString("");
                txt_pid.Text = newStr.ToString();
            }
            con.Close();
        }

        private void btn_pnew_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txt_pid.Text.Length == 0)
                {
                    errotype.Text = "* Payment ID cannot be blank";
                    txt_pid.Focus();
                }
                else if (txt_pdate.Text.Length == 0)
                {
                    errotype.Text = "* Payment Date cannot be blank";
                    txt_pdate.Focus();
                }
                else if (txt_ptype.Text.Length == 0)
                {
                    errotype.Text = "* Payment Type cannot be blank";
                    errotype.Focus();
                }
                else if (txt_pamount.Text.Length == 0)
                {
                    errotype.Text = "* Payment Amount cannot be blank";
                    txt_pamount.Focus();
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(txt_pamount.Text, "[^0-9]"))
                {
                    errotype.Text = "*Payment Amount cannot include letters";
                    txt_pamount.Focus();
                }


                else
                {
                    errotype.Text = "";

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into  Payment(Payment_ID,Date,Payment_Amount,Payment_Type)values(@pid,@d,@a,@t ) ";
                    cmd.Parameters.AddWithValue("@pid", txt_pid.Text);
                    cmd.Parameters.AddWithValue("@d", txt_pdate.Text);
                    cmd.Parameters.AddWithValue("@a", txt_pamount.Text);
                    cmd.Parameters.AddWithValue("@t", txt_ptype.Text);

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
            txt_pid.Clear();

            txt_pamount.Clear();


        }
        public void disp_data()

        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Payment_ID,Date,Payment_Amount,Payment_Type  from Payment ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                pgrid.ItemsSource = dt.DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btn_pupdate_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Payment set Payment_ID = '" + txt_pid.Text.ToString() + "' ,Date ='" + txt_pdate.Text.ToString() + "',Payment_Amount = '" + txt_pamount.Text.ToString() + "',Payment_Type ='" + txt_ptype.Text.ToString() + "' where Payment_ID = '" + txt_pid.Text.ToString() + "'  ", con);
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

        private void btn_pdelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Payment where Payment_ID = '" + txt_pid.Text.ToString() + "'";
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

        private void btn_psearch_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            string sql = "select * from Payment    where Payment_ID  = '" + txt_pid.Text + "'   ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())

            {



                int Payment_Amount = myreader.GetInt32(2);




                txt_pamount.Text = Payment_Amount.ToString();
                



            }
            con.Close();

        }

       
    }
}
