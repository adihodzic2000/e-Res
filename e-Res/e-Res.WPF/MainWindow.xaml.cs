using e_Res.WPF.Dtos;
using e_Res.WPF.Enums;
using MaterialDesignThemes.Wpf;
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

namespace e_Res.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Home, "Početna", PackIconKind.Home)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Guests, "Gosti", PackIconKind.GuestRoom)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Services, "Usluge", PackIconKind.ServiceToolbox)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Calendar, "Kalendar", PackIconKind.Calendar)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Rooms, "Sobe", PackIconKind.RoomService)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Bills, "Računi", PackIconKind.AttachMoney)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.Chat, "Chat", PackIconKind.Chat)));
            Menu.Children.Add(new UserControlMenuItem(new ItemMenu(Categories.EditProfile, "Uredi profil", PackIconKind.Man)));

        }

        private void Menu_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dataContext = ((ItemMenu)((FrameworkElement)e.OriginalSource).DataContext);
            if (dataContext.Category == Categories.Guests)
            {
                Child.Children.Clear();
                Guests frmVerification = new Guests();
                Child.Children.Add(frmVerification);
            }
            else if(dataContext.Category == Categories.Calendar)
            {
                Child.Children.Clear();
                Calendar frmCalendar = new Calendar();
                Child.Children.Add(frmCalendar);
            }
            else if (dataContext.Category == Categories.Rooms)
            {
                Child.Children.Clear();
                Rooms frmRooms = new Rooms();
                Child.Children.Add(frmRooms);
            }
            else if (dataContext.Category == Categories.Services)
            {
                Child.Children.Clear();
                Services frmServices = new Services();
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
            else
                Child.Children.Clear();

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
    }
}
