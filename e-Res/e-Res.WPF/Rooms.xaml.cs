using Core.Dto;
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
    /// Interaction logic for Rooms.xaml
    /// </summary>
    public partial class Rooms : UserControl
    {
        public PackIconKind Icon = PackIconKind.Edit; 
        public Rooms()
        {
            InitializeComponent();
            initialLoad();
        }

        private void initialLoad()
        {
            RoomsGetDto room = new RoomsGetDto
            {
               Description="Ovo je dvokrevetna soba",
               NumberOfRoom=202,
               Title="Dvokrevetna soba"
            };
            RoomsGetDto room1 = new RoomsGetDto
            {
                Description = "Ovo je dvokrevetna soba",
                NumberOfRoom = 203,
                Title = "Dvokrevetna soba"
            };
            List<RoomsGetDto> list = new List<RoomsGetDto>();
            list.Add(room);
            list.Add(room1);
            gridData.ItemsSource = list;
        }
    }
}
