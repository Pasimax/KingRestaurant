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
using System.Net.Mail;
using System.Net;
using System.Data;
using System.Data.SqlClient;

namespace Final_Resturant
{
    /// <summary>
    /// Interaction logic for sms.xaml
    /// </summary>
    public partial class sms : Page
    {
        public sms()
        {
            InitializeComponent();
            fillcombobox();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FS3EMK1\SQL;Initial Catalog=King_Taste_Restaurant;Integrated Security=True");

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("pasindugunawardana01@gmail.com", "5590654323134890123");
                MailMessage msgobj = new MailMessage();
                msgobj.To.Add(txt_email.Text);
                msgobj.From = new MailAddress("pasindugunawardana01@gmail.com");
                msgobj.Body = txt_message.Text;
                client.Send(msgobj);

                MessageBox.Show("Message has been sent successfully");



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmd_mid_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from Employee where Emp_ID = '" + cmd_mid.Text + "' order by Emp_ID ASC ";

                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader myreader;

                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {

                    string Emp_Name = myreader.GetString(1);
                    string email = myreader.GetString(6);
                    txt_mname.Text = Emp_Name;
                    txt_email.Text = email;



                }
                



                con.Close();



            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public void fillcombobox()
        {
            try
            {
                string sql = "select * from Employee  ";


                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader myreader;
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string Emp_ID = myreader.GetString(0);


                    cmd_mid.Items.Add(Emp_ID);

                }

                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void salesdate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            con.Open();
            var query = "select SUM (Payment_Amount) from Payment   where Date  = '" + salesdate.Text + "'  ";
            using (var cmd = new SqlCommand(query, con))
            {

                sales.Text = cmd.ExecuteScalar().ToString();
                con.Close();
            }





            con.Close();
            txt_message.Text = "Dear Mr./Mrs.  " +
                                Environment.NewLine +
                                Environment.NewLine +
                                "Here,is the summary of the sales amount of '" + salesdate.Text + "'" +
                                Environment.NewLine +
                                "sales amount = Rs.   '" + sales.Text + "'" +



                                Environment.NewLine +
                                "Warm regards," +
                                Environment.NewLine +
                                "Nimal.";
        }
    }
}
