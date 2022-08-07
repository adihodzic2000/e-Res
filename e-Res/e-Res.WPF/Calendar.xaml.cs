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
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : UserControl
    {
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public int CurrentMonth { get; set;} = DateTime.Now.Month;

        public Calendar()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            displayDays();
            lblDate.Content = $"{CurrentYear}, {Globals.GetMonthName(CurrentMonth)}";
        }

    

        private void displayDays()
        {
            DateTime now = new DateTime(CurrentYear, CurrentMonth,1);
           

            DateTime MonthStartDay = new DateTime(now.Year, now.Month, 1);

            int daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);

            int dayOfTheWeek = Convert.ToInt32(MonthStartDay.DayOfWeek.ToString("d")) -1;
            if(dayOfTheWeek<0)
                dayOfTheWeek = 7+dayOfTheWeek;
            int column = 0;
            int row = 0;
            column = dayOfTheWeek;
            GridDays.Children.Clear();
            for (int i = dayOfTheWeek+1; i < daysInMonth + dayOfTheWeek+1; i++)
            {
                BrushConverter br1 = new BrushConverter();
                Brush brush1 = br1.ConvertFrom("#418bb0") as Brush;
                brush1.Freeze();

                BrushConverter br2 = new BrushConverter();
                Brush brush2 = br2.ConvertFrom("#ffffff") as Brush;
                brush2.Freeze();


                Day day = new Day(i.ToString());
                Border border = new Border();
                border.CornerRadius = new CornerRadius(15);
                border.Background = brush1;
                border.Margin= new Thickness(5);
                StackPanel stackPanel = new StackPanel();
                
                TextBlock textBlock = new TextBlock();
                textBlock.Text = (i-dayOfTheWeek).ToString();
                textBlock.FontSize = 20;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Top;
                textBlock.Foreground = brush2;

                TextBlock reservation = new TextBlock();
                textBlock.Text = (i - dayOfTheWeek).ToString();
                textBlock.FontSize = 20;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                BrushConverter br = new BrushConverter();
                Brush brush = br.ConvertFrom("#f70213") as Brush;
                brush.Freeze();
                reservation.Background = brush;
                
                stackPanel.Children.Add(textBlock);
                stackPanel.Children.Add(reservation);
               border.Child=(stackPanel);    
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
            if (CurrentMonth <1)
            {
                CurrentYear--;
                CurrentMonth = 12;
            }
            displayDays();
        }
    }
}
