using Common.Dto.Reservations;
using Common.Dto.Rooms;
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
using WPF.Dtos;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : UserControl
    {
        public MainWindow MainWindow { get; set; }
        public APIService APIService { get; set; } = new APIService();
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public int CurrentMonth { get; set; } = DateTime.Now.Month;
        List<ReservationGetDto> reservations = new List<ReservationGetDto>();
        public Calendar(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadReservations();
            lblDate.Content = $"{CurrentYear}, {Globals.GetMonthName(CurrentMonth)}";
            loadRooms();
            displayDays();
        }
        private async void loadReservations()
        {
            try
            {
                var data = await APIService.Get("Reservation/get-reservations-by-company-id");
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                reservations = JsonConvert.DeserializeObject<List<ReservationGetDto>>(jsonResult);
                displayDays();
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }
        private async void loadRooms()
        {
            try
            {
                BaseSearchObject baseSearchObject = new BaseSearchObject { ByName = "", Id = (Guid)APIService.CompanyId };

                var data = await APIService.Post<Message>("Room/get-rooms-by-company-id", baseSearchObject);
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<RoomGetDto>>(jsonResult);

                foreach (var room in x)
                {
                    DockPanel dockPanel = new DockPanel();
                    Border border = new Border();
                    border.Width = 20;
                    border.Height = 20;
                    border.CornerRadius = new CornerRadius(20);
                    border.Background = Helper.Colors.GetColorFromHex(room.Color);
                    TextBlock textBlock = new TextBlock();
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.Foreground = Helper.Colors.WhiteColor();
                    textBlock.Text = room.Title;
                    textBlock.Margin = new Thickness(5);

                    dockPanel.Children.Add(border);
                    dockPanel.Children.Add(textBlock);
                    clRooms.Children.Add(dockPanel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom preuzimanja soba!");
            }
        }

        private void displayDays()
        {
            DateTime now = new DateTime(CurrentYear, CurrentMonth, 1);


            DateTime MonthStartDay = new DateTime(now.Year, now.Month, 1);

            int daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);

            int dayOfTheWeek = Convert.ToInt32(MonthStartDay.DayOfWeek.ToString("d")) - 1;
            if (dayOfTheWeek < 0)
                dayOfTheWeek = 7 + dayOfTheWeek;
            int column = 0;
            int row = 0;
            column = dayOfTheWeek;
            GridDays.Children.Clear();
            //List<ReservationGetDto> reservationList = reservations.Where(x => x.DateFrom.Month == MonthStartDay.Month || x.DateTo.Month == MonthStartDay.Month).ToList();
            List<ReservationGetDto> reservationList = reservations.Where(x => x.DateFrom.Month <= MonthStartDay.Month || x.DateTo.Month >= MonthStartDay.Month).ToList();
            for (int i = dayOfTheWeek + 1; i < daysInMonth + dayOfTheWeek + 1; i++)
            {
                BrushConverter br1 = new BrushConverter();
                Brush brush1 = br1.ConvertFrom("#418bb0") as Brush;
                brush1.Freeze();

                BrushConverter br2 = new BrushConverter();
                Brush brush2 = br2.ConvertFrom("#ffffff") as Brush;
                brush2.Freeze();


                //Day day = new Day(i.ToString());
                Border border = new Border();
                border.CornerRadius = new CornerRadius(15);
                border.Background = brush1;
                border.Margin = new Thickness(5);
                ScrollViewer scrollViewer = new ScrollViewer();

                StackPanel stackPanel = new StackPanel();

                TextBlock textBlock = new TextBlock();
                textBlock.Text = (i - dayOfTheWeek).ToString();
                textBlock.FontSize = 20;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Top;
                textBlock.Foreground = brush2;
                stackPanel.Children.Add(textBlock);
                string ToolTip = "";
                foreach (var reservation in reservationList)
                {
                    var dateFromDiff = Globals.diffDays(reservation.DateFrom);
                    var dateToDiff = Globals.diffDays(reservation.DateTo);
                    var diff = Globals.diffDays(new DateTime(CurrentYear, CurrentMonth, i - dayOfTheWeek));
                    if (dateFromDiff <= diff && dateToDiff >= diff)
                    //if (reservation.DateFrom.Day <= i - dayOfTheWeek && reservation.DateTo.Day >= i - dayOfTheWeek && (reservation.DateFrom.Month==CurrentMonth))
                    {

                        TextBlock _reservation = new TextBlock();
                        _reservation.Padding = new Thickness(3);
                        _reservation.Foreground = Helper.Colors.WhiteColor();
                        _reservation.Text = reservation.Guest.FirstName + " " + reservation.Guest.LastName;
                        _reservation.FontSize = 10;
                        _reservation.HorizontalAlignment = HorizontalAlignment.Center;
                        _reservation.TextWrapping = TextWrapping.Wrap;
                        _reservation.Background = Helper.Colors.GetColorFromHex(reservation.Room.Color);
                        _reservation.Uid = reservation.Id.ToString();
                        _reservation.PreviewMouseLeftButtonDown += TextBlock_PreviewMouseLeftButtonDown;
                        stackPanel.Children.Add(_reservation);
                        //ToolTip += string.IsNullOrWhiteSpace(ToolTip) == true ? "" : "\n";
                        //ToolTip += reservation.Guest.FirstName + " " + reservation.Guest.LastName + " " + reservation.Room.Title;

                    }
                }
                //border.ToolTip = ToolTip;
                scrollViewer.Content = stackPanel;
                border.Child = (scrollViewer);
                Grid.SetRow(border, row);
                Grid.SetColumn(border, column);
                GridDays.Children.Add(border);
                column++;
                if (column == 7)
                {
                    row++;
                    column = 0;
                }
            }
            lblDate.Content = $"{CurrentYear}, {Globals.GetMonthName(CurrentMonth)}";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentMonth++;
            if (CurrentMonth > 12)
            {
                CurrentYear++;
                CurrentMonth = 1;
            }
            displayDays();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CurrentMonth--;
            if (CurrentMonth < 1)
            {
                CurrentYear--;
                CurrentMonth = 12;
            }
            displayDays();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            MainWindow.BackgroundOverlay.IsOpen = true;
            AddReservation addReservation = new AddReservation();
            addReservation.ShowDialog();
            MainWindow.BackgroundOverlay.IsOpen = false;
            loadReservations();
        }

        private async void TextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var data = await APIService.GetById("Reservation/get-reservation", ((TextBlock)sender).Uid);
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var selectedReservation = new ReservationGetDto();
                selectedReservation = JsonConvert.DeserializeObject<ReservationGetDto>(jsonResult);
                if (selectedReservation.IsFinished == true)
                {
                    MessageBox.Show("Rezervacija završena!");
                    return;
                }
                MainWindow.BackgroundOverlay.IsOpen = true;
                AddReservation addReservation = new AddReservation(selectedReservation);
                addReservation.ShowDialog();
                MainWindow.BackgroundOverlay.IsOpen = false;
                loadReservations();
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }

        }
    }
}
