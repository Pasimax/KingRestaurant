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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        public Reports()
        {
            InitializeComponent();

            _rv_customer.Load += rv_customer_Load;
            _rv_employee.Load += rv_employee_Load;
            _rv_fditem.Load += rv_fditem_Load;
           
            _rv_sales.Load += rv_sales_Load;
        }
        private bool _isrvsalesLoaded;
        private bool _isrvcustomerLoaded;
        private bool _isrvemployeeLoaded;
        private bool _isrvfditemLoaded;
       

        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TabablzControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
       
        
        private void rv_reservation_Load(object sender, EventArgs e)
        {
           
        }
        private void rv_fditem_Load(object sender, EventArgs e)
        {
            if (!_isrvfditemLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                RestaurantDataSet2 dataset = new RestaurantDataSet2();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.Food_Item;
                this._rv_fditem.LocalReport.DataSources.Add(reportDataSource1);

                this._rv_fditem.LocalReport.ReportPath = "../../Report_fditem.rdlc";
                dataset.EndInit();

                //fill data into WpfApplication4DataSet
                RestaurantDataSet2TableAdapters.Food_ItemTableAdapter
                FoodItemTableAdapter = new
                RestaurantDataSet2TableAdapters.Food_ItemTableAdapter();

                FoodItemTableAdapter.ClearBeforeFill = true;
                FoodItemTableAdapter.Fill(dataset.Food_Item);
                _rv_fditem.RefreshReport();
                _isrvfditemLoaded = true;
            }
        }

        private void rv_employee_Load(object sender, EventArgs e)
        {
            if (!_isrvemployeeLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                RestaurantDataSet3 dataset = new RestaurantDataSet3();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.Employee;
                this._rv_employee.LocalReport.DataSources.Add(reportDataSource1);

                this._rv_employee.LocalReport.ReportPath = "../../Report_employee.rdlc";
                dataset.EndInit();

                //fill data into WpfApplication4DataSet
                RestaurantDataSet3TableAdapters.EmployeeTableAdapter
                EmployeeTableAdapter = new
                RestaurantDataSet3TableAdapters.EmployeeTableAdapter();

                EmployeeTableAdapter.ClearBeforeFill = true;
                EmployeeTableAdapter.Fill(dataset.Employee);
                _rv_employee.RefreshReport();
                _isrvemployeeLoaded = true;
            }
        }
        private void rv_customer_Load(object sender, EventArgs e)
        {
            if (!_isrvcustomerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                RestaurantDataSet1 dataset = new RestaurantDataSet1();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.Customer;
                this._rv_customer.LocalReport.DataSources.Add(reportDataSource1);

                this._rv_customer.LocalReport.ReportPath = "../../Report_customer.rdlc";
                dataset.EndInit();

                //fill data into WpfApplication4DataSet
                RestaurantDataSet1TableAdapters.CustomerTableAdapter
                CustomerTableAdapter = new
                RestaurantDataSet1TableAdapters.CustomerTableAdapter();

                CustomerTableAdapter.ClearBeforeFill = true;
                CustomerTableAdapter.Fill(dataset.Customer);
                _rv_customer.RefreshReport();
                _isrvcustomerLoaded = true;
            }
        }
        private void rv_sales_Load(object sender, EventArgs e)
        {
            if (!_isrvsalesLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                RestaurantDataSet4 dataset = new RestaurantDataSet4();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.DataTable1;
                this._rv_sales.LocalReport.DataSources.Add(reportDataSource1);

                this._rv_sales.LocalReport.ReportPath = "../../Report_sales.rdlc";
                dataset.EndInit();

                //fill data into WpfApplication4DataSet
                RestaurantDataSet4TableAdapters.DataTable1TableAdapter
                DataTable1TableAdapter = new
                RestaurantDataSet4TableAdapters.DataTable1TableAdapter();

                DataTable1TableAdapter.ClearBeforeFill = true;
                DataTable1TableAdapter.Fill(dataset.DataTable1);
                _rv_sales.RefreshReport();
                _isrvsalesLoaded = true;
            }
        }
    }
}
