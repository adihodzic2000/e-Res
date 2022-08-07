using e_Res.WPF.Dtos;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace e_Res.WPF
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : System.Windows.Controls.UserControl
    {
        public APIService service { get; set; } = new APIService("File");

        public Profile()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                image.Source = new BitmapImage(new Uri(openFile.FileName));
                Image _image=Image.FromFile(openFile.FileName);
                try
                {
                    byte[] im= Globals.FromImageToByte(_image);

                    await service.Post<Message>("test");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Greska");
                }
            }


        }
    }
}
