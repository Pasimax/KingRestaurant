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
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Page
    {
        public Employee()
        {
            InitializeComponent();
            disp_data();
            auto();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FS3EMK1\SQL;Initial Catalog=King_Taste_Restaurant;Integrated Security=True");
        SqlCommand cmd;
        public void auto()
        {
            SqlCommand cmd = new SqlCommand(); SqlDataReader srn = null; cmd.Connection = con; cmd.CommandText = "Select top(1) Emp_ID  from Employee order by Emp_ID desc "; con.Open(); srn = cmd.ExecuteReader(); if (srn.Read())
            {
                string str = srn.GetValue(0).ToString(); string digits = new string(str.Where(char.IsDigit).ToArray()); string letters = new string(str.Where(char.IsLetter).ToArray()); int number; if (!int.TryParse(digits, out number)) ;
                string newStr = letters + (++number).ToString("");
                txt_eid.Text = newStr.ToString();
            }
            con.Close();
        }

        private void btn_new_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_eid.Text.Length == 0)
                {
                    error.Text = "* Employee ID cannot be blank";
                    txt_eid.Focus();
                }
                else if (txt_ename.Text.Length == 0)
                {
                    error.Text = "* Employee Name cannot be blank";
                    txt_ename.Focus();
                }
                else if (txt_ename.Text.Any(char.IsDigit))
                {
                    error.Text = "* Employee name cannot include numbers";
                    txt_ename.Focus();
                }

                else if (txt_address.Text.Length == 0)
                {
                    error.Text = "* Address  cannot be blank";
                    txt_address.Focus();
                }

                else if (txt_contact.Text.Length == 0)
                {
                    error.Text = "*Contact cannot be blank";
                    txt_contact.Focus();
                }
                else if (!Regex.IsMatch(txt_contact.Text, @"^(?:7|0|(?:\+94))[0-9]{8,9}$"))
                {
                    error.Text = "* Please enter a valid Mobile No";
                    txt_contact.Focus();
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(txt_contact.Text, "[^0-9]"))
                {
                    error.Text = "*Contact cannot include letters";
                    txt_contact.Focus();
                }
                //else if (!gender.IsChecked && !gender1.IsChecked)
                //{
                   // error.Text = "*Please choose a gender";
                    //gender.Focus();
                    //gender1.Focus();
                //}
               
                else if (cmb_desination.Text.Length == 0)
                {
                    error.Text = "* Designation cannot be blank";

                }
                else if (txt_email.Text.Length == 0)
                {
                    error.Text = "* Email cannot be blank";
                    txt_email.Focus();

                }
                else if (!Regex.IsMatch(txt_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    error.Text = "* Please enter a valid Email Address. Eg:name@gmail.com";
                    txt_email.Focus();
                }

                else
                {


                    error.Text = "";



                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Employee(Emp_ID,Emp_Name,Address,Contact_No,Designation,Gender,email)values(@id,@n,@a,@c,@d,@g,@e ) ";
                    cmd.Parameters.AddWithValue("@id", txt_eid.Text);
                    cmd.Parameters.AddWithValue("@n", txt_ename.Text);
                    cmd.Parameters.AddWithValue("@a", txt_address.Text);
                    cmd.Parameters.AddWithValue("@c", txt_contact.Text);
                    cmd.Parameters.AddWithValue("@d", cmb_desination.Text);
                    if (gender.IsChecked == true)
                        cmd.Parameters.AddWithValue("@g", "Male");
                    else
                        cmd.Parameters.AddWithValue("@g", "Female");

                    cmd.Parameters.AddWithValue("@e", txt_email.Text);

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
                txt_eid.Clear();
                txt_ename.Clear();
                txt_address.Clear();
                txt_contact.Clear();
                txt_email.Clear();

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
                cmd.CommandText = "select Emp_ID,Emp_Name,Address,Contact_No,Gender,Designation from Employee";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                DataGridEmployee.ItemsSource = dt.DefaultView;
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
                cmd.CommandText = "delete from Employee where Emp_ID   = '" + txt_eid.Text.ToString() + "'";
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
            SqlCommand cmd = new SqlCommand("update Employee set Emp_ID= '" + txt_eid.Text.ToString() + "' ,Emp_Name  ='" + txt_ename.Text.ToString() + "',Designation = '" + cmb_desination.Text.ToString() + "',Address ='" + txt_address.Text.ToString() + "',Contact_No='" + txt_contact.Text.ToString() + "' where Emp_ID = '" + txt_eid.Text.ToString() + "'  ", con);
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
            string sql = "select * from Employee    where Emp_ID  = '" + txt_eid.Text + "'   ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())

            {


                string Emp_Name = myreader.GetString(1);

                string Address = myreader.GetString(2);
                string Contact_No = myreader.GetString(3);



                txt_ename.Text = Emp_Name;

                txt_address.Text = Address;
                txt_contact.Text = Contact_No;



            }
            con.Close();
        }
    }
}
