using Common.Dto.Guests;
using Common.Dto.Reservations;
using Common.Dto.Rooms;
using Common.Dtos.Reservations;
using Core;
using Core.SearchObjects;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddReservation.xaml
    /// </summary>
    public partial class AddReservation : Window
    {
        public APIService APIService { get; set; } = new APIService();
        ReservationGetDto reservationGetDto = new ReservationGetDto();

        public AddReservation(ReservationGetDto reservationGetDto = null)
        {
            InitializeComponent();
            this.reservationGetDto = reservationGetDto;
            initialLoad();
            Action.Content = "SAČUVAJ";

        }
        private void initialLoad()
        {

            loadGuests();
            loadRooms();
            if (reservationGetDto != null)
            {
                dateFrom.SelectedDate = reservationGetDto.DateFrom;

                dateTo.SelectedDate = reservationGetDto.DateTo;

                btnRR.Visibility = Visibility.Visible;
                btnASG.Visibility = Visibility.Visible;
                btnMR.Visibility = Visibility.Visible;
            }

        }
        private async void loadGuests(string search = "")
        {
            try
            {
                BaseSearchObject baseSearchObject = new BaseSearchObject { ByName = search, Id = (Guid)APIService.CompanyId };

                var data = await APIService.Post<Message>("Guest/get-guest-by-company-id", baseSearchObject);
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<GuestGetDto>>(jsonResult);

                cbGuests.ItemsSource = x;

                cbGuests.DisplayMemberPath = "FullName";
                cbGuests.SelectedValuePath = "Id";
                if (reservationGetDto != null)
                {
                    cbGuests.SelectedValue = reservationGetDto.Guest.Id;
                }

            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }
        private async void loadRooms(string search = "")
        {
            try
            {
                BaseSearchObject baseSearchObject = new BaseSearchObject { ByName = search, Id = (Guid)APIService.CompanyId };

                var data = await APIService.Post<Message>("Room/get-rooms-by-company-id", baseSearchObject);
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<RoomGetDto>>(jsonResult);

                cbRooms.ItemsSource = x;

                cbRooms.DisplayMemberPath = "Title";
                cbRooms.SelectedValuePath = "Id";
                if (reservationGetDto != null)
                {

                    cbRooms.SelectedValue = reservationGetDto.Room.Id;
                _nameOfAction.Text = "Ažuriranje rezervacije";
                }



            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                throw new InvalidOperationException(error.Result.Info);
            }
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                if (reservationGetDto == null)
                {
                    if (dateFrom.SelectedDate == null || dateTo.SelectedDate == null || cbGuests.SelectedValue == null)
                    {
                        MessageBox.Show("Sva polja osim sobe su obavezna!");
                        return;
                    }

                    ReservationCreateDto reservationCreateDto = new ReservationCreateDto();

                    reservationCreateDto.DateFrom = (DateTime)dateFrom.SelectedDate;
                    reservationCreateDto.DateTo = (DateTime)dateTo.SelectedDate;
                    reservationCreateDto.GuestId = (Guid)cbGuests.SelectedValue == null ? Guid.Empty : (Guid)cbGuests.SelectedValue;
                    reservationCreateDto.RoomId = cbRooms.SelectedValue == null ? Guid.Empty : (Guid)cbRooms.SelectedValue;

                    var data = await APIService.Post<Message>("Reservation/create-reservation", reservationCreateDto);
                    var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                    MessageBox.Show("Uspješno ste dodali rezervaciju!");
                }
                else
                {
                    ReservationUpdateDto reservationUpdateDto = new ReservationUpdateDto
                    {
                        DateFrom = (DateTime)dateFrom.SelectedDate,
                        DateTo = (DateTime)dateTo.SelectedDate,
                        GuestId = (Guid)cbGuests.SelectedValue,
                        RoomId = (Guid)cbRooms.SelectedValue,
                    };
                    var data = await APIService.Put("Reservation/update-reservation", reservationGetDto.Id, reservationUpdateDto);
                    MessageBox.Show("Uspješno ste ažurirali rezervaciju!");
                }
                this.Close();
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnRR_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                await APIService.DeleteById("Reservation/delete-reservation", reservationGetDto.Id);
                MessageBox.Show("Uspješno ste izbrisali rezervaciju!");


                this.Close();
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }

        private void btnASG_Click(object sender, RoutedEventArgs e)
        {
            var addingServiceToReservation = new AddingServiceToReservation(reservationGetDto.Id);
            addingServiceToReservation.ShowDialog();
        }

        private async void btnMR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FinishReservationDto finishReservationDto = new FinishReservationDto();
                finishReservationDto.Id = reservationGetDto.Id;
                finishReservationDto.IsFinished = true;
                var message = await APIService.Post<Message>("Reservation/mark-reservation-as-finished",finishReservationDto);

                MessageBox.Show(message.Info);

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
