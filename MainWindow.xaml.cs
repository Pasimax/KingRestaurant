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

namespace Final_Resturant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        public static string pass;
        public MainWindow()
        {
            InitializeComponent();

            //Login page2Obj = new Login(); //Create object of Page2
            //((MainWindow)Application.Current.MainWindow).frame1.Content = new Login();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // food_item page2Obj = new food_item(); //Create object of Page2
            //((MainWindow)Application.Current.MainWindow).frame1.Content = new food_item();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            frame1.Content = new Customer();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            frame1.Content = new Employee();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            frame1.Content = new Reservation();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
           frame1.Content = new sms();

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            frame1.Content = new food_item();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
           frame1.Content = new Order();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
           frame1.Content = new Payment();
           
        }

        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
           frame1.Content = new Reports();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
           frame1.Content = new Invoice();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }
    }
}
