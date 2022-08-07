using Common.Dto.ReservationServices;
using Common.Dtos.Services;
using Core;
using Core.SearchObjects;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddingServiceToReservation.xaml
    /// </summary>
    public partial class AddingServiceToReservation : Window
    {
        public APIService APIService { get; set; } = new APIService();
        public List<AddingServiceDto> addingServiceDto = new List<AddingServiceDto>();
        public Guid _reservationId { get; set; }
        public AddingServiceToReservation(Guid reservationId)
        {
            InitializeComponent();
            dgTmpData.AutoGenerateColumns = false;
            _reservationId = reservationId;
            loadReservationServices();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (cbServices.SelectedItem == null || string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("Unesite vrijednost za uslugu i količinu !");
                return;
            }
            AddingServiceDto addingService = new AddingServiceDto
            {
                Id = (Guid)cbServices.SelectedValue,
                Quantity = int.Parse(txtQuantity.Text),
                Title = ((ServicesGetDto)cbServices.SelectedItem).Title,
                TotalAmount = int.Parse(txtQuantity.Text) * ((ServicesGetDto)cbServices.SelectedItem).Price,
                rb = addingServiceDto.Count
            };

            addingServiceDto.Add(addingService);
            dgTmpData.ItemsSource = addingServiceDto.ToList();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var service in dgTmpData.Items)
                {

                    AddServiceToGuest addServiceToGuest = new AddServiceToGuest();
                    addServiceToGuest.ServiceId = ((AddingServiceDto)service).Id;
                    addServiceToGuest.Quantity = ((AddingServiceDto)service).Quantity;
                    addServiceToGuest.ReservationId = _reservationId;

                    var data = await APIService.Post<Message>("Service/add-service-to-guest", addServiceToGuest);
                }
                loadReservationServices();
                dgTmpData.ItemsSource = null;
                addingServiceDto = new List<AddingServiceDto>();

            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
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

                cbServices.ItemsSource = x;

                cbServices.DisplayMemberPath = "Title";
                cbServices.SelectedValuePath = "Id";

            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }
        private async void loadReservationServices()
        {
            try
            {

                var data = await APIService.GetById("Reservation/get-reservation-services", _reservationId);
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<ReservationServicesGetDto>>(jsonResult);
                List<AddingServiceDto> baseData = new List<AddingServiceDto>();
                foreach (var r in x)
                {
                    AddingServiceDto n = new AddingServiceDto();
                    n.Id = r.Id;
                    n.Title = r.Service.Title;
                    n.Quantity = r.Quantity;
                    n.Id = Guid.Empty;
                    n.rb = 0;
                    n.TotalAmount = r.Quantity * r.Service.Price;
                    baseData.Add(n);
                }
                dgBaseData.ItemsSource = baseData;

            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddingServiceDto rb = ((AddingServiceDto)((Button)sender).DataContext);
            addingServiceDto.Remove(rb);
            dgTmpData.ItemsSource = null;
            dgTmpData.ItemsSource = addingServiceDto;
        }

        private void txtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
