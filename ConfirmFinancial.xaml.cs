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
    /// Interaction logic for ConfirmFinancial.xaml
    /// </summary>
    public partial class ConfirmFinancial : Window
    {
        public delegate void ChangeFinancialPass(object sender, object obj);
        public event ChangeFinancialPass ChangePass;


        public Context context = new Context();
        public ConfirmFinancial()
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
            string admin = context.Admins.FirstOrDefault(a => a.ID == 1).FinancialPassword;

            if (textFirstpass.Password == admin && textSecondPass.Password == admin)
            {
                if (ChangePass != null)
                {                    
                    context.SaveChanges();
                    ChangePass(this, new object());
                    this.Close();
                }

            }
            else
            {
                textFirstpass.Password = "";
                textSecondPass.Password = "";
            }
        }
    }
}
