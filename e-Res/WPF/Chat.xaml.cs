using Common.Dto.User;
using Common.Dtos.Chat;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using Core.SearchObjects;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : System.Windows.Controls.UserControl
    {
        public APIService APIService { get; set; } = new APIService();
        public List<UserGetDto> userGetDtos { get; set; } = new List<UserGetDto>();
        private Guid CurrentUser { get; set; }
        public Chat()
        {
            InitializeComponent();
            loadContacts();
            scrollViewer.ScrollToEnd();
        }
        private Timer timer1;
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 10000; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadMessages();
        }
        private async void loadContacts(bool canRepeat = false)
        {
            try
            {
                SearchByName request = new SearchByName { Name = "" };
                var data1 = await APIService.Post<Core.Message>("Chat/get-my-users", request);
                var jsonResult = JsonConvert.DeserializeObject(data1.Data.ToString()).ToString();
                userGetDtos = JsonConvert.DeserializeObject<List<UserGetDto>>(jsonResult);

                var data2 = await APIService.Get("Chat/get-unclicked-messages");
                var jsonResult2 = JsonConvert.DeserializeObject(data2.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<GetMessageDto>>(jsonResult2);
                contacts.Children.Clear();
                foreach (var n in userGetDtos)
                {
                    int counter = x.FindAll(x => x.UserFromId == n.Id).Count();

                    System.Windows.Controls.Button button = new System.Windows.Controls.Button();
                    button.BorderThickness = new Thickness(0);
                    button.Padding = new Thickness(0);
                    button.Height = 50;
                    button.Width = 260;
                    button.Uid = n.Id.ToString();
                    button.Click += Button_Clik1;
                    DockPanel dockPanel = new DockPanel();
                    dockPanel.Width = 260;

                    Rectangle rectangle = new Rectangle();
                    rectangle.RadiusX = 50;
                    rectangle.RadiusY = 50;
                    rectangle.Width = 40;
                    rectangle.Height = 40;
                    rectangle.StrokeThickness = 1;
                    rectangle.Stroke = Helper.Colors.WhiteColor();

                    ImageBrush image = new ImageBrush();
                    image.ImageSource = new BitmapImage(new Uri($"{APIService._endpointImage}{n.Image.Path}"));

                    rectangle.Fill = image;
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = n.FirstName + " " + n.LastName + (counter == 0 ? "" : (" (" + counter + ")"));
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.Foreground = Helper.Colors.WhiteColor();
                    textBlock.FontSize = 15;
                    textBlock.Margin = new Thickness(5, 0, 0, 0);
                    dockPanel.Children.Add(rectangle);
                    dockPanel.Children.Add(textBlock);

                    button.Content = dockPanel;
                    contacts.Children.Add(button);
                }
                if (userGetDtos.Count == 0)
                {
                    chat.Visibility = Visibility.Hidden;
                    System.Windows.MessageBox.Show("Nemate još upućenih poruka!");
                }
                else
                {
                    if (!canRepeat)
                    {
                        CurrentUser = userGetDtos[0].Id;
                        user.Text = userGetDtos[0].FirstName + " " + userGetDtos[0].LastName;

                        myImage.ImageSource = new BitmapImage(new Uri($"{APIService._endpointImage}{userGetDtos[0].Image.Path}"));
                        loadMessages();
                    }
                }
            }
            catch (FlurlHttpException ex)
            {
               // var error = ex.GetResponseJsonAsync<Core.Message>();
                System.Windows.Forms.MessageBox.Show("Greška");
            }

        }

        private void Button_Clik1(object sender, RoutedEventArgs e)
        {
            var newUser = new Guid(((System.Windows.Controls.Button)sender).Uid);
            CurrentUser = newUser;
            var _user = userGetDtos.Where(x => x.Id == newUser).FirstOrDefault();
            user.Text = _user.FirstName + " " + _user.LastName;
            myImage.ImageSource = new BitmapImage(new Uri($"{APIService._endpointImage}{_user.Image.Path}"));
            loadMessages();
        }

        private async void loadMessages()
        {
            try
            {
                var data3 = await APIService.Put($"Chat/see-unclicked-messages", CurrentUser, null);
                loadContacts(true);
                
                var data1 = await APIService.Get($"Chat/get-messages/{APIService.MyId}/{CurrentUser}");
                var jsonResult = JsonConvert.DeserializeObject(data1.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<GetMessageDto>>(jsonResult);
                var orderByDate = x.OrderBy(x => x.CreatedDate);
                messages.Children.Clear();
                foreach (var n in orderByDate)
                {
                    if (n.UserFromId == CurrentUser)
                    {
                        Border border = new Border();
                        border.Width = 400;
                        border.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        border.CornerRadius = new CornerRadius(2, 10, 10, 2);
                        border.Padding = new Thickness(5);
                        border.Margin = new Thickness(5);
                        border.Background = Helper.Colors.WhiteColor();

                        TextBlock textBlock = new TextBlock();
                        textBlock.TextWrapping = TextWrapping.Wrap;

                        textBlock.Text = n.Content;
                        border.Child = textBlock;
                        messages.Children.Add(border);
                    }
                    else if (n.UserFromId == APIService.MyId)
                    {
                        Border border = new Border();
                        border.Width = 400;
                        border.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                        border.CornerRadius = new CornerRadius(10, 2, 2, 10);
                        border.Padding = new Thickness(5);
                        border.Margin = new Thickness(5);
                        border.Background = Helper.Colors.GetColorFromHex("#47abcf");
                        TextBlock textBlock = new TextBlock();
                        textBlock.TextWrapping = TextWrapping.Wrap;

                        textBlock.Foreground = Helper.Colors.WhiteColor();

                        textBlock.Text = n.Content;
                        border.Child = textBlock;
                        messages.Children.Add(border);
                    }
                }
                scrollViewer.ScrollToEnd();

            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Core.Message>();
                System.Windows.Forms.MessageBox.Show(error.Result == null ? "Greška!" : error.Result.Info);
            }
        }
        private void ScrollViewer_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateMessageDto createMessageDto = new CreateMessageDto();
                createMessageDto.UserFromId = APIService.MyId;
                createMessageDto.UserToId = CurrentUser;
                createMessageDto.Content = Message.Text;
                var data1 = await APIService.Post<Core.Message>($"Chat/create-message", createMessageDto);
                loadMessages();
                Message.Text = "";
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }



        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            InitTimer();
        }
    }
}
