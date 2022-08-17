using Common.Dto.Guests;
using Core;
using Core.SearchObjects;
using Flurl.Http;
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
using System.Windows.Shapes;
using WPF.Dtos;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Guests.xaml
    /// </summary>
    public partial class Guests : UserControl
    {
        public MainWindow MainWindow { get; set; }
        public APIService APIService { get; set; } = new APIService();
        public Guests(MainWindow mainWindow)
        {
            this.MainWindow = mainWindow;
            InitializeComponent();
            initialLoad();

        }

        private void initialLoad()
        {
            loadGuests();
        }
        private async void loadGuests(string search="")
        {
            try
            {
                BaseSearchObject baseSearchObject = new BaseSearchObject { ByName = search, Id = (Guid)APIService.CompanyId };

                var data = await APIService.Post<Message>("Guest/get-guest-by-company-id",baseSearchObject);
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<GuestGetDto>>(jsonResult);
                gridData.ItemsSource = x;

            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }
        private async void deleteGuest(Guid Id)
        {
            try
            {
                await APIService.DeleteById("Guest/delete-guest", Id);
               
                MessageBox.Show("Uspješno obrisan gost");
                loadGuests();
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loadGuests(Name.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //ADD GUEST
            MainWindow.BackgroundOverlay.IsOpen = true;
            AddGuest addGuest = new AddGuest();
            addGuest.ShowDialog();
            MainWindow.BackgroundOverlay.IsOpen = false;
            loadGuests();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GuestGetDto edit = (((Button)sender)).DataContext as GuestGetDto;
            MainWindow.BackgroundOverlay.IsOpen = true;
            AddGuest addGuest = new AddGuest(edit);
            addGuest.ShowDialog();
            loadGuests();

            MainWindow.BackgroundOverlay.IsOpen = false;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GuestGetDto delete = (((Button)sender)).DataContext as GuestGetDto;
            deleteGuest(delete.Id);
    
        }
    }
}
