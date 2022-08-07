using Common.Dto.Reservations;
using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public APIService APIService { get; set; } = new APIService();

        public Dashboard()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            getDataAboutLastMonth();
            getDataAboutCurrentMonth();
            getNumberOfGuests();
            loadXReservations();
        }
        public async void getDataAboutLastMonth()
        {
            try
            {
                var data = await APIService.Get("Reservation/get-revenue-of-reservations-by-last-month");
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<double>(jsonResult);

                TotalRevenuesByLastMonth.Text += x.ToString() + " KM";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom preuzimanja podataka o prihodima prošlog mjeseca");
            }
        }
        public async void getDataAboutCurrentMonth()
        {
            try
            {
                var data = await APIService.Get("Reservation/get-revenue-of-reservations-by-current-month");
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<double>(jsonResult);

                TotalRevenuesByCurrentMonth.Text += x.ToString() + " KM";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom preuzimanja podataka o prihodima trenutnog mjeseca");
            }
        }  
        public async void getNumberOfGuests()
        {
            try
            {
                var data = await APIService.Get("Reservation/get-number-of-reservations-by-current-month");
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<int>(jsonResult);

                TotalGuestsByCurrentMonth.Text += x.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom preuzimanja podataka o broju gostiju");
            }
        }
        public async void loadXReservations()
        {
            try
            {
                var data = await APIService.Get("Reservation/get-x-reservations-by-company-id/7");
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<ReservationGetDto>>(jsonResult);

                for (int i = 0; i < x.Count; i++)
                {

                    TextBlock textBlock = new TextBlock();
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.FontSize = 20;
                    textBlock.Foreground = Helper.Colors.WhiteColor();
                    textBlock.Text = x[i].Guest.FirstName + " " + x[i].Guest.LastName;
                    textBlock.FontFamily = new FontFamily("Comic Sans MS");

                    Grid.SetRow(textBlock, i);
                    Grid.SetColumn(textBlock, 0);
                    gridData.Children.Add(textBlock);

                    TextBlock textBlock1 = new TextBlock();
                    textBlock1.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock1.VerticalAlignment = VerticalAlignment.Center;
                    textBlock1.FontSize = 20;
                    textBlock1.Foreground = Helper.Colors.WhiteColor();
                    textBlock1.Text = "Od " + x[i].DateFrom.ToString("dd.MM.yyyy");
                    textBlock1.FontFamily = new FontFamily("Comic Sans MS");
                    Grid.SetRow(textBlock1, i);
                    Grid.SetColumn(textBlock1, 1);
                    gridData.Children.Add(textBlock1);

                    TextBlock textBlock2 = new TextBlock();
                    textBlock2.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock2.VerticalAlignment = VerticalAlignment.Center;
                    textBlock2.FontSize = 20;
                    textBlock2.Foreground = Helper.Colors.WhiteColor();
                    textBlock2.Text = "Do " + x[i].DateTo.ToString("dd.MM.yyyy");
                    textBlock2.FontFamily = new FontFamily("Comic Sans MS");
                    Grid.SetRow(textBlock2, i);
                    Grid.SetColumn(textBlock2, 2);
                    gridData.Children.Add(textBlock2);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom preuzimanja podataka o prihodima trenutnog mjeseca");
            }
        }
    }
}
