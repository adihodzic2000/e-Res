using Common.Dtos.Verification;
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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public APIService APIService { get; set; } = new APIService();
        public string Email { get; set; }
        public bool IsPassed {get;set;}
        public Guid UserId { get; set; }
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void txtNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtNewPassword.Password.Length > 0)
                hintNewPassword.Text = "";
            else
                hintNewPassword.Text = "Password";
        }

        private void txtRepeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtRepeatPassword.Password.Length > 0)
                hintRepeatPassword.Text = "";
            else
                hintRepeatPassword.Text = "Password";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                VerificationCreateDto verification = new VerificationCreateDto();
                verification.Email = txtEmail.Text;

                var data = await APIService.Post<Message>("User/forgot-password", verification);

                if (data.IsValid)
                {
                    txtEmail.IsEnabled = false;
                    btnCheckEmail.IsEnabled = false;
                }
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }

        }

        private async void Button_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                VerificationCodeDto verification = new VerificationCodeDto();
                verification.Email = txtEmail.Text;
                verification.Code = txtCode.Text;
                var data = await APIService.Post<Message>("User/check-code", verification);

                if (data.IsValid)
                {
                    txtCode.IsEnabled = false;
                    btnCheckCode.IsEnabled = false;
                    IsPassed = true;
                    UserId = new Guid((string)data.Data);
                }
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Message>();
                MessageBox.Show(error.Result.Info);
            }
        }

        private async void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (txtNewPassword.Password == txtRepeatPassword.Password)
            {

                try
                {
                    NewPasswordDto verification = new NewPasswordDto();
                    verification.Id = UserId;
                    verification.Password = txtNewPassword.Password;
                    var data = await APIService.Post<Message>("User/change-password", verification);

                    if (data.IsValid)
                    {
                        this.Close();
                        Login frmLogin = new Login();
                        frmLogin.ShowDialog();
                    }
                }
                catch (FlurlHttpException ex)
                {
                    var error = ex.GetResponseJsonAsync<Message>();
                    MessageBox.Show(error.Result.Info);
                }
            }
            else lblPassword.Visibility = Visibility.Visible;
        }

        private void lblPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
        
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            Login frmLogin = new Login();
            frmLogin.ShowDialog();
        }
    }
}
