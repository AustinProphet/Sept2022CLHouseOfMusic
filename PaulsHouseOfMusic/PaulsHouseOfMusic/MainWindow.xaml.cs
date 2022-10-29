﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PaulsHouseOfMusic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BuildInventory();
            TotalCart();
            checkoutMessage.Visibility = Visibility.Hidden;
        }

        public void BuildInventory()
        {
            string file = "musicalinstruments.json";
            if (File.Exists(file))
            {
                string json = File.ReadAllText(file);
                var inventory = JsonConvert.DeserializeObject<List<MusicEquipment>>(json);

                foreach (var item in inventory)
                {
                    forSale.Items.Add($"{item.Name}............${item.Price}");
                }
            }
            else
            {
                forSale.Items.Add("No Items In Inventory!");
            }

        }

        private void TotalCart()
        {
            double total = 0;
            total += inCart.Items.OfType<MusicEquipment>().Sum(item => item.Price);
            grandTotal.Text = $"${total}";
            
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {

            var selected = forSale.SelectedItem;
            if (selected is null) return;
            forSale.Items.Remove(selected);
            inCart.Items.Add(selected);

        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            var selected = inCart.SelectedItem;
            if (selected is null) return;
            inCart.Items.Remove(selected);
            forSale.Items.Add(selected);

        }

        private async void Checkout_Click(object sender, RoutedEventArgs e)
        {
            inCart.Items.Clear();
            forSale.Items.Clear();
            checkoutMessage.Visibility = Visibility.Visible;
            checkoutMessage.Text = "Thank you for shopping at Paul's House Of Music \n Your purchase has been completed and your receipt has been emailed to \n User@user.com";
            await Task.Delay(3000);
            grandTotal.Text = " ";
            checkoutMessage.Visibility = Visibility.Hidden;
            BuildInventory();
        }



        //private void CalculateTotal()
        //{
        //    //List<double> total =



        //}



        //private void forSale_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int i = 0;
        //    while (forSale.SelectedItem != salePrice.Items[i])
        //        i++;
        //    salePrice.SelectedIndex = i;
        //}
    }
}
