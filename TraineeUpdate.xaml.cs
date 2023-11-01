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
    /// Interaction logic for TraineeUpdate.xaml
    /// </summary>
    public partial class TraineeUpdate : Window
    {
        public static Trainee currentTrainee;
        //public static string Password { get; set; } = "1234";


        Trainee TraineeAfterUpdate = new Trainee();

        public delegate void TraineeData(object sender, Trainee obj);
        public event TraineeData TaineeDataUpdated;


        Context context = new Context();

        public TraineeUpdate()
        {
            InitializeComponent();
        }
        private bool Increaser = false;
        private bool Decrease = false;

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
            Trainee trainee = new Trainee();
                var currentDate = DateTime.Now.Date;

            if (TaineeDataUpdated != null)
            {
                var query =
                    context.Trainees.FirstOrDefault(t => t.Id == int.Parse(textId.Text) && t.IsDeleted == false);
                query.Name = textName.Text;

                if (int.TryParse(textPhone.Text, out int phone)&&textPhone.Text.Length==11)
                {
                    query.Phone = textPhone.Text;
                    TrPhone.Visibility = Visibility.Hidden;
                }else 
                {
                    TrPhone.Visibility = Visibility.Visible;
                }
                
                if (int.TryParse(textAge.Text, out int Age))
                {
                    query.Age = int.Parse(textAge.Text);
                    TrAge.Visibility = Visibility.Hidden;
                }else
                {
                    TrAge.Visibility = Visibility.Visible;
                }

                


                if (int.Parse(textsession.Text) != query.NumberOfAttendance)
                {
                    query.LastAttendDate = DateTime.Today;
                    TraineeDateAttend traineeDateAttend = new TraineeDateAttend();
                    traineeDateAttend.TraineeID = query.Id;
                    traineeDateAttend.Attend = DateTime.Now;

                    query.Seen = false;

                    context.TraineeDateAttend.Add(traineeDateAttend);
                    context.SaveChanges();
                }
                query.NumberOfAttendance = int.Parse(textsession.Text);

                if (special.IsChecked == true)
                {
                    query.StartDate = DateTime.Now;
                    query.EndDate = DateTime.Now.AddMonths(query.Subscription.NumberOfMonths);
                    query.NumberOfAttendance = 0;
                    query.Price = 0.0;
                    query.Price = 0.0;
                    query.Price = double.Parse(textPrice.Text);
                    query.Paid = query.Price;

                    special.IsChecked = false;
                }
                else
                {
                    if ((query.NumberOfAttendance >= query.Subscription.NumbersOfSessions
                        || currentDate > query.EndDate) || ((query.NumberOfAttendance == 0 &&
                        currentDate.AddDays(2) >= query.StartDate)))
                    {
                        if (combBoxTrainee.SelectedItem != null)
                        {
                            Subscription subscribe = combBoxTrainee.SelectedItem as Subscription;
                            query.SubscriptionID = subscribe.Id;
                        }
                        query.StartDate = DateTime.Now;
                        Subscription subscription = context.Subscriptions.FirstOrDefault(x => x.Id == query.SubscriptionID);
                        query.EndDate = DateTime.Now.AddMonths(subscription.NumberOfMonths);
                    }

                    else
                    {
                        query.NumberOfAttendance = int.Parse(textsession.Text);
                        if (query.Paid != query.Subscription.Price)
                        {
                            if (textPrice.Text == "0")
                            {
                                query.Price = query.Price;
                                query.Paid = double.Parse(TraineePricLabel.Content.ToString());
                            }
                            else
                            {
                                
                                query.Price +=  double.Parse(textPrice.Text);
                                query.Paid = query.Paid + double.Parse(textPrice.Text);
                            }
                        }
                    }

                }

                if (textName.Text == "" || textPhone.Text == ""
                        || textAge.Text == "" || TrAge.IsVisible||TrPhone.IsVisible||TrPrice.IsVisible ||
                         combBoxTrainee.SelectedItem == null)
                    {
                        flag = false;
                    }


                
            }
            if (flag)
            {
                context.SaveChanges();
                TaineeDataUpdated(this, trainee);
                this.Close();

            }
            else
            {
                errorTextblock.Text = "برجاء ادخال جميع البيانات";
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textId.Text = currentTrainee.Id.ToString();
            textName.Text = currentTrainee.Name;
            textPhone.Text = currentTrainee.Phone;
            textAge.Text = currentTrainee.Age.ToString();
            textsession.Text = currentTrainee.NumberOfAttendance.ToString();
            textPrice.Text = "0";
            TraineePricLabel.Content = currentTrainee.Paid.ToString();

            combBoxTrainee.SelectedValue = "ID";
            combBoxTrainee.DisplayMemberPath = "Name";
            var subscription = context.Subscriptions.Where(x => x.IsDeleted == false).ToList();
            combBoxTrainee.ItemsSource = subscription;

            combBoxTrainee.Text = context.Subscriptions.FirstOrDefault(x => x.Id == currentTrainee.SubscriptionID).Name;

        }
        private void combBoxTrainee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Subscription subscribe = combBoxTrainee.SelectedItem as Subscription;
            if (subscribe != null)
            {
                SubPricLabel.Content = subscribe.Price.ToString();
            }
        }

        private void Increase_attandance(object sender, MouseEventArgs e)
        {
            textsession.Text = (int.Parse(textsession.Text) + 1).ToString();
        }
        private void DecreaseAttandance(object sender, MouseEventArgs e)
        {
            if (int.Parse(textsession.Text) > 0)
            {
                textsession.Text = (int.Parse(textsession.Text) - 1).ToString();
            }
        }
        private void textPass_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string EdAndDelPass = context.Admins.FirstOrDefault(ed => ed.ID == 1).EDitAndDeletPassword;
            if (e.Key == Key.Enter)
            {
                if (textPass.Password == EdAndDelPass)
                {
                    AllDataTrinee.Visibility = Visibility.Visible;
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
