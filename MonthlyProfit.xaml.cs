

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
    /// Interaction logic for BuyProteinAdd.xaml
    /// </summary>
    public partial class MonthlyProfit : Window
    {
        Context context;

        public MonthlyProfit()
        {
            InitializeComponent();
            context = new Context();
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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var currentDate = DateTime.Today;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var dailyProfit = context.DailyProfits
                                             .Where(t => t.ProfitDate >= firstDayOfMonth && t.ProfitDate <= lastDayOfMonth)
                                             .Distinct()
                                             .GroupBy(c=>c.ProfitDate)
                                             .ToList();

            var daialyProduct = context.OrderItem
                                        .Where(t => t.orderdateTime >= firstDayOfMonth && t.orderdateTime <= lastDayOfMonth)
                                        .Distinct()
                                        .GroupBy(t=>t.orderdateTime)
                                        .ToList();

            dataGrideProfit.ItemsSource = dailyProfit;
        }
    }
}
