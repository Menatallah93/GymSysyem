
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using QRCoder;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection.PortableExecutable;
using GymSysyemWpf.Models;
using System.Diagnostics;
using Microsoft.Win32;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using static GymSysyemWpf.MonthlyProfit;
using Microsoft.Identity.Client;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using GlobalLowLevelInputHooks;


namespace GymSysyemWpf
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        Context context;

        //private LowLevelKeyboardHook lowLevelKeyboardHook;
        public AdminWindow()
        {
            InitializeComponent();
            context = new Context();

        }

        static public SeriesCollection SeriesCollection { get; set; }
        private bool mEditPtnPressed = false;

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            


            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
            {
                this.WindowState = WindowState.Normal;

                
            }


        }




        // Trainee
        public bool IsOpen = true;
        Bitmap qrCodeBitmap;
        int NumOfTrai;
        double Price;
        SaveFileDialog SaveFile = new SaveFileDialog();
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        private void traniee_Click(object sender, RoutedEventArgs e)
        {
            headerText.Text = "المتدربين";
            traineeGrid.Visibility = Visibility.Visible;
            SubscripeGrid.Visibility = Visibility.Hidden;
            attendanceGrid.Visibility = Visibility.Hidden;
            changePasswordGrid.Visibility = Visibility.Hidden;
            dataGrideBill.Visibility = Visibility.Hidden;
            BillGrid.Visibility = Visibility.Hidden;

            protienGrid.Visibility = Visibility.Hidden;
            BuyprotienGrid.Visibility = Visibility.Hidden;
            deletedTab.Visibility = Visibility.Hidden;

            subdeletedGrid.Visibility = Visibility.Hidden;
            proddeletedGrid.Visibility = Visibility.Hidden;
            traineedeletedGrid.Visibility = Visibility.Hidden;


            DailyTrainee.Visibility = Visibility.Visible;
            texttotal.Visibility = Visibility.Hidden;

            Search.Visibility = Visibility.Visible;
            searsh.Visibility = Visibility.Visible;
            SerchAdd.Visibility = Visibility.Visible;

            btnSub.Visibility = Visibility.Hidden;
            btnSpcail.Visibility = Visibility.Hidden;
            financialGrid.Visibility = Visibility.Hidden;
            TraineeAttendeFeature.Visibility = Visibility.Hidden;


            TAdd.Visibility = Visibility.Visible;
            PAdd.Visibility = Visibility.Hidden;
            bAdd.Visibility = Visibility.Hidden;
            SAdd.Visibility = Visibility.Hidden;
            billAdd.Visibility = Visibility.Hidden;

            if (context.DailyTrainees.FirstOrDefault(e => !e.Done) != null)
            {
                NumOfTrai = context.DailyTrainees.FirstOrDefault(e => !e.Done).NumberOfTrainee;
            }
            else
            {
                NumOfTrai = 0;
            }

            Price = context.Subscriptions.FirstOrDefault(m => m.Name == "يومي").Price;

            NumOfTrainee.Content = NumOfTrai.ToString();
            constDailyText.Content = Price.ToString();

            Profit.Content = (NumOfTrai * Price).ToString();


            dataGrideTrainee.DataContext = null;

            var tran = context.Trainees.Where(x => x.IsDeleted == false).Select(v => new
            {
                Id = v.Id,
                Name = v.Name,
                Phone = v.Phone,
                Age = v.Age,
                Gender = v.Gender,
                Price = v.Paid,
                NumberOfAttendance = v.NumberOfAttendance,
                SubsName = v.Subscription.Name,
                SubscriptionID = v.SubscriptionID,
                IsDeleted = v.IsDeleted,
                startdate = v.StartDate,
                EndDate = v.EndDate,
                order= context.Trainees.ToList().IndexOf(v) + 1
            }).ToList();
            dataGrideTrainee.ItemsSource = tran;
        }
        private void TrEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Search.Text != "")
            {

            }
            TraineeUpdate TrainUpdate = new TraineeUpdate();
            TrainUpdate.TaineeDataUpdated += TrainUpdate_TaineeDataUpdated;
            Trainee trainee = new Trainee();
            mEditPtnPressed = true;
            if (mEditPtnPressed)
            {
                if (dataGrideTrainee.SelectedItem != null)
                {
                    dynamic selectedItem = dataGrideTrainee.SelectedItem;

                    // Now you can access the properties of the anonymous object directly
                    trainee.Name = selectedItem.Name;
                    trainee.Phone = selectedItem.Phone;
                    trainee.NumberOfAttendance = selectedItem.NumberOfAttendance;
                    trainee.Paid = selectedItem.Price;
                    trainee.Price = selectedItem.Price;
                    trainee.Age = selectedItem.Age;
                    trainee.Gender = selectedItem.Gender;
                    trainee.Id = selectedItem.Id;
                    trainee.SubscriptionID = selectedItem.SubscriptionID;
                    //trainee.Subscription.Name = selectedItem.Subscription.Name;

                TraineeUpdate.currentTrainee = trainee;
                TrainUpdate.Show();

                dataGrideTrainee.SelectedItem = null;
                mEditPtnPressed = false;
                }

            }
        }
        private void TrDelete_Click(object sender, RoutedEventArgs e)
        {

            ConfirmDeleting confirm = new ConfirmDeleting();
            confirm.confirmDeleteData += ConfirmDeleting_confirmDeleteData;
            Trainee trainee = new Trainee();
            if (dataGrideTrainee.SelectedItem != null)
            {
                dynamic selectedItem = dataGrideTrainee.SelectedItem;

                var TraineeId = (int)selectedItem.Id;
                ConfirmDeleting.deletedId = TraineeId;
                ConfirmDeleting.Type = "Trainee";
            }
            confirm.Show();


        }
        private void ConfirmDeleting_confirmDeleteData(object sender, object obj)
        {
            dataGrideTrainee.DataContext = null;
            var tran = context.Trainees.Where(x => x.IsDeleted == false).Select(v => new
            {
                Id = v.Id,
                Name = v.Name,
                Phone = v.Phone,
                Age = v.Age,
                Gender = v.Gender,
                Price = v.Paid,
                NumberOfAttendance = v.NumberOfAttendance,
                SubsName = v.Subscription.Name,
                SubscriptionID = v.SubscriptionID,
                IsDeleted = v.IsDeleted,
                startdate = v.StartDate,
                EndDate = v.EndDate,
                order = context.Trainees.ToList().IndexOf(v) + 1
            }).ToList();

            dataGrideTrainee.ItemsSource = tran;
        }
        private void T_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TraineeAdd trainee = new TraineeAdd();
            trainee.traineeDataChanged += Trainee_TraineeDataChanged;
            trainee.Show();
        }
        private void Trainee_TraineeDataChanged(object sender, Trainee obj)
        {
            Trainee trainee = new Trainee();
            trainee = obj;

            Trainee trainLast = context.Trainees.OrderBy(v => v.Id).LastOrDefault();

            if (trainLast == null)
            {
                trainee.DisplayOrderID = 1;
            }
            else
            {
                trainee.DisplayOrderID = trainLast.DisplayOrderID + 1;
            }

            trainee.QrCode = trainee.DisplayOrderID + "---" + trainee.Name;
            context.Trainees.Add(trainee);
            context.SaveChanges();

            string QrCode = trainee.DisplayOrderID + "---" + trainee.Name;
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(QrCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            qrCodeBitmap = qrCode.GetGraphic(20);

            SaveFile.Filter = "PNG|*.png";
            SaveFile.FileName = QrCode;
            if (SaveFile.ShowDialog() == true)
            {
                if (qrCodeBitmap != null)
                    qrCodeBitmap.Save(SaveFile.FileName, ImageFormat.Png);
            }

            GetTrainee(trainee);
            dataGrideTrainee.DataContext = trainee;
        }
        private void GetTrainee(Trainee trainee)
        {
            dataGrideTrainee.DataContext = null;
            var tran = context.Trainees.Where(x => x.IsDeleted == false).Select(v => new
            {
                Id = v.Id,
                Name = v.Name,
                Phone = v.Phone,
                Age = v.Age,
                Gender = v.Gender,
                Price = v.Paid,
                NumberOfAttendance = v.NumberOfAttendance,
                SubsName = v.Subscription.Name,
                SubscriptionID = v.SubscriptionID,
                IsDeleted = v.IsDeleted,
                startdate = v.StartDate,
                EndDate = v.EndDate,
                order = context.Trainees.ToList().IndexOf(v)+1
            }).ToList();

            dataGrideTrainee.ItemsSource = tran;
        }
        private void TrainUpdate_TaineeDataUpdated(object sender, Trainee obj)
        {
            if (Search.Text != "")
            {
                 dataGrideTrainee.DataContext = null;
            var tran = context.Trainees.Where(x => x.IsDeleted == false).Select(v => new
            {
                Id = v.Id,
                Name = v.Name,
                Phone = v.Phone,
                Age = v.Age,
                Gender = v.Gender,
                Price = v.Paid,
                NumberOfAttendance = v.NumberOfAttendance,
                SubsName = v.Subscription.Name,
                SubscriptionID = v.SubscriptionID,
                IsDeleted = v.IsDeleted,
                startdate = v.StartDate,
                EndDate = v.EndDate,
                order = context.Trainees.ToList().IndexOf(v) + 1
            }).ToList();

            dataGrideTrainee.ItemsSource = tran;
            }
            else
            {
                dataGrideTrainee.DataContext = null;
                var tran = context.Trainees.Where(x => x.IsDeleted == false).Select(v => new
                {
                    Id = v.Id,
                    Name = v.Name,
                    Phone = v.Phone,
                    Age = v.Age,
                    Gender = v.Gender,
                    Price = v.Paid,
                    NumberOfAttendance = v.NumberOfAttendance,
                    SubsName = v.Subscription.Name,
                    SubscriptionID = v.SubscriptionID,
                    IsDeleted = v.IsDeleted,
                    startdate = v.StartDate,
                    EndDate = v.EndDate,
                    order = context.Trainees.ToList().IndexOf(v) + 1
                }).ToList();

                dataGrideTrainee.ItemsSource = tran;
            }
        }
        private void searsh_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dataGrideTrainee.DataContext = null;
            var tran = context.Trainees.Where(x => x.IsDeleted == false && (x.QrCode.Contains(Search.Text) || x.Name.Contains(Search.Text) || x.Phone.Contains(Search.Text))).Select(v => new
            {
                Id = v.Id,
                Name = v.Name,
                Phone = v.Phone,
                Age = v.Age,
                Gender = v.Gender,
                Price = v.Price,
                NumberOfAttendance = v.NumberOfAttendance,
                SubsName = v.Subscription.Name,
                SubscriptionID = v.SubscriptionID,
                IsDeleted = v.IsDeleted,
                startdate = v.StartDate,
                EndDate = v.EndDate
            }).ToList();

            dataGrideTrainee.ItemsSource = tran;
        }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrideTrainee.DataContext = null;
            var tran = context.Trainees.Where(x => x.IsDeleted == false && (x.QrCode.Contains(Search.Text) || x.Name.Contains(Search.Text) || x.Phone.Contains(Search.Text))).Select(v => new
            {
                Id = v.Id,
                Name = v.Name,
                Phone = v.Phone,
                Age = v.Age,
                Gender = v.Gender,
                Price = v.Price,
                NumberOfAttendance = v.NumberOfAttendance,
                SubsName = v.Subscription.Name,
                SubscriptionID = v.SubscriptionID,
                IsDeleted = v.IsDeleted,
                startdate = v.StartDate,
                EndDate = v.EndDate
            }).ToList();

            dataGrideTrainee.ItemsSource = tran;

        }
        private void btnAddTrainee_Click(object sender, RoutedEventArgs e)
        {
            ConfirmDeleting confirm = new ConfirmDeleting();
            confirm.confirmDeleteData += Confirm_confirmDeleteData2;
            confirm.Show();

        }
        private void Confirm_confirmDeleteData2(object sender, object obj)
        {
            context = new Context();
            var currentDate = DateTime.Now.Date;


            DailyTrainee dailyTrainee = context.DailyTrainees
                                                .FirstOrDefault(e=>e.Done == false);


            double Price = context.Subscriptions.FirstOrDefault(m => m.Name == "يومي").Price;
            if (dailyTrainee != null)
            {
                dailyTrainee.NumberOfTrainee++;
                int train = dailyTrainee.NumberOfTrainee;
                dailyTrainee.TotalIncome += Price;
                double totalInc = dailyTrainee.TotalIncome;
                NumOfTrainee.Content = (train).ToString();
                Profit.Content = totalInc.ToString();
            }
            else
            {
                DailyTrainee DailyTrainee = new DailyTrainee();


                DailyTrainee.NumberOfTrainee = 1;
                DailyTrainee.TotalIncome = Price;
                DailyTrainee.Date = DateTime.Now;


                NumOfTrainee.Content = "1";
                Profit.Content = Price.ToString();
                context.DailyTrainees.Add(DailyTrainee);
            }

            context.SaveChanges();
        }
        private void dataGrideTrainee_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            dynamic rowData = e.Row.DataContext;

            if (rowData != null)
            {
                var currentDate = DateTime.Now.Date;


                int subId = Convert.ToInt32(rowData.SubscriptionID);
                DateTime EndDate = Convert.ToDateTime(rowData.EndDate);
                int NomOfAtt = Convert.ToInt32(rowData.NumberOfAttendance);
                Subscription sub = context.Subscriptions.FirstOrDefault(s => s.Id == subId);
                if (NomOfAtt >= sub.NumbersOfSessions
                    || currentDate > EndDate)
                {
                    e.Row.Background = System.Windows.Media.Brushes.Black;
                    e.Row.Foreground = System.Windows.Media.Brushes.White;
                }
                else
                {
                    e.Row.Background = System.Windows.Media.Brushes.White;
                }
            }
        }



        // Attendance
        private void attendance_Click(object sender, RoutedEventArgs e)
        {
            headerText.Text = "الحضور";
            traineeGrid.Visibility = Visibility.Hidden;
            SubscripeGrid.Visibility = Visibility.Hidden;
            attendanceGrid.Visibility = Visibility.Visible;
            dataGrideBill.Visibility = Visibility.Hidden;
            BillGrid.Visibility = Visibility.Hidden;

            protienGrid.Visibility = Visibility.Hidden;
            BuyprotienGrid.Visibility = Visibility.Hidden;
            deletedTab.Visibility = Visibility.Hidden;

            subdeletedGrid.Visibility = Visibility.Hidden;
            proddeletedGrid.Visibility = Visibility.Hidden;
            traineedeletedGrid.Visibility = Visibility.Hidden;
            changePasswordGrid.Visibility = Visibility.Hidden;

            DailyTrainee.Visibility = Visibility.Hidden;
            texttotal.Visibility = Visibility.Hidden;

            Search.Visibility = Visibility.Hidden;
            searsh.Visibility = Visibility.Hidden;
            SerchAdd.Visibility = Visibility.Hidden;
            financialGrid.Visibility = Visibility.Hidden;

            btnSub.Visibility = Visibility.Hidden;
            btnSpcail.Visibility = Visibility.Hidden;
            TraineeAttendeFeature.Visibility = Visibility.Visible;

            TAdd.Visibility = Visibility.Hidden;
            PAdd.Visibility = Visibility.Hidden;
            bAdd.Visibility = Visibility.Hidden;
            SAdd.Visibility = Visibility.Hidden;
            billAdd.Visibility = Visibility.Hidden;



            dataGrideAttendance.ItemsSource = null;
            var currentDate = DateTime.Now.Date;
             
            var traineesWithMatchingDate = context.Trainees.Where(t => !t.Seen)
                .Select(v => new
            {
                Id = v.Id,
                Name = v.Name,
                Phone = v.Phone,
                Age = v.Age,
                Gender = v.Gender,
                Price = v.Paid,
                NumberOfAttendance = v.NumberOfAttendance,
                SubsName = v.Subscription.Name,
                SubscriptionID = v.SubscriptionID,
                IsDeleted = v.IsDeleted,
                startdate = v.StartDate,
                EndDate = v.EndDate,
                order = context.Trainees.ToList().IndexOf(v) + 1
                }).ToList();

            dataGrideAttendance.ItemsSource = traineesWithMatchingDate;
        }
        private void ToggleCalendarButtonClick(object sender, RoutedEventArgs e)
        {
            CalendarPopup.IsOpen = !CalendarPopup.IsOpen;
        }
        private void TraineeAttend_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TraineeAttend.SelectedDate.HasValue)
            {

                DateTime selectedDate = TraineeAttend.SelectedDate.Value;
                context = new Context();
                List<Trainee> traineeDateAttends = context.TraineeDateAttend
                                                                    .Where(r => r.Attend.Date == selectedDate.Date)
                                                                    .Include(n=>n.Trainee)
                                                                    .ThenInclude(n=>n.Subscription)
                                                                    .Select(m=>m.Trainee)
                                                                    .Distinct()
                                                                    .ToList();

                var trainees = traineeDateAttends.Select(v => new
                {
                    Id = v.Id,
                    Name = v.Name,
                    Phone = v.Phone,
                    Age = v.Age,
                    Gender = v.Gender,
                    Price = v.Price,
                    NumberOfAttendance = v.NumberOfAttendance,
                    SubsName = v.Subscription.Name,
                    SubscriptionID = v.SubscriptionID,
                    IsDeleted = v.IsDeleted,
                    startdate = v.StartDate,
                    EndDate = v.EndDate,
                    order = traineeDateAttends.IndexOf(v) + 1
                }).ToList();


                dataGrideAttendance.ItemsSource = trainees;


            }
        }
        



        // Subscription
        static bool flag = true;
        private void Subscripe_Click(object sender, RoutedEventArgs e)
        {
            headerText.Text = "الاشتراكات ";
            traineeGrid.Visibility = Visibility.Hidden;
            SubscripeGrid.Visibility = Visibility.Visible;
            attendanceGrid.Visibility = Visibility.Hidden;
            dataGrideBill.Visibility = Visibility.Hidden;
            BillGrid.Visibility = Visibility.Hidden;

            protienGrid.Visibility = Visibility.Hidden;
            BuyprotienGrid.Visibility = Visibility.Hidden;

            subdeletedGrid.Visibility = Visibility.Hidden;
            proddeletedGrid.Visibility = Visibility.Hidden;
            traineedeletedGrid.Visibility = Visibility.Hidden;
            deletedTab.Visibility = Visibility.Hidden;
            changePasswordGrid.Visibility = Visibility.Hidden;


            DailyTrainee.Visibility = Visibility.Hidden;
            texttotal.Visibility = Visibility.Hidden;


            Search.Visibility = Visibility.Hidden;
            searsh.Visibility = Visibility.Hidden;
            SerchAdd.Visibility = Visibility.Hidden;
            financialGrid.Visibility = Visibility.Hidden;
            TraineeAttendeFeature.Visibility = Visibility.Hidden;


            btnSub.Visibility = Visibility.Visible;
            btnSpcail.Visibility = Visibility.Visible;

            TAdd.Visibility = Visibility.Hidden;
            PAdd.Visibility = Visibility.Hidden;
            bAdd.Visibility = Visibility.Hidden;
            SAdd.Visibility = Visibility.Visible;
            billAdd.Visibility = Visibility.Hidden;


            var query = context.Subscriptions.Where(m => m.IsDeleted == false).ToList();
            dataGrideSubscripe.DataContext = query;

        }
        private void S_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SubscriptionAdd Sb = new SubscriptionAdd();
            Sb.SubscriptionDataChanged += Sb_SubscriptionDataChanged;
            Sb.Show();
        }
        private void Sb_SubscriptionDataChanged(object sender, Subscription obj)
        {
            Subscription subscription = new Subscription();
            subscription = obj;


            context.Subscriptions.Add(subscription);
            context.SaveChanges();
            Getsubscription(subscription);
            dataGrideSubscripe.DataContext = subscription;
        }
        private void Getsubscription(Subscription subscription)
        {
            if (flag == false)
            {
                dataGrideSubscripe.ItemsSource = null;
                dataGrideSubscripe.ItemsSource = context.Subscriptions.Where(m => m.IsDeleted == false && m.Special == true).ToList();
            }
            else
            {
                dataGrideSubscripe.ItemsSource = null;
                dataGrideSubscripe.ItemsSource = context.Subscriptions.Where(m => m.IsDeleted == false).ToList();

            }
        }
        private void SbEdit_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionUpdate subscription = new SubscriptionUpdate();
            subscription.SubscriptionDataUpdated += Subscription_SubscriptionDataUpdated;

            mEditPtnPressed = true;
            if (mEditPtnPressed)
            {
                Subscription currentSubscription;
                currentSubscription = dataGrideSubscripe.SelectedItem as Subscription;
                SubscriptionUpdate.currentSubscription = currentSubscription;
                subscription.Show();
                dataGrideSubscripe.SelectedItem = null;

                mEditPtnPressed = false;

            }
        }
        private void Subscription_SubscriptionDataUpdated(object sender, Subscription obj)
        {
            dataGrideSubscripe.ItemsSource = null;
            context = new Context();
            dataGrideSubscripe.ItemsSource = context.Subscriptions.Where(m => m.IsDeleted == false).ToList();


            if (flag == false)
            {
                dataGrideSubscripe.ItemsSource = null;
                dataGrideSubscripe.ItemsSource = context.Subscriptions.Where(m => m.IsDeleted == false && m.Special == true).ToList();
            }
            else
            {
                dataGrideSubscripe.ItemsSource = null;
                dataGrideSubscripe.ItemsSource = context.Subscriptions.Where(m => m.IsDeleted == false).ToList();

            }
        }
        private void SbDelete_Click(object sender, RoutedEventArgs e)
        {
            ConfirmDeleting confirm = new ConfirmDeleting();
            confirm.confirmDeleteData += Confirm_confirmDeleteData;
            if (dataGrideSubscripe.SelectedItem != null)
            {
                dynamic selectedItem = dataGrideSubscripe.SelectedItem;

                var SubId = (int)selectedItem.Id;
                ConfirmDeleting.deletedId = SubId;
                ConfirmDeleting.Type = "Subscription";
            }
            confirm.Show();



        }
        private void Confirm_confirmDeleteData(object sender, object obj)
        {
            if (flag == false)
            {
                dataGrideSubscripe.ItemsSource = null;
                dataGrideSubscripe.ItemsSource = context.Subscriptions.Where(m => m.IsDeleted == false && m.Special == true).ToList();
            }
            else
            {
                dataGrideSubscripe.ItemsSource = null;
                dataGrideSubscripe.ItemsSource = context.Subscriptions.Where(m => m.IsDeleted == false).ToList();

            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            dataGrideSubscripe.ItemsSource = null;
            dataGrideSubscripe.ItemsSource = context.Subscriptions.Where(m => m.Special == true && m.IsDeleted == false).ToList();
        }
        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            flag = true;
            dataGrideSubscripe.ItemsSource = null;
            dataGrideSubscripe.ItemsSource = context.Subscriptions.Where(m => m.IsDeleted == false).ToList();
        }



        // Protien Product
        private void protien_Click(object sender, RoutedEventArgs e)
        {
            headerText.Text = "إضافة منتج";
            traineeGrid.Visibility = Visibility.Hidden;
            SubscripeGrid.Visibility = Visibility.Hidden;
            attendanceGrid.Visibility = Visibility.Hidden;

            protienGrid.Visibility = Visibility.Visible;
            BuyprotienGrid.Visibility = Visibility.Hidden;
            changePasswordGrid.Visibility = Visibility.Hidden;
            dataGrideBill.Visibility = Visibility.Hidden;
            BillGrid.Visibility = Visibility.Hidden;

            subdeletedGrid.Visibility = Visibility.Hidden;
            proddeletedGrid.Visibility = Visibility.Hidden;
            traineedeletedGrid.Visibility = Visibility.Hidden;
            deletedTab.Visibility = Visibility.Hidden;


            DailyTrainee.Visibility = Visibility.Hidden;
            texttotal.Visibility = Visibility.Hidden;

            Search.Visibility = Visibility.Hidden;
            searsh.Visibility = Visibility.Hidden;
            SerchAdd.Visibility = Visibility.Hidden;
            financialGrid.Visibility = Visibility.Hidden;
            TraineeAttendeFeature.Visibility = Visibility.Hidden;


            btnSub.Visibility = Visibility.Hidden;
            btnSpcail.Visibility = Visibility.Hidden;


            TAdd.Visibility = Visibility.Hidden;
            PAdd.Visibility = Visibility.Visible;
            bAdd.Visibility = Visibility.Hidden;
            SAdd.Visibility = Visibility.Hidden;
            billAdd.Visibility = Visibility.Hidden;



            dataGrideProtien.ItemsSource = null;
            var query = context.BuyProducts.Where(x => x.IsDeleted == false).Select(R => R).ToList();
            dataGrideProtien.ItemsSource = query;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProtienUpdate prot = new ProtienUpdate();
            prot.ProtienDataUpdated += Prot_ProtienDataUpdated;

            mEditPtnPressed = true;
            if (mEditPtnPressed)
            {
                BuyProducts currentProtien;
                currentProtien = dataGrideProtien.SelectedItem as BuyProducts;
                ProtienUpdate.currentProtien = currentProtien;
                prot.Show();
                dataGrideProtien.SelectedItem = null;
                //currentProtien = null;
                mEditPtnPressed = false;
                dataGrideProtien.ItemsSource = null;
                dataGrideProtien.ItemsSource = context.BuyProducts.Where(x => x.IsDeleted == false).ToList();
            }

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ConfirmDeleting confirm = new ConfirmDeleting();
            confirm.confirmDeleteData += Confirm_confirmDeleteData1;
            BuyProducts trainee = new BuyProducts();
            if (dataGrideProtien.SelectedItem != null)
            {
                BuyProducts selectedItem = dataGrideProtien.SelectedItem as BuyProducts;

                var ProductId = selectedItem.ID;
                ConfirmDeleting.deletedId = ProductId;
                ConfirmDeleting.Type = "BuyProducts";
            }
            confirm.Show();
        }
        private void Confirm_confirmDeleteData1(object sender, object obj)
        {
            dataGrideProtien.ItemsSource = null;
            dataGrideProtien.ItemsSource = context.BuyProducts.Where(x => x.IsDeleted == false).ToList();
        }
        private void P_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProtienAdd pr = new ProtienAdd();
            pr.protienhDataChanged += pr_protienDataChanged;

            pr.Show();
        }
        private void pr_protienDataChanged(object sender, BuyProducts obj)
        {
            BuyProducts pro = new BuyProducts();
            pro = obj;

            context.BuyProducts.Add(pro);
            context.SaveChanges();

            GetProtien();
        }
        private void GetProtien()
        {
            dataGrideProtien.ItemsSource = null;
            context = new Context();
            dataGrideProtien.ItemsSource = context.BuyProducts.Where(x => x.IsDeleted == false).ToList();

        }
        private void Prot_ProtienDataUpdated(object sender, BuyProducts prot)
        {
            dataGrideProtien.ItemsSource = null;
            context = new Context();
            dataGrideProtien.ItemsSource = context.BuyProducts.Where(x => x.IsDeleted == false).ToList();

        }




        // Selling Protein 
        private void BUYProtein_Click(object sender, RoutedEventArgs e)
        {
            textTotal.Text = context.OrderItem.Sum(o => o.Price).ToString();
            headerText.Text = "المبيعات";
            traineeGrid.Visibility = Visibility.Hidden;
            SubscripeGrid.Visibility = Visibility.Hidden;
            attendanceGrid.Visibility = Visibility.Hidden;
            changePasswordGrid.Visibility = Visibility.Hidden;
            dataGrideBill.Visibility = Visibility.Hidden;
            BillGrid.Visibility = Visibility.Hidden;

            protienGrid.Visibility = Visibility.Hidden;
            BuyprotienGrid.Visibility = Visibility.Visible;

            subdeletedGrid.Visibility = Visibility.Hidden;
            proddeletedGrid.Visibility = Visibility.Hidden;
            traineedeletedGrid.Visibility = Visibility.Hidden;

            deletedTab.Visibility = Visibility.Hidden;
            financialGrid.Visibility = Visibility.Hidden;
            TraineeAttendeFeature.Visibility = Visibility.Hidden;


            texttotal.Visibility = Visibility.Visible;
            DailyTrainee.Visibility = Visibility.Hidden;

            Search.Visibility = Visibility.Hidden;
            searsh.Visibility = Visibility.Hidden;
            SerchAdd.Visibility = Visibility.Hidden;


            btnSub.Visibility = Visibility.Hidden;
            btnSpcail.Visibility = Visibility.Hidden;


            TAdd.Visibility = Visibility.Hidden;
            PAdd.Visibility = Visibility.Hidden;
            bAdd.Visibility = Visibility.Visible;
            SAdd.Visibility = Visibility.Hidden;
            billAdd.Visibility = Visibility.Hidden;



            textTotal.Text = context.OrderItem.Sum(m => m.Price).ToString();
            dataGridBuyProtien.ItemsSource = null;

            dataGridBuyProtien.ItemsSource = context.OrderItem.Where(c=>!c.Done).Select(x => new
            {
                Name = x.BuyProducts.Name,
                SaledQuantaty = x.Quantity,
                Price = x.Price,
                orderTime = x.orderdateTime,
            }).ToList();

            textTotal.Text = context.OrderItem.Where(c => !c.Done).Sum(tp => tp.Price).ToString();

        }
      



        // Deleted
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            LoadTraineeData();
            LoadSubscriptionData();
            LoadProductData();

            headerText.Text = "المحذوفات";
            traineeGrid.Visibility = Visibility.Hidden;
            SubscripeGrid.Visibility = Visibility.Hidden;
            attendanceGrid.Visibility = Visibility.Hidden;
            changePasswordGrid.Visibility = Visibility.Hidden;

            protienGrid.Visibility = Visibility.Hidden;
            BuyprotienGrid.Visibility = Visibility.Hidden;

            deletedTab.Visibility = Visibility.Visible;


            subdeletedGrid.Visibility = Visibility.Visible;
            proddeletedGrid.Visibility = Visibility.Visible;
            traineedeletedGrid.Visibility = Visibility.Visible;
            dataGrideBill.Visibility = Visibility.Hidden;
            BillGrid.Visibility = Visibility.Hidden;


            DailyTrainee.Visibility = Visibility.Hidden;
            texttotal.Visibility = Visibility.Hidden;
            financialGrid.Visibility = Visibility.Hidden;
            TraineeAttendeFeature.Visibility = Visibility.Hidden;


            Search.Visibility = Visibility.Hidden;
            searsh.Visibility = Visibility.Hidden;
            SerchAdd.Visibility = Visibility.Hidden;

            btnSub.Visibility = Visibility.Hidden;
            btnSpcail.Visibility = Visibility.Hidden;

            TAdd.Visibility = Visibility.Hidden;
            PAdd.Visibility = Visibility.Hidden;
            bAdd.Visibility = Visibility.Hidden;
            SAdd.Visibility = Visibility.Hidden;
            billAdd.Visibility = Visibility.Hidden;


        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                TabItem selectedTab = e.AddedItems[0] as TabItem;

                if (selectedTab != null)
                {
                    switch (selectedTab.Header.ToString())
                    {
                        case "المتدربين":
                            LoadTraineeData();
                            break;
                        case "المنتجات":
                            LoadProductData();
                            break;
                        case "الاشتراكات":
                            LoadSubscriptionData();
                            break;
                    }
                }
            }
        }
        private void LoadTraineeData()
        {
            TraineeGrideDeleted.ItemsSource = null;
            TraineeGrideDeleted.ItemsSource =
                  context.Trainees.Where(x => x.IsDeleted == true).Select(v => new
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Phone = v.Phone,
                        Age = v.Age,
                        Gender = v.Gender,
                        Price = v.Price,
                        NumberOfAttendance = v.NumberOfAttendance,
                        SubsName = v.Subscription.Name,
                        SubscriptionID = v.SubscriptionID,
                        IsDeleted = v.IsDeleted,
                        startdate = v.StartDate,
                        EndDate = v.EndDate
                    }).ToList();


        }
        private void LoadProductData()
        {

            ProductGrideDeleted.DataContext = null;
            ProductGrideDeleted.ItemsSource = context.BuyProducts.Where(p => p.IsDeleted == true).ToList();
        }
        private void LoadSubscriptionData()
        {
            SubGrideDeleted.ItemsSource = null;
            SubGrideDeleted.ItemsSource = context.Subscriptions.Where(s => s.IsDeleted).ToList();
            ;
        }
        private void TrReturn_Click(object sender, RoutedEventArgs e)
        {
           dynamic selectedItem = TraineeGrideDeleted.SelectedItem ;
            if (selectedItem != null)
            {
                int TrineeId = Convert.ToInt32(selectedItem.Id);
                Trainee trainee = context.Trainees.FirstOrDefault(b => b.Id == TrineeId);
                trainee.IsDeleted = true;
                context.SaveChanges();
                trainee.IsDeleted = false;
                context.SaveChanges();

                LoadTraineeData();
            }
        }
        private void ProReturn_Click(object sender, RoutedEventArgs e)
        {
            BuyProducts buyProd = new BuyProducts();
            buyProd = ProductGrideDeleted.SelectedItem as BuyProducts;
            buyProd.IsDeleted = true;
            if (buyProd != null)
            {
                BuyProducts b = context.BuyProducts.FirstOrDefault(b => b.ID == buyProd.ID);
                b.IsDeleted = true;
                context.SaveChanges();
                b.IsDeleted = false;
                context.SaveChanges();

                LoadProductData();
            }
        }
        private void SbReturn_Click(object sender, RoutedEventArgs e)
        {
            Subscription Sub = new Subscription();
            Sub = SubGrideDeleted.SelectedItem as Subscription;
            Sub.IsDeleted = true;
            if (Sub != null)
            {
                Subscription b = context.Subscriptions.FirstOrDefault(b => b.Id == Sub.Id);
                b.IsDeleted = true;
                context.SaveChanges();
                b.IsDeleted = false;
                context.SaveChanges();

                LoadSubscriptionData();
            }

        }


        //Bill
        private void bill_Click(object sender, RoutedEventArgs e)
        {
            headerText.Text = "الفواتير";
            traineeGrid.Visibility = Visibility.Hidden;
            SubscripeGrid.Visibility = Visibility.Hidden;
            attendanceGrid.Visibility = Visibility.Hidden;
            dataGrideBill.Visibility = Visibility.Visible;

            protienGrid.Visibility = Visibility.Hidden;
            BuyprotienGrid.Visibility = Visibility.Hidden;
            deletedTab.Visibility = Visibility.Hidden;

            subdeletedGrid.Visibility = Visibility.Hidden;
            proddeletedGrid.Visibility = Visibility.Hidden;
            traineedeletedGrid.Visibility = Visibility.Hidden;
            changePasswordGrid.Visibility = Visibility.Hidden;

            DailyTrainee.Visibility = Visibility.Hidden;
            texttotal.Visibility = Visibility.Hidden;

            Search.Visibility = Visibility.Hidden;
            searsh.Visibility = Visibility.Hidden;
            SerchAdd.Visibility = Visibility.Hidden;
            financialGrid.Visibility = Visibility.Hidden;

            btnSub.Visibility = Visibility.Hidden;
            btnSpcail.Visibility = Visibility.Hidden;
            TraineeAttendeFeature.Visibility = Visibility.Hidden;
            BillGrid.Visibility = Visibility.Visible;

            TAdd.Visibility = Visibility.Hidden;
            PAdd.Visibility = Visibility.Hidden;
            bAdd.Visibility = Visibility.Hidden;
            SAdd.Visibility = Visibility.Hidden;
            billAdd.Visibility = Visibility.Visible;

            var bill = context.AddintionalBills.Where(e => !e.IsDeleted).Select(v => new
            {
                Id = v.Id,
                Name = v.Name,
                Price = v.Price,
                Date = v.Date,
            }).ToList();
            dataGrideBill.ItemsSource = bill;
        }
        private void bill_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Bill bill = new Bill();
            bill.AddintionalBillData += Bill_AddintionalBillData;
            bill.Show();
        }
        private void Bill_AddintionalBillData(object sender, AddintionalBill obj)
        {
            AddintionalBill addintionalBill = new AddintionalBill();
            addintionalBill = obj;

            context.AddintionalBills.Add(addintionalBill);
            context.SaveChanges();

            var bill = context.AddintionalBills.Where(e => !e.IsDeleted).Select(v => new
            {
                Id = v.Id,
                Name = v.Name,
                Price = v.Price,
                Date = v.Date,
            }).ToList();
            dataGrideBill.ItemsSource = bill;
        }




        // Financial
        int totalIncome;
        int totalOut;
        private void financial_Click(object sender, RoutedEventArgs e)
        {
            ConfirmFinancial confirm = new ConfirmFinancial();
            confirm.ChangePass += Confirm_ChangePass; ;
            ConfirmDeleting.Type = "financial";
            confirm.Show();
        }

        private void Confirm_ChangePass(object sender, object obj)
        {
            headerText.Text = "تقرير مالي";
            traineeGrid.Visibility = Visibility.Hidden;
            SubscripeGrid.Visibility = Visibility.Hidden;
            attendanceGrid.Visibility = Visibility.Hidden;

            protienGrid.Visibility = Visibility.Hidden;
            BuyprotienGrid.Visibility = Visibility.Hidden;
            deletedTab.Visibility = Visibility.Hidden;

            TraineeAttendeFeature.Visibility = Visibility.Hidden;
            financialGrid.Visibility = Visibility.Visible;
            DailyTrainee.Visibility = Visibility.Hidden;
            texttotal.Visibility = Visibility.Hidden;
            BillGrid.Visibility = Visibility.Hidden;
            dataGrideBill.Visibility = Visibility.Hidden;

            Search.Visibility = Visibility.Hidden;
            searsh.Visibility = Visibility.Hidden;

            btnSub.Visibility = Visibility.Hidden;
            btnSpcail.Visibility = Visibility.Hidden;

            TAdd.Visibility = Visibility.Hidden;
            PAdd.Visibility = Visibility.Hidden;
            bAdd.Visibility = Visibility.Hidden;
            SAdd.Visibility = Visibility.Hidden;
            billAdd.Visibility = Visibility.Hidden;


            changePasswordGrid.Visibility = Visibility.Hidden;
            BillGrid.Visibility = Visibility.Hidden;



            DailyProfit dailyProfit = context.DailyProfits.FirstOrDefault(e => !e.Done);
                double IncomeFromTraineeDialy = context.Trainees
                                                   .Where(t => !t.Seen)
                                                   .Sum(m => m.Price);

                double IncomeFromDailyTraineeDialy = context.DailyTrainees
                                                            .Where(m => !m.Done)
                                                            .Sum(m => m.TotalIncome);

                double IncomeFromSellingProduct = context.OrderItem
                                                         .Where(m => !m.Done)
                                                         .Sum(m => m.Price);

                double OutComeFromBuyProduct = context.BuyProducts
                                                      .Where(op => !op.Done)
                                                      .Sum(m => m.Price);

                double OutComeFromBills = context.AddintionalBills
                                                 .Where(ob=>!ob.IsOnDay)
                                                 .Sum (m => m.Price);

                double totalTrain = IncomeFromTraineeDialy + IncomeFromDailyTraineeDialy;
                double totalProduct = IncomeFromSellingProduct - OutComeFromBuyProduct;
                double totalBill = OutComeFromBills;

            if (dailyProfit != null)
            {
                dailyProfit.TraineePrice = totalTrain;
                dailyProfit.ProductPrice = totalProduct;
                dailyProfit.BillPrice = totalBill;

                dailyProfit.Price = totalTrain + totalProduct - totalBill;
                context.SaveChanges();

                totalTrainee.Content = totalTrain.ToString();
                totalProd.Content = totalProduct.ToString();
                totalDailyIncome.Content = dailyProfit.Price.ToString();
            }
            else
            {
                totalDailyIncome.Content = 0;

                dailyProfit = new DailyProfit();
                dailyProfit.ProfitDate = DateTime.Today;
                dailyProfit.TraineePrice = totalTrain;
                dailyProfit.ProductPrice = totalProduct;
                dailyProfit.BillPrice += totalBill;
                dailyProfit.Price = totalTrain + totalProduct - totalBill;

                context.DailyProfits.Add(dailyProfit);
                context.SaveChanges();

                totalTrainee.Content = totalTrain.ToString();
                totalProd.Content = totalProduct.ToString();
                totalDailyIncome.Content = dailyProfit.Price.ToString();
            }


            var currentDate = DateTime.Today;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            MonthlyIncome monthlyProfit = context.MonthlyProfits.FirstOrDefault(t => !t.Refresh);
            if (monthlyProfit != null)
            {
                var totalIncome = context.DailyProfits
                                     .Where(t => t.ProfitDate >= firstDayOfMonth && t.ProfitDate <= lastDayOfMonth)
                                     .Sum(m => m.Price);

                monthlyProfit.MonthlyPrice = totalIncome;
                totalMonthlyIncome.Content = totalIncome.ToString();
            }
            else
            {
                totalMonthlyIncome.Content = 0;
                monthlyProfit = new MonthlyIncome();
                monthlyProfit.ProfitMonthDate = currentDate;
                context.MonthlyProfits.Add(monthlyProfit);
            }

            context.SaveChanges();
        }

        private void btnRestartDay_Click(object sender, RoutedEventArgs e)
        {
            context.Trainees.ExecuteUpdate(t=>t.SetProperty(m=>m.Price, 0.0));

            var addintionalBill = context.AddintionalBills.Where(e => !e.IsOnDay).ToList();
            if (addintionalBill != null)
            {
                context.AddintionalBills.Where(e => !e.IsOnDay).ExecuteUpdate(p => p.SetProperty(y => y.IsOnDay, true));
            }

            // DailyTrainee
            DailyTrainee dailyTrainee = context.DailyTrainees.FirstOrDefault(e => !e.Done);
            if (dailyTrainee != null)
            {
                dailyTrainee.Done = true;
                context.SaveChanges();

                NumOfTrainee.Content = "0";
                constDailyText.Content = Price.ToString();

                Profit.Content = (0 * Price).ToString();
            }

            // Trainees Attendance
            foreach (dynamic trainee in dataGrideAttendance.Items)
            {
                int traineeID = Convert.ToInt32(trainee.Id);
                Trainee train = context.Trainees.FirstOrDefault(n => n.Id == traineeID);
                train.Price = 0.0;
                train.Seen = true;
                context.SaveChanges();
            }
            dataGrideAttendance.ItemsSource = null;

            // OrderItem
            context.OrderItem.Where(oi => !oi.Done)
                             .ExecuteUpdate(x => x.SetProperty(y => y.Done, true));

            context.BuyProducts.Where(oi => !oi.Done)
                               .ExecuteUpdate(x => x.SetProperty(y => y.Done, true));
            textTotal.Text = "0";

            // DailyProfit
            DailyProfit dailyProfit = context.DailyProfits
                                             .FirstOrDefault(e => !e.Done);
            if (dailyProfit != null)
            {
                dailyProfit.Done = true;
                context.SaveChanges();

                totalDailyIncome.Content = "0";
            }

            // Daily Product and trainee
            totalProd.Content = "0";
            totalTrainee.Content = "0";
        }
        private void btnRestartMonth_Click(object sender, RoutedEventArgs e)
        {
            var addintionalBill = context.AddintionalBills.Where(e => !e.IsDeleted).ToList();
            if(addintionalBill != null)
            {
                context.AddintionalBills.Where(e => !e.IsDeleted).ExecuteUpdate(p=>p.SetProperty(y => y.IsDeleted, true));
            }

            MonthlyIncome monthlyProfit = context.MonthlyProfits.FirstOrDefault(e => !e.Refresh);
            if (monthlyProfit != null)
            {
                monthlyProfit.Refresh = true;
                context.SaveChanges();

                totalMonthlyIncome.Content = "0";
            }
        }
        private void MonthlyProfit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MonthlyProfit bPAdd = new MonthlyProfit();
            bPAdd.Show();
        }
        private void btnLastSixMonth_Click(object sender, RoutedEventArgs e)
        {
            if(ProfitGrid.Visibility == Visibility.Visible)
            {
                ProfitGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                ProfitGrid.Visibility = Visibility.Visible;
                var currentDate = DateTime.Now.Month; 
                var sixMonthsAgo = DateTime.Now.AddMonths(-6).Month;

                List<object> EachMonthProfit = new List<object>();
                string[] monthName = new string[] { "يناير", "فبراير", "مارس", "ابريل","مايو","يونيو","يوليو","اغسطس","سبتمبر","اكتوبر","نوفمبر","ديسمبر" };
                for (int i = sixMonthsAgo; i <= currentDate; i++)
                {
                    var x = new
                    {
                        Months = monthName[i-1],
                        MonthlyPrice = context.DailyProfits
                            .Where(t => t.ProfitDate.Month == i)
                            .Sum(dp => dp.Price),
                    };

                    EachMonthProfit.Add(x);
                }

                dataGridLastSixMonthesProfit.ItemsSource = EachMonthProfit;

            }


        }




        //ChangePasswords
        public static string CurrentPassword;
        private void changePass_Click(object sender, RoutedEventArgs e)
        {
            headerText.Text = "تغير الارقام السريه";
            traineeGrid.Visibility = Visibility.Hidden;
            SubscripeGrid.Visibility = Visibility.Hidden;
            attendanceGrid.Visibility = Visibility.Hidden;

            protienGrid.Visibility = Visibility.Hidden;
            BuyprotienGrid.Visibility = Visibility.Hidden;
            deletedTab.Visibility = Visibility.Hidden;

            TraineeAttendeFeature.Visibility = Visibility.Hidden;
            financialGrid.Visibility = Visibility.Hidden;

            DailyTrainee.Visibility = Visibility.Hidden;
            texttotal.Visibility = Visibility.Hidden;

            Search.Visibility = Visibility.Hidden;
            searsh.Visibility = Visibility.Hidden;

            btnSub.Visibility = Visibility.Hidden;
            btnSpcail.Visibility = Visibility.Hidden;

            TAdd.Visibility = Visibility.Hidden;
            PAdd.Visibility = Visibility.Hidden;
            bAdd.Visibility = Visibility.Hidden;
            SAdd.Visibility = Visibility.Hidden;
            changePasswordGrid.Visibility = Visibility.Visible;

        }
        private void btnUpdataPass_ClickToChangeDelPass(object sender, RoutedEventArgs e)
        {
            string oldPassword = textOldPass.Password;
            string newPassword = textNewPass.Password;
            string confirmPassword = textConfirmPass.Password;
            CurrentPassword = oldPassword;

            // Perform validation checks
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password fields do not match.");
                return;
            }

            var user = context.Admins.FirstOrDefault(x => x.Password == CurrentPassword);

            if (user == null)
            {
                MessageBox.Show("هذا المستخدم غير موجود");
                return;
            }


            else if (oldPassword != user.Password)
            {
                MessageBox.Show("الرقم الذي ادخلته غير صحيح.");
                return;
            }

            else
            {
                user.Password = newPassword;
            };


            context.Admins.Update(user);
            context.SaveChanges();
            MessageBox.Show("تم تتغير الرقم السري بنجاح.");

            if(textOldPass.Password != "" && textNewPass.Password != "" && textConfirmPass.Password != "")
            {
                textOldPass.Password = "";
                textNewPass.Password = "";
                textConfirmPass.Password = "";
            }

        }
        private void btnUpdataPass_ClickToChangePassForDeleting(object sender, RoutedEventArgs e)
        {
            string oldPassword = textOldPassForDeleting.Password;
            string newPassword = textNewPassForDeleting.Password;
            string confirmPassword = textConfirmPassForDeleting.Password;
            CurrentPassword = oldPassword;

            // Perform validation checks
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password fields do not match.");
                return;
            }

            var user = context.Admins.FirstOrDefault(x => x.EDitAndDeletPassword == CurrentPassword);
            if (user == null)
            {
                MessageBox.Show("هذا المستخدم غير موجود");
                return;
            }

            else if (oldPassword != user.EDitAndDeletPassword)
            {
                MessageBox.Show("الرقم الذي ادخلته غير صحيح.");
                return;
            }
            else
            {
                user.EDitAndDeletPassword = newPassword;
            };

            context.Admins.Update(user);
            context.SaveChanges();
            MessageBox.Show("تم تتغير الرقم السري بنجاح.");
            if (textOldPassForDeleting.Password != "" && textNewPassForDeleting.Password != "" && textConfirmPassForDeleting.Password != "")
            {
                textOldPassForDeleting.Password = "";
                textNewPassForDeleting.Password = "";
                textConfirmPassForDeleting.Password = "";
            }
        }
        private void btnUpdataPass_ClickToChangePassForFinancal(object sender, RoutedEventArgs e)
        {
            string oldPassword = textOldPassForFinancial.Password;
            string newPassword = textNewPassForFinancial.Password;
            string confirmPassword = textConfirmPassForFinancial.Password;
            CurrentPassword = oldPassword;

            // Perform validation checks
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password fields do not match.");
                return;
            }

            var user = context.Admins.FirstOrDefault(x => x.FinancialPassword == CurrentPassword);

            if (user == null)
            {
                MessageBox.Show("هذا المستخدم غير موجود");
                return;
            }

            else if (oldPassword != user.FinancialPassword)
            {
                MessageBox.Show("الرقم الذي ادخلته غير صحيح");
                return;
            }
            else
            {
                user.FinancialPassword = newPassword;
            };

            context.Admins.Update(user);
            context.SaveChanges();
            MessageBox.Show("تم تتغير الرقم السري بنجاج");
            if (textOldPassForFinancial.Password != "" && textNewPassForFinancial.Password != "" && textConfirmPassForFinancial.Password != "")
            {
                textOldPassForFinancial.Password = "";
                textNewPassForFinancial.Password = "";
                textConfirmPassForFinancial.Password = "";
            }
        }
       



        //logout
        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }


        StringBuilder specialCharacterBuffer = new StringBuilder();
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
            {
                char character = ConvertKeyToCharacter(e.Key);
                if (character != '\0')
                {
                    specialCharacterBuffer.Append(character);
                }
            }
            else
            {
                string specialCharacterData = specialCharacterBuffer.ToString();


                Trainee trainee = context.Trainees.FirstOrDefault(e => e.QrCode == specialCharacterData);
                if (trainee != null)
                {
                    trainee.NumberOfAttendance++;
                    trainee.LastAttendDate = DateTime.Today;
                    trainee.Seen = false;
                    context.SaveChanges();

                    dataGrideTrainee.DataContext = null;

                    var tran = context.Trainees.Where(x => x.IsDeleted == false).Select(v => new
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Phone = v.Phone,
                        Age = v.Age,
                        Gender = v.Gender,
                        Price = v.Paid,
                        NumberOfAttendance = v.NumberOfAttendance,
                        SubsName = v.Subscription.Name,
                        SubscriptionID = v.SubscriptionID,
                        IsDeleted = v.IsDeleted,
                        startdate = v.StartDate,
                        EndDate = v.EndDate,
                        order = context.Trainees.ToList().IndexOf(v) + 1
                    }).ToList();
                    dataGrideTrainee.ItemsSource = tran;
                }
                else
                {
                    ProcessInput(specialCharacterData);
                }
                
                specialCharacterBuffer.Clear();
            }
        }
        private char ConvertKeyToCharacter(Key key)
        {
            Dictionary<Key, char> specialCharacters = new Dictionary<Key, char>
                {
                    { Key.OemTilde, '~' },
                    { Key.OemPlus, '+' },
                    { Key.OemMinus, '-' },
                    { Key.OemComma, ',' },
                    { Key.OemPeriod, '.' },
                    { Key.OemQuestion, '?' },
                
                    { Key.OemQuotes, '"' },
                    { Key.OemOpenBrackets, '{' },
                    { Key.OemCloseBrackets, '}' },
                    { Key.OemSemicolon, ':' },
                     { Key.D0, '0' },
                    { Key.D1, '1' },
                    { Key.D2, '2' },
                    { Key.D3, '3' },
                    { Key.D4, '4' },
                    { Key.D5, '5' },
                    { Key.D6, '6' },
                    { Key.D7, '7' },
                    { Key.D8, '8' },
                    { Key.D9, '9' },
                };
            if (specialCharacters.ContainsKey(key))
            {
                return specialCharacters[key];
            }

            KeyConverter keyConverter = new KeyConverter();
            string keyString = keyConverter.ConvertToString(key);

            if (!string.IsNullOrEmpty(keyString) && keyString.Length == 1)
            {
                return keyString[0];
            }
            return '\0';
        }

        private void ProcessInput(string data)
        {
            BuyProducts buyProducts = context.BuyProducts.FirstOrDefault(bp=>bp.QrCode == data);

            OrderItems orderItem = context.OrderItem.FirstOrDefault(ot=>ot.BuyProducts.QrCode == data && !ot.Done);
            if (orderItem != null)
            {
                if(buyProducts.Quantaty == 0)
                {
                    MessageBox.Show("نفذت الكمية ");
                }
                else
                {
                    orderItem.Quantity += 1;
                    buyProducts.Quantaty--;
                    orderItem.Price += buyProducts.SaledPrice;
                }
            }
            else 
            {
                if(buyProducts != null)
                {
                    OrderItems orderItems = new OrderItems();
                    buyProducts.Quantaty--;
                    orderItems.Quantity = 1;
                    orderItems.ProductId = buyProducts.ID;
                    orderItems.Price = buyProducts.SaledPrice;
                    orderItems.orderdateTime = DateTime.Now.Date;
                    context.OrderItem.Add(orderItems);

                }


            }
            context.SaveChanges();

            dataGridBuyProtien.ItemsSource = null;
            context = new Context();
            dataGridBuyProtien.ItemsSource = context.OrderItem.Where(c=>!c.Done)
                .Select(x=> new
            {
                Name = x.BuyProducts.Name,
                SaledQuantaty = x.Quantity,
                Price = x.Price,
                orderTime= x.orderdateTime,
            }).ToList();
            textTotal.Text = context.OrderItem.Where(c => !c.Done).Sum(tp => tp.Price).ToString();
        }


    }
}
