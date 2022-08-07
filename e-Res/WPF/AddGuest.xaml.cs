using Common.Dto.Guests;
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
    /// Interaction logic for AddGuest.xaml
    /// </summary>
    public partial class AddGuest : Window
    {
        public APIService APIService { get; set; } = new APIService();
        public GuestGetDto GuestGetDto { get; set; }

        public AddGuest(GuestGetDto guestGetDto = null)
        {
            InitializeComponent();
            this.GuestGetDto = guestGetDto;
            if (guestGetDto != null)
            {
                _nameOfAction.Text = "AŽURIRANJE PODATAKA OD KORISNIKA";
                loadData();
                Action.Content = "SPASI PROMJENE";
            }
            else
                Action.Content = "DODAJ GOSTA";
                
        }

        private void loadData()
        {
            FirstName.Text = this.GuestGetDto.FirstName;
            LastName.Text = this.GuestGetDto.LastName;
            phoneNumber.Text = this.GuestGetDto.PhoneNumber;
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
                if (GuestGetDto == null)
                {
                    GuestCreateDto guestCreateDto = new GuestCreateDto
                    {
                        CompanyId = (Guid)APIService.CompanyId,
                        FirstName = FirstName.Text,
                        LastName = LastName.Text,
                        PhoneNumber = phoneNumber.Text,
                    };
                    var data = await APIService.Post<Message>("Guest/add-guest", guestCreateDto);
                    MessageBox.Show("Uspješno ste dodali korisnika!");
                }
                else
                {
                    mess = "ažuriranja";
                    GuestUpdateDto guestCreateDto = new GuestUpdateDto
                    {
                        CompanyId = (Guid)APIService.CompanyId,
                        FirstName = FirstName.Text,
                        LastName = LastName.Text,
                        PhoneNumber = phoneNumber.Text,
                    };
                    var data = await APIService.Put("Guest/update-guest", GuestGetDto.Id, guestCreateDto);
                    if (data.IsValid)
                        MessageBox.Show("Uspješno ste izmjenuli podatke od korisnika!");
                    
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
