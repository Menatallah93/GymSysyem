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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace GymSysyemWpf
{
    /// <summary>
    /// Interaction logic for SubscriptionUpdate.xaml
    /// </summary>
    public partial class SubscriptionUpdate : Window
    {

        public static Subscription currentSubscription;

        Subscription SubscripeAfterUpdate = new Subscription();

        public delegate void SubscriptionData(object sender, Subscription obj);
        public event SubscriptionData SubscriptionDataUpdated;

        Context context = new Context();

        public SubscriptionUpdate()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(Object sender, MouseButtonEventArgs e)
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            Subscription subscription = new Subscription();

            if (SubscriptionDataUpdated != null)
            {
                 subscription =
                    context.Subscriptions.Where(t => t.Id == currentSubscription.Id).FirstOrDefault();


                subscription.Name = textName.Text;
                if (int.TryParse(textSessionNumber.Text, out int session))
                {
                    SubSession.Visibility = Visibility.Hidden;
                    subscription.NumbersOfSessions = session;

                }
                else
                {
                    SubSession.Visibility = Visibility.Visible;

                }
                if (int.TryParse(textMonth.Text, out int Month))
                {
                    SubMonth.Visibility = Visibility.Hidden;
                    subscription.NumberOfMonths = Month;

                }
                else
                {
                    SubMonth.Visibility = Visibility.Visible;

                }
                if (int.TryParse(textPrice.Text, out int price))
                {
                    SubPrice.Visibility = Visibility.Hidden;
                    subscription.Price = price;

                }
                else
                {
                    SubPrice.Visibility = Visibility.Visible;

                }


                    if (special.IsChecked == true)
                    {
                    subscription.Special = true;
                    }
                
               
                    if (textName.Text == "" || textSessionNumber.Text == "" || textPrice.Text == "" 
                    ||SubSession.IsVisible||SubPrice.IsVisible||SubMonth.IsVisible
                    )
                    {
                        flag = false;
                    }
                

            }
            if (flag)
            {
                context.SaveChanges();
                SubscriptionDataUpdated(this, subscription);
                this.Close();

            }
            else
            {
                errorTextblock.Text = "برجاء ادخال جميع البيانات";
            }
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (currentSubscription != null)
            {
                textMonth.Text = currentSubscription.NumberOfMonths.ToString();
                textName.Text = currentSubscription.Name;
                textSessionNumber.Text = currentSubscription.NumbersOfSessions.ToString();
                textPrice.Text = currentSubscription.Price.ToString();
                special.IsChecked = currentSubscription.Special;

            }
            else
            {
                this.Close();
            }
        }
        
        private void textPass_PreviewKeyDownSubScription(object sender, KeyEventArgs e)
        {
            string EdAndDelPass = context.Admins.FirstOrDefault(ed => ed.ID == 1).EDitAndDeletPassword;
            if (e.Key == Key.Enter)
            {
                if (textPass.Password == EdAndDelPass)
                {
                    AllSub.Visibility = Visibility.Visible;
                    textPass.Password = "";
                }
                else
                {
                    MessageBox.Show("الرقم الذي ادخلته غير صحيح!", "قم بادخال الرقم السري", MessageBoxButton.OK, MessageBoxImage.Information);
                    MessageBoxResult result = MessageBox.Show("هل تريد اعاده المحاوله", "تأكيد", MessageBoxButton.YesNo, MessageBoxImage.Question);


                    if (result == MessageBoxResult.No)
                    {
                        this.Close();

                    }
                    else
                    {
                        textPass.Password = "";
                    }

                }
            }
        }

    }
}
