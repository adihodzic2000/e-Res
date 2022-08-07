using Common.Dtos.Services;
using Core;
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

namespace WPF
{
    /// <summary>
    /// Interaction logic for AddService.xaml
    /// </summary>
    public partial class AddService : Window
    {
        public APIService APIService { get; set; } = new APIService();
        public ServicesGetDto servicesGetDto { get; set; }
        public AddService(ServicesGetDto servicesGetDto = null)
        {
            InitializeComponent();
            this.servicesGetDto = servicesGetDto;
            if (servicesGetDto != null)
            {
                _nameOfAction.Text = "AŽURIRANJE PODATAKA OD USLUGE";
                loadData();
                Action.Content = "SPASI PROMJENE";
            }
            else
                Action.Content = "DODAJ USLUGU";
        }
        private void loadData()
        {
            Title.Text = this.servicesGetDto.Title;
            Price.Text = this.servicesGetDto.Price.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string mess = "dodavanja";
            try
            {
                if (servicesGetDto == null)
                {
                    ServicesCreateDto serviceCreateDto = new ServicesCreateDto
                    {
                        Title = Title.Text,
                        Price = double.Parse(Price.Text),
                    };
                    var data = await APIService.Post<Message>("Service/add-service", serviceCreateDto);
                    var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                    MessageBox.Show("Uspješno ste dodali uslugu!");
                }
                else
                {
                    mess = "ažuriranja";
                    ServicesUpdateDto servicesUpdateDto = new ServicesUpdateDto
                    {
                        Title = Title.Text,
                        Price = double.Parse(Price.Text),
                    };
                    var data = await APIService.Put("Service/update-service", servicesGetDto.Id, servicesUpdateDto);
                    if (data.IsValid)
                        MessageBox.Show("Uspješno ste izmjenuli podatke od USLUGE!");

                    else
                        MessageBox.Show("Neuspješno spašavanje podataka!");
                }
                this.Close();
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }
    }
}
