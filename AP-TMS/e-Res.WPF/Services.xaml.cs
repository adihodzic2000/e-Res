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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace e_Res.WPF
{
    /// <summary>
    /// Interaction logic for Services.xaml
    /// </summary>
    public partial class Services : UserControl
    {
        public Services()
        {
            InitializeComponent();
            initialLoad();
        }

        private void initialLoad()
        {
            ServicesGetDto service = new ServicesGetDto
            {
                Price=20.00,
                Title="Doručak"
            };
            ServicesGetDto service1 = new ServicesGetDto
            {
                Price = 30.00,
                Title = "Ručak"
            };
            List<ServicesGetDto> list = new List<ServicesGetDto>();
            list.Add(service);
            list.Add(service1);
            gridData.ItemsSource = list;
        }
    }
}
