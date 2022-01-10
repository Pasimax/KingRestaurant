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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        public Login()
        {
            InitializeComponent();
            
        }
        database da = new database();

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    txt_error.Text = "* Please enter a username";
                }


                else if (string.IsNullOrEmpty(txtPassword.Password))
                {
                    txt_error.Text = "* Please enter password";
                }

                else
                {
                    int count = Convert.ToInt32(da.getValue("select count(*) from Login where User_name='" + txtUsername.Text + "' and Password = '" + txtPassword.Password + "' "));
                    if (count > 0)
                    {
                        // MainWindow manager = new MainWindow();


                        //manager.Show();
                        MainWindow dashboard = new MainWindow();
                        this.Close();
                        dashboard.Show();


                    }
                    else
                        txt_error.Text = "* Incorrect username or password";


                }
                

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally
            {
                da.closeConnection();
            }
        }
        
    }
}
