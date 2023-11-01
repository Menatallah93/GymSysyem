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

namespace GymSysyemWpf
{
	/// <summary>
	/// Interaction logic for ProtienAdd.xaml
	/// </summary>
	public partial class ProtienAdd : Window
	{

		public delegate void ProtienData(object sender, BuyProducts obj);

		public event ProtienData protienhDataChanged;

		public ProtienAdd()
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

		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
            bool flag = true;
            BuyProducts protien = new BuyProducts();

			protien.Name = TextName.Text;

            if (textQuantaty.Text.Length == 0)
            {
                protien.Quantaty = 0;
            }
            else
            {
                if (int.TryParse(textQuantaty.Text, out int Quantity))
                {
                    protien.Quantaty = Quantity;
                    ProCount.Visibility = Visibility.Hidden;

                }
                else
                {
                    ProCount.Visibility = Visibility.Visible;
                }

            }

            if(textPrice.Text.Length == 0)
            {
                protien.Price = 0;
            }
            else
            {
                if (int.TryParse(textPrice.Text, out int Price))
                {
                    protien.Price = Price;
                    ProPrice.Visibility = Visibility.Hidden;

                }
                else
                {
                    ProPrice.Visibility = Visibility.Visible;
                }
            }


            if (int.TryParse(textSale.Text, out int SelledPrice))
            {
                protien.SaledPrice = SelledPrice;
                ProSelledPrice.Visibility = Visibility.Hidden;

            }
            else
            {
                ProSelledPrice.Visibility = Visibility.Visible;
            }
			protien.QrCode = textQrCode.Text;
			protien.PurchaseDate = DateTime.Now;

            if (TextName.Text == "" || textQuantaty.Text == ""||ProSelledPrice.IsVisible||ProPrice.IsVisible
                ||ProCount.IsVisible
                || textPrice.Text == "" || textSale.Text == "" || textQrCode.Text == "")
            {

                flag = false;

            }
            if (flag)
            {
                if (protienhDataChanged != null)
                {
                    protienhDataChanged(this, protien);

                }
                this.Close();
            }
            else
            {
                errorTextblock.Text = "برجاء ادخال جميع البيانات";
            }


            

			if (TextName.Text != "" && textQuantaty.Text != "" && textPrice.Text != "" && textSale.Text != "")
			{
				textQuantaty.Text = "";
				textPrice.Text = "";
				textSale.Text = "";

            }
		}
	}
}
