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
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class Reservation : Page
    {
        public Reservation()
        {
            InitializeComponent();
            disp_data();
            auto();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FS3EMK1\SQL;Initial Catalog=King_Taste_Restaurant;Integrated Security=True");
        SqlCommand cmd;
        public void auto()
        {
            SqlCommand cmd = new SqlCommand(); SqlDataReader srn = null; cmd.Connection = con; cmd.CommandText = "Select top(1) Reservation_ID  from Reservation order by Reservation_ID desc "; con.Open(); srn = cmd.ExecuteReader(); if (srn.Read())
            {
                string str = srn.GetValue(0).ToString(); string digits = new string(str.Where(char.IsDigit).ToArray()); string letters = new string(str.Where(char.IsLetter).ToArray()); int number; if (!int.TryParse(digits, out number)) ;
                string newStr = letters + (++number).ToString("");
                txt_rid.Text = newStr.ToString();
            }
            con.Close();
        }

        private void btn_new_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_rid.Text.Length == 0)
                {
                    error.Text = "* Reservation Id cannot be blank";
                    txt_rid.Focus();
                }
                else if (txt_rcid.Text.Length == 0)
                {
                    error.Text = "* Customer Id cannot be blank";
                    txt_rcid.Focus();
                }
                else if (txt_dateapplied.Text.Length == 0)
                {
                    error.Text = "* Date Applied cannot be blank";
                    error.Focus();
                }
                else if (txt_dateshedule.Text.Length == 0)
                {
                    error.Text = "* Date of Sheduled cannot be blank";
                    error.Focus();
                }
                else if (txt_eventname.Text.Length == 0)
                {
                    error.Text = "* Event Name cannot be blank";
                    txt_eventname.Focus();
                }



                else
                {
                    error.Text = "";
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into  Reservation(Reservation_ID,Event_Name,Date_Applied ,Date_of_scheduled)values(@id,@n,@c,@p) ";
                    cmd.Parameters.AddWithValue("@id", txt_rid.Text);
                    cmd.Parameters.AddWithValue("@b", txt_rcid.Text);
                    cmd.Parameters.AddWithValue("@n", txt_eventname.Text);
                    cmd.Parameters.AddWithValue("@c", txt_dateapplied.Text);
                    cmd.Parameters.AddWithValue("@p", txt_dateshedule.Text);


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
                txt_rid.Clear();
                txt_eventname.Clear();


                txt_rcid.Clear();
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
                cmd.CommandText = "select Reservation_ID,Event_Name,Date_Applied,Date_of_scheduled  from Reservation";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                rgrid.ItemsSource = dt.DefaultView;
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
                cmd.CommandText = "delete Reservation where Reservation_ID ='" + txt_rid.ToString() + "'";
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
            SqlCommand cmd = new SqlCommand("update Reservation set Reservation_ID = '" + txt_rid.Text.ToString() + "' ,Event_Name ='" + txt_eventname.Text.ToString() + "',Date_Applied='" + txt_dateapplied.Text.ToString() + "',Date_of_scheduled='" + txt_dateshedule.Text.ToString() + "' where Reservation_ID = '" + txt_rid.Text.ToString() + "'  ", con);
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
            string sql = "select * from Reservation    where Reservation_ID  = '" + txt_rid.Text + "'   ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())

            {


                string Event_Name = myreader.GetString(1);

                //string Cus_ID = myreader.GetString(2);




                txt_eventname.Text = Event_Name;

                //txt_rcid.Text = Cus_ID;



            }
            con.Close();
        }
    }
}
