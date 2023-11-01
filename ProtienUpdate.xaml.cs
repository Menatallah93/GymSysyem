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
	/// Interaction logic for ProtienUpdate.xaml
	/// </summary>
	public partial class ProtienUpdate : Window
	{
		public static BuyProducts currentProtien;

		BuyProducts ProtienAfterUpdate = new BuyProducts();



		public delegate void ProtienData(object sender, BuyProducts prot);

		public event ProtienData ProtienDataUpdated;



		Context context = new Context();
		public ProtienUpdate()
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

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			textId.Text = currentProtien.ID.ToString();
			textName.Text = currentProtien.Name;
			txtQuantaty.Text = currentProtien.Quantaty.ToString();
			txtPrice.Text = currentProtien.Price.ToString();
            textSale.Text= currentProtien.SaledPrice.ToString();
			textQRcode.Text = currentProtien.QrCode;
        }

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
            bool flag = true;

            BuyProducts protien = new BuyProducts();
			if (ProtienDataUpdated != null)
			{
				var query = context.BuyProducts.Where(p => p.ID == int.Parse(textId.Text)).Select(p => p);
				foreach (var p in query)
				{
					p.PurchaseDate = DateTime.Now.Date;
					p.Done = false;

					p.Name = textName.Text;
					p.QrCode = textQRcode.Text;

					if (txtQuantaty.Text.Length == 0)
					{
						p.Quantaty = 0;
					}
					else
					{
                        if (int.TryParse(txtQuantaty.Text, out int Quantity))
                        {
                            p.Quantaty += Quantity;
                            ProCount.Visibility = Visibility.Hidden;

                        }
                        else
                        {
                            ProCount.Visibility = Visibility.Visible;
                        }
                    }

					if (txtPrice.Text.Length == 0)
					{
						p.Price = 0;
					}
					else
					{
                        if (int.TryParse(txtPrice.Text, out int Price))
                        {
                            p.Price = Price;
                            ProPrice.Visibility = Visibility.Hidden;

                        }
                        else
                        {
                            ProPrice.Visibility = Visibility.Visible;
                        }
                    }

                    if (int.TryParse(textSale.Text, out int SelledPrice))
                    {
                        p.SaledPrice = SelledPrice;
                        ProSelledPrice.Visibility = Visibility.Hidden;

                    }
                    else
                    {
                        ProSelledPrice.Visibility = Visibility.Visible;
                    }
                    //p.Total = p.Quantaty * p.Price;
                    protien = p;
				}
				if (textName.Text == "" || txtQuantaty.Text == "" || txtPrice.Text == ""
					||ProCount.IsVisible||ProPrice.IsVisible||ProSelledPrice.IsVisible
					)
				{ flag = false; }
			}

			if (flag)
			{
				context.SaveChanges();
				ProtienDataUpdated(this, protien);

				this.Close();
			}
			else
			{
				errorTextblock.Text = "برجاء ادخال جميع البيانات";
			}

		}


        private void textPass_PreviewKeyDownProduct(object sender, KeyEventArgs e)
        {
            string EdAndDelPass = context.Admins.FirstOrDefault(ed => ed.ID == 1).EDitAndDeletPassword;
            if (e.Key == Key.Enter)
            {
                if (textPass.Password == EdAndDelPass)
                {
                    AllProducts.Visibility = Visibility.Visible;
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

