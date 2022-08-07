using Common.Dto.Rooms;
using Core;
using MaterialDesignThemes.Wpf;
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

namespace WPF
{
    /// <summary>
    /// Interaction logic for AddRoom.xaml
    /// </summary>
    public partial class AddRoom : Window
    {
        public APIService APIService { get; set; } = new APIService();
        public RoomGetDto roomGetDto { get; set; }

        public AddRoom(RoomGetDto roomGetDto = null)
        {
            InitializeComponent();
            this.roomGetDto = roomGetDto;
            if(roomGetDto != null)
            {
                color.Color = (Color)ColorConverter.ConvertFromString(roomGetDto.Color);
                txtDescription.Text = roomGetDto.Description;
                price.Text = roomGetDto.Price.ToString();
                txtTitle.Text = roomGetDto.Title;
 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var n = ((ColorPicker)color).Color;
            try
            {
                if (roomGetDto == null)
                {
                    RoomCreateDto roomCreateDto = new RoomCreateDto
                    {
                        Color = n.ToString(),
                        CompanyId = (Guid)APIService.CompanyId,
                        Description = txtDescription.Text,
                        Price = double.Parse(price.Text),
                        Title = txtTitle.Text
                    };
                    var data = await APIService.Post<Message>("Room/add-room", roomCreateDto);
                    var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                    MessageBox.Show("Uspješno ste dodali sobu!");
                }
                else
                {
                    RoomUpdateDto roomUpdateDto= new RoomUpdateDto
                    {
                        Color = n.ToString(),
                        CompanyId = (Guid)APIService.CompanyId,
                        Description = txtDescription.Text,
                        Price = double.Parse(price.Text),
                        Title = txtTitle.Text
                    };
                    var data = await APIService.Put("Room/update-room", roomGetDto.Id, roomUpdateDto);
                    if (data.IsValid)
                        MessageBox.Show("Uspješno ste izmjenuli podatke od sobe!");

                    else
                        MessageBox.Show("Neuspješno spašavanje podataka!");
                }
                this.Close();
            }
            catch (Flurl.Http.FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }

        private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }

}
