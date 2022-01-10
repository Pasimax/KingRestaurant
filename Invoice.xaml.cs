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
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Page
    {
       
        public Invoice()
        {
            InitializeComponent();
            auto();
           
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FS3EMK1\SQL;Initial Catalog=King_Taste_Restaurant;Integrated Security=True");
        public void auto()
        {
            SqlCommand cmd = new SqlCommand(); SqlDataReader srn = null; cmd.Connection = con; cmd.CommandText = "Select top(1) Invoice_No  from Invoice order by Invoice_No desc "; con.Open(); srn = cmd.ExecuteReader(); if (srn.Read())
            {
                string str = srn.GetValue(0).ToString(); string digits = new string(str.Where(char.IsDigit).ToArray()); string letters = new string(str.Where(char.IsLetter).ToArray()); int number; if (!int.TryParse(digits, out number)) ;
                string newStr = letters + (++number).ToString("");
                txt_Iid.Text = newStr.ToString();
            }
            con.Close();
        }
        private bool _isrvinvoiceLoaded;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_isrvinvoiceLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                RestaurantDataSet6 dataset = new RestaurantDataSet6();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.DataTable1;
                this._rv_invoice.LocalReport.DataSources.Add(reportDataSource1);

                this._rv_invoice.LocalReport.ReportPath = "../../Report_invoices.rdlc";
                dataset.EndInit();

                //fill data into WpfApplication4DataSet
                RestaurantDataSet6TableAdapters.DataTable1TableAdapter
                DataTable1TableAdapter = new
                RestaurantDataSet6TableAdapters.DataTable1TableAdapter();

                DataTable1TableAdapter.ClearBeforeFill = true;
                DataTable1TableAdapter.Fill(dataset.DataTable1);
                _rv_invoice.RefreshReport();
                _isrvinvoiceLoaded = true;
            }
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlConnection Con = new SqlConnection(@"Data Source=LAPTOP-3HTKK94M;Initial Catalog=Restaurant_Managment_System;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("select qty,Total_Amount from Orderr where Order_ID = '" + txt_oid.Text + "' ", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
        }
    }
}
