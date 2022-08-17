
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Common.Dto.City;
using Common.Dto.Company;
using Common.Dto.File;
using Common.Dto.Images;
using Core;
using Flurl.Http;
using Newtonsoft.Json;
using WPF.Dtos;
using Message = Core.Message;
using MessageBox = System.Windows.Forms.MessageBox;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : System.Windows.Controls.UserControl
    {
        public APIService service { get; set; } = new APIService();
        public ImageGetDto logo { get; set; }
        public ImageGetDto profileImageGet { get; set; }
        public System.Drawing.Image currentImage { get; set; }
        public System.Drawing.Image profileImage { get; set; }
        public Profile()
        {
            InitializeComponent();
            loadCities();
            loadImages();
        }
        private async void loadImages()
        {
            try
            {
                var data = await service.Get("File/get-images");
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<ImageGetDto>>(jsonResult);

                listImages.Children.Clear();
                foreach (var n in x)
                {
                    System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle();
                    rectangle.Width = 150;
                    rectangle.Height = 150;
                    rectangle.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    rectangle.Margin = new Thickness(10);
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri($"{service._endpointImage}{n.Path}"));
                    rectangle.Uid = n.Id.ToString();
                    rectangle.PreviewMouseDown += DockPanel_PreviewMouseDown;
                    rectangle.Fill = imageBrush;
                    listImages.Children.Add(rectangle);

                }
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Core.Message>();
                System.Windows.MessageBox.Show(error.Result.Info);
            }
        }
        private async void loadCities()
        {
            try
            {
                var data = await service.GetById("City/get-cities-by-country-id", "6770521D-70F2-4878-31E1-08DA6A8579B0"); //hardcoded on BiH
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<CityGetDto>>(jsonResult);
                cbCities.ItemsSource = x;
                cbCities.DisplayMemberPath = "Title";
                cbCities.SelectedValuePath = "Id";
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Core.Message>();
                System.Windows.MessageBox.Show(error.Result.Info);
            }
            loadCompanyData();
        }
        private async void loadCompanyData()
        {
            try
            {
                var data = await service.GetById("Company/get-company", APIService.CompanyId);
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<CompanyGetDto>(jsonResult);
                txtCompanyName.Text = x.Title;
                txtLatitude.Text = x.Location.Latitude;
                txtLongitude.Text = x.Location.Longitude;
                txtAddress.Text = x.Address;
                cbCities.SelectedValue = x.Location.City.Id;
                logo = x.Logo;
                logoImage.Source = new BitmapImage(new Uri($"{service._endpointImage}{x.Logo.Path}"));

                var data1 = await service.Get("File/get-profile-image");
                var jsonResult1 = JsonConvert.DeserializeObject(data1.Data.ToString()).ToString();
                var x1 = JsonConvert.DeserializeObject<ImageGetDto>(jsonResult1);
                myProfileImage.Source = new BitmapImage(new Uri($"{service._endpointImage}{x1.Path}"));
                profileImageGet = x1;
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Core.Message>();
                System.Windows.MessageBox.Show(error.Result.Info);
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                logoImage.Source = new BitmapImage(new Uri(openFile.FileName));
                System.Drawing.Image _image = System.Drawing.Image.FromFile(openFile.FileName);
                currentImage = _image;
            }


        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentImage != null)
                {
                    byte[] im = Globals.FromImageToByte(currentImage);
                    FileUploadDto fileUpload = new FileUploadDto();
                    fileUpload.ImageURL = im;
                    var data1 = await service.Post<Message>("File/upload-image", fileUpload);
                    var jsonResult = JsonConvert.DeserializeObject(data1.Data.ToString()).ToString();
                    logo = JsonConvert.DeserializeObject<ImageGetDto>(jsonResult);
                }
                if (profileImage != null)
                {
                    byte[] im = Globals.FromImageToByte(profileImage);
                    FileUploadDto fileUpload = new FileUploadDto();
                    fileUpload.ImageURL = im;
                    var data1 = await service.Post<Message>("File/upload-profile-image", fileUpload);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Greska");
            }
            CompanyUpdateDto companyUpdateDto = new CompanyUpdateDto();
            companyUpdateDto.Longitude = txtLongitude.Text;
            companyUpdateDto.Latitude = txtLatitude.Text;
            companyUpdateDto.Address = txtAddress.Text;
            companyUpdateDto.CityId = (Guid)cbCities.SelectedValue;
            companyUpdateDto.Title = txtCompanyName.Text;
            companyUpdateDto.LogoId = logo.Id;
            try
            {
                var data = await service.Put("Company/update-company", APIService.CompanyId, companyUpdateDto);
                await Task.Delay(2000);
                MessageBox.Show("Uspješno ste spasili postavke!");

            }
            catch (FlurlHttpException ex)
            {

            }
        }

        private async void btnNewPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Image _image = System.Drawing.Image.FromFile(openFile.FileName);
                try
                {
                    byte[] im = Globals.FromImageToByte(_image);
                    FileUploadDto fileUpload = new FileUploadDto();
                    fileUpload.ImageURL = im;
                    var data1 = await service.Post<Message>("File/upload-image", fileUpload);
                    var jsonResult = JsonConvert.DeserializeObject(data1.Data.ToString()).ToString();
                    logo = JsonConvert.DeserializeObject<ImageGetDto>(jsonResult);
                    loadImages();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Greska");
                }
            }
        }
        private async void deleteImage(Guid Id)
        {
            try
            {

                await service.DeleteById("File/delete-image", Id);
                System.Windows.MessageBox.Show("Uspješno izbrisano!");
                loadImages();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Greska");
            }
        }
        private async void DockPanel_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Guid Id = new Guid(((System.Windows.Shapes.Rectangle)sender).Uid);
            await Task.Delay(1000);
            MessageBoxResult result = System.Windows.MessageBox.Show("Da li ste sigurni da želite izbrisati sliku?",
               "Potvrda", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {

                deleteImage(Id);

            }
        }

        private void myProfileImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                myProfileImage.Source = new BitmapImage(new Uri(openFile.FileName));
                System.Drawing.Image _image = System.Drawing.Image.FromFile(openFile.FileName);
                profileImage = _image;
            }
        }
    }
}
