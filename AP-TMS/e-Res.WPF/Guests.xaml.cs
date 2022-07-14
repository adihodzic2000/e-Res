using Core.Dto;
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

namespace e_Res.WPF
{
    /// <summary>
    /// Interaction logic for Guests.xaml
    /// </summary>
    public partial class Guests : UserControl
    {
        public Guests()
        {
            InitializeComponent();
            initialLoad();
        }

        private void initialLoad()
        {
            GuestsGetDto guest = new GuestsGetDto
            {
               Email="adihodzic94@gmail.com",
               FName="Adi",
               LName="Hodzic",
               PhoneNumber="062610068"
            };
            GuestsGetDto guest1 = new GuestsGetDto
            {
                Email = "adihodzic94@gmail.com",
                FName = "Adi",
                LName = "Hodzic",
                PhoneNumber = "062610068"
            };
            List<GuestsGetDto> list = new List<GuestsGetDto>();
            list.Add(guest);
            list.Add(guest1);
            gridData.ItemsSource = list;
        }
    }
}
