using GymSysyemWpf.Models;
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
using System.Windows.Shapes;

namespace GymSysyemWpf
{
    /// <summary>
    /// Interaction logic for SubscriptionAdd.xaml
    /// </summary>
    public partial class SubscriptionAdd : Window
    {

        Context context = new Context();


        public delegate void SubscriptionData(object sender, Subscription obj);

        public event SubscriptionData SubscriptionDataChanged;

        public SubscriptionAdd()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            Subscription subscription = new Subscription();

            subscription.Name = textName.Text;
            if(int.TryParse(textSessionNumber.Text, out int NumSession))
                {
                 subscription.NumbersOfSessions = NumSession;
                 SubSession.Visibility = Visibility.Hidden;

                }
            else
            {
                SubSession.Visibility = Visibility.Visible;

            }
            if (double.TryParse(textPrice.Text, out double price))
            {
                SubPrice.Visibility = Visibility.Hidden;
                  subscription.Price = price;

            }
            else
            {
                SubPrice.Visibility = Visibility.Visible;

            }
            if (int.TryParse(monthtext.Text, out int Month))
            {
                SubMonths.Visibility = Visibility.Hidden;
                 subscription.NumberOfMonths = Month;

            }
            else
            {
                SubMonths.Visibility = Visibility.Visible;

            }



            if (special.IsChecked == true)
            {
                subscription.Special = true;
            }
            else
            {
                subscription.Special = false;
            }
                      
            if (textName.Text == "" || monthtext.Text == "" || textSessionNumber.Text == "" || textPrice.Text == ""
                                   ||SubMonths.IsVisible||SubPrice.IsVisible||SubSession.IsVisible
                                   || special.IsChecked == null)
            {
                flag = false;
            }
            if (flag)
            {
                if (SubscriptionDataChanged != null)
                {
                    SubscriptionDataChanged(this, subscription);
                    this.Close();
                }
            }
            else
            {
                errorTextblock.Text = "برجاء ادخال جميع البيانات";
            }

            if (textName.Text != "" && textSessionNumber.Text != "" && textPrice.Text != "" && special.IsChecked != null)
            {
              
                special.IsChecked = false;
            }
        }
    }
}
