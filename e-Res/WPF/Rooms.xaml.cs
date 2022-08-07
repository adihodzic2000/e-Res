using Common.Dto.Rooms;
using Core;
using Core.SearchObjects;
using Flurl.Http;
using MaterialDesignThemes.Wpf;
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
using WPF.Dtos;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Rooms.xaml
    /// </summary>
    public partial class Rooms : UserControl
    {
        public MainWindow MainWindow { get; set; }
        public APIService APIService { get; set; } = new APIService();
        public PackIconKind Icon = PackIconKind.Edit;
        public Rooms(MainWindow MainWindow, bool IsApartment)
        {
            this.MainWindow = MainWindow;
            InitializeComponent();
            initialLoad();
            if (IsApartment)
                btnAdd.Visibility = Visibility.Hidden;
        }
        private async void loadRooms(string search = "")
        {
            try
            {
                BaseSearchObject baseSearchObject = new BaseSearchObject { ByName = search, Id = (Guid)APIService.CompanyId };

                var data = await APIService.Post<Message>("Room/get-rooms-by-company-id", baseSearchObject);
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<RoomGetDto>>(jsonResult);
                gridData.ItemsSource = x;

            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }
        private void initialLoad()
        {
            loadRooms();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loadRooms(txtRoom.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.BackgroundOverlay.IsOpen = true;
            AddRoom addRooms = new AddRoom();
            addRooms.ShowDialog();
            loadRooms();
            MainWindow.BackgroundOverlay.IsOpen = false;
        }
        private async void deleteRoom(Guid Id)
        {
            try
            {
                await APIService.DeleteById("Room/delete-room", Id);
                MessageBox.Show("Uspješno obrisana soba");
                loadRooms();
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RoomGetDto edit = (((Button)sender)).DataContext as RoomGetDto;
            MainWindow.BackgroundOverlay.IsOpen = true;
            AddRoom addGuest = new AddRoom(edit);
            addGuest.ShowDialog();
            loadRooms();

            MainWindow.BackgroundOverlay.IsOpen = false;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Ukoliko izbrišete sobu izbrisat ćete i sve pripadajuće rezervacije ovoj sobi. Da li i dalje želite nastaviti?",
                "Potvrda", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                RoomGetDto delete = (((Button)sender)).DataContext as RoomGetDto;
                deleteRoom(delete.Id);

            }

        }
    }
}
