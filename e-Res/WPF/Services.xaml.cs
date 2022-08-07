using Common.Dto.Guests;
using Common.Dtos.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Services.xaml
    /// </summary>
    public partial class Services : UserControl
    {
        public MainWindow MainWindow { get; set; }
        public APIService APIService { get; set; } = new APIService();
        public Services(MainWindow mainWindow)
        {
            this.MainWindow = mainWindow;
            InitializeComponent();
            initialLoad();
        }


        private void initialLoad()
        {
            loadServices();
        }

        private async void loadServices(string search = "")
        {
            try
            {
                BaseSearchObject baseSearchObject = new BaseSearchObject { ByName = search, Id = (Guid)APIService.CompanyId };

                var data = await APIService.Post<Message>("Service/get-services-by-company-id", baseSearchObject);
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<ServicesGetDto>>(jsonResult);
                gridData.ItemsSource = x;

            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }
        private async void deleteService(Guid Id)
        {
            try
            {
                await APIService.DeleteById("Service/delete-service", Id);
                MessageBox.Show("Uspješno obrisana usluga");
                loadServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom brisanja podataka o usluzi");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //edit
            ServicesGetDto edit = (((Button)sender)).DataContext as ServicesGetDto;
            MainWindow.BackgroundOverlay.IsOpen = true;
            AddService addService = new AddService(edit);
            addService.ShowDialog();
            loadServices();

            MainWindow.BackgroundOverlay.IsOpen = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //delete
            ServicesGetDto delete = (((Button)sender)).DataContext as ServicesGetDto;
            deleteService(delete.Id);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //ADD Service
            MainWindow.BackgroundOverlay.IsOpen = true;
            AddService editService = new AddService();
            editService.ShowDialog();
            MainWindow.BackgroundOverlay.IsOpen = false;
            loadServices();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            loadServices(Title.Text);
        }
    }
}
