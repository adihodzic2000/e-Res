using Common.Dto.Auth;
using Core;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public APIService APIService { get; set; } = new APIService();

        public Login()
        {
            InitializeComponent();
        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password.Password.Length > 0)
                hintPassword.Text = "";
            else
                hintPassword.Text = "Password";

        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.ShowDialog();
            this.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Spinner.Opacity = 70;
            btnLogin.IsEnabled = false;
            try
            {
                var username = Username.Text;
                var password = Password.Password;
                LoginDto loginDto = new LoginDto
                {
                    Username = username,
                    Password = password
                };
                SessionDto response = await APIService.Post<SessionDto>("Auth", loginDto);

                APIService.CompanyId = response.CompanyId;
                APIService.Token = "bearer " + response.Token;
                APIService.MyId = response.UserId;
                APIService.FullName = response.FirstName + " " + response.LastName;
                Application.Current.MainWindow = new MainWindow();
                Application.Current.MainWindow.Show();
                this.Close();

                //SessionDto session = (SessionDto)response.Data;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogrijesili ste korisničko ime ili lozinku!");
            }
            btnLogin.IsEnabled = true;
            Spinner.Opacity = 0;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
              
               // var n = await APIService.PostPayment<dynamic>(loginDto);



            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogrijesili ste korisničko ime ili lozinku!");
            }
        }
    }
}
