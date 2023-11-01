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
    /// Interaction logic for Bill.xaml
    /// </summary>
    public partial class Bill : Window
    {

        public delegate void BillData(object sender, AddintionalBill obj);
        public event BillData AddintionalBillData;
        
        public Context context = new Context();

        public Bill()
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

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }


        private void btnBill_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            AddintionalBill bill = new AddintionalBill();

            bill.Name = TextName.Text;
            bill.Price = double.Parse(textPrice.Text);



            if (TextName.Text == "" || textPrice.Text == "")
            {

                flag = false;

            }
            if (flag)
            {
                if (AddintionalBillData != null)
                {
                    AddintionalBillData(this, bill);

                }
                this.Close();
            }
            else
            {
                errorTextblock.Text = "برجاء ادخال جميع البيانات";
            }

            if (TextName.Text != ""  && textPrice.Text != "" )
            {
                TextName.Text = "";
                textPrice.Text = "";
            }
        }
    }
}

