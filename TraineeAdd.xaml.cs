
using GymSysyemWpf.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace GymSysyemWpf
{
    /// <summary>
    /// Interaction logic for TraineeAdd.xaml
    /// </summary>
    public partial class TraineeAdd : Window
    {
        Context context = new Context();


        public delegate void TranieeData(object sender, Trainee obj);

        public event TranieeData traineeDataChanged;
        public TraineeAdd()
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
            Trainee trainee = new Trainee();

            trainee.Name = textName.Text;
            if(int.TryParse(textPhone.Text, out int phone) && textPhone.Text.Length == 11)
            {
                TrPhone.Visibility = Visibility.Hidden;
                 trainee.Phone = textPhone.Text;
            }
            else
            {
                TrPhone.Visibility = Visibility.Visible;
            }
            trainee.StartDate = DateTime.Now;
            if (int.TryParse(textPrice.Text, out int price))
            {
                trainee.Price = price;
                trainee.Paid = trainee.Paid + trainee.Price;
                TrPrice.Visibility = Visibility.Hidden;
                                               
            }
            else
            {
                textPrice.Text = "";
                TrPrice.Visibility = Visibility.Visible;
            }
            if (int.TryParse(textAge.Text, out int Age))
            {
                trainee.Age = Age;
                TrAge.Visibility = Visibility.Hidden;

            }
            else
            {
                textAge.Text = "";
                TrAge.Visibility = Visibility.Visible;

            }

            if (female.IsChecked == true)
            {
                trainee.Gender = Gender.Female;
            }
            else if (male.IsChecked == true)
            {
                trainee.Gender = Gender.Male;
            }

            if (combBoxTrainee.SelectedItem != null)
            {
                Subscription subscribe = combBoxTrainee.SelectedItem as Subscription;
                if (subscribe != null)
                {
                    SubPricLabel.Content = subscribe.Price.ToString();
                }

                trainee.SubscriptionID = subscribe.Id;
                trainee.EndDate = DateTime.Now.AddMonths(subscribe.NumberOfMonths);
                trainee.Seen = true;


            }
            if (textName.Text == "" || textPhone.Text == "" || textPrice.Text == ""
                                   || textAge.Text == "" || (female.IsChecked == false && male.IsChecked == false) ||
                                   TrAge.IsVisible||TrPrice.IsVisible||TrPhone.IsVisible||
                                   combBoxTrainee.SelectedItem == null)
            {

                flag = false;

            }
            if (flag)
            {

                if (traineeDataChanged != null)
                {
                    traineeDataChanged(this, trainee);

                    this.Close();
                }
            }
            else
            {
                errorTextblock.Text = "برجاء ادخال جميع البيانات";
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            combBoxTrainee.SelectedValue = "ID";
            combBoxTrainee.DisplayMemberPath = "Name";
            var subscription = context.Subscriptions.Where(m => m.IsDeleted == false).ToList();
            combBoxTrainee.ItemsSource = subscription;

        }

        private void combBoxTrainee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Subscription subscribe = combBoxTrainee.SelectedItem as Subscription;
            if (subscribe != null)
            {
                SubPricLabel.Content = subscribe.Price.ToString();
            }
        }
    }
}
