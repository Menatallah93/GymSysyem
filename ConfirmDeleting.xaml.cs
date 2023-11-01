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
    /// Interaction logic for ConfirmDeleting.xaml
    /// </summary>
    public partial class ConfirmDeleting : Window
    {

        public delegate void DeleteData(object sender, object obj);
        public event DeleteData confirmDeleteData;
        public static bool ISDel = false;
        public static int deletedId;
        public static string Type;
        public Context context = new Context();

        public ConfirmDeleting()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {

            }
        }
        private void btnLogin_ClickToCheckDelPass(object sender, RoutedEventArgs e)
        {
            string EdAndDelPass = context.Admins.FirstOrDefault(ed=>ed.ID == 1).EDitAndDeletPassword;

            if (textFirstpass.Password == EdAndDelPass)
            {
                ISDel = true;
                if (confirmDeleteData != null)
                {
                    if (Type == "Trainee")
                    {
                        context.Trainees.FirstOrDefault(x => x.Id == deletedId).IsDeleted = true;

                    }
                    else if (Type == "Subscription")
                    {
                        context.Subscriptions.FirstOrDefault(x => x.Id == deletedId).IsDeleted = true;

                    }
                    else if (Type == "BuyProducts")
                    {
                        context.BuyProducts.FirstOrDefault(x => x.ID == deletedId).IsDeleted = true;
                    }
                    

                    context.SaveChanges();
                    confirmDeleteData(this, new object());
                    this.Close();
                }

            }
            else
            {
                textFirstpass.Password = "";
            }
        }
    }
}