using Common.Dto.Company;
using Common.Dtos.Chat;
using Core;
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
using System.Windows.Shapes;
using WPF.Dtos;
using WPF.Enums;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public APIService APIService { get; set; } = new APIService();
        public CompanyGetDto CompanyData { get; set; }
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getCompanyData();
        }
        public async void getCompanyData()
        {
            try
            {
                var data = await APIService.GetById("Company/get-company", APIService.CompanyId);
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                CompanyData = JsonConvert.DeserializeObject<CompanyGetDto>(jsonResult);

                APIService.LogoPath = new Uri($"{APIService._endpointImage}{CompanyData.Logo.Path}").AbsoluteUri;
                myImage.ImageSource = new BitmapImage(new Uri($"{APIService._endpointImage}{CompanyData.Logo.Path}"));
                CompanyName.Text = CompanyData.Title;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom preuzimanja podataka o kompaniji");
            }
            string messagesNumber = "";
            try
            {

                var data2 = await APIService.Get("Chat/get-unclicked-messages");
                var jsonResult2 = JsonConvert.DeserializeObject(data2.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<GetMessageDto>>(jsonResult2);
                if (x.Count() > 0)
                    messagesNumber = " (" + x.Count() + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom preuzimanja podataka o porukama koje niste vidjeli");
            }
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Home, "Početna", PackIconKind.Home)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Guests, "Gosti", PackIconKind.GuestRoom)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Services, "Usluge", PackIconKind.ServiceToolbox)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Calendar, "Kalendar", PackIconKind.Calendar)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Rooms, $"Sobe", PackIconKind.RoomService)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Bills, "Računi", PackIconKind.AttachMoney)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Chat, $"Chat{messagesNumber}", PackIconKind.Chat)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.EditProfile, "Uredi profil", PackIconKind.Company)));


            Child.Children.Clear();
            Dashboard frmDashboard = new Dashboard();
            Child.Children.Add(frmDashboard);
        }
        private async void Menu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dataContext = ((ItemMenu)((FrameworkElement)e.OriginalSource).DataContext);
            if (dataContext.Category == Categories.Guests)
            {
                Child.Children.Clear();
                Guests frmVerification = new Guests(this);
                Child.Children.Add(frmVerification);
            }
            else if (dataContext.Category == Categories.Calendar)
            {
                Child.Children.Clear();
                Calendar frmCalendar = new Calendar(this);
                Child.Children.Add(frmCalendar);
            }
            else if (dataContext.Category == Categories.Rooms)
            {
                Child.Children.Clear();
                Rooms frmRooms = new Rooms(this, CompanyData.IsApartment);
                Child.Children.Add(frmRooms);
            }
            else if (dataContext.Category == Categories.Services)
            {
                Child.Children.Clear();
                Services frmServices = new Services(this);
                Child.Children.Add(frmServices);
            }
            else if (dataContext.Category == Categories.Bills)
            {
                Child.Children.Clear();
                Bills frmBills = new Bills();
                Child.Children.Add(frmBills);
            }
            else if (dataContext.Category == Categories.Chat)
            {
                Child.Children.Clear();
                Chat frmChat = new Chat();
                Child.Children.Add(frmChat);
            }
            else if (dataContext.Category == Categories.EditProfile)
            {
                Child.Children.Clear();
                Profile frmProfile = new Profile();
                Child.Children.Add(frmProfile);
            }
            else if (dataContext.Category == Categories.Home)
            {
                Child.Children.Clear();
                Dashboard frmDashboard = new Dashboard();
                Child.Children.Add(frmDashboard);
            }
            else
                Child.Children.Clear();

            try
            {
                string messagesNumber = "";

                var data2 = await APIService.Get("Chat/get-unclicked-messages");
                var jsonResult2 = JsonConvert.DeserializeObject(data2.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<GetMessageDto>>(jsonResult2);
                if (x.Count() > 0)
                {
                    messagesNumber = " (" + x.Count() + ")";
                    //Menu.Children[6] = new UserControlMenuItem(new ItemMenu(Categories.Chat, $"Chat{messagesNumber}", PackIconKind.Chat));
                }
                var child = ((UserControlMenuItem)Menu.Children[6]).DataContext;
                ((ItemMenu)child).Header = $"Chat{messagesNumber}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom preuzimanja podataka o porukama koje niste vidjeli");
            }

        }

        private void Menu_Expanded(object sender, RoutedEventArgs e)
        {
            var lastClickedExpander = (Expander)e.OriginalSource;
            BrushConverter bc = new BrushConverter();
            Brush white = bc.ConvertFrom("#ffffff") as Brush;
            white.Freeze();

            BrushConverter bc2 = new BrushConverter();
            Brush blue = bc2.ConvertFrom("#0e4b6c") as Brush;
            blue.Freeze();
            foreach (var child in Menu.Children)
            {
                var OneByOne = ((UserControlMenuItem)child).ExpanderMenu;

                if (string.Compare(((ItemMenu)lastClickedExpander.DataContext).Header.ToString(), ((ItemMenu)OneByOne.DataContext).Header.ToString()) != 0 && OneByOne.IsExpanded)
                {
                    OneByOne.IsExpanded = false;
                }
                    ((Border)OneByOne.Header).BorderThickness = new Thickness(0, 0, 0, 0);
                OneByOne.Foreground = white;
                OneByOne.BorderBrush = blue;
            }

            BrushConverter br = new BrushConverter();
            Brush brush = br.ConvertFrom("#ffffff") as Brush;
            brush.Freeze();
            lastClickedExpander.Foreground = brush;
            BrushConverter br2 = new BrushConverter();
            Brush brush2 = br2.ConvertFrom("#08669b") as Brush;
            brush2.Freeze();
            lastClickedExpander.BorderBrush = brush2;
            ((Border)lastClickedExpander.Header).BorderThickness = new Thickness(5, 0, 0, 0);
        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await APIService.Post<dynamic>("Auth/logout", null);
                Application.Current.MainWindow = new Login();
                Application.Current.MainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                Application.Current.MainWindow = new Login();
                Application.Current.MainWindow.Show();
                this.Close();
            }
        }
    }
}
