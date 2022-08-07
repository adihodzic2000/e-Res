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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace e_Res.WPF
{
    /// <summary>
    /// Interaction logic for Bills.xaml
    /// </summary>
    public partial class Bills : System.Windows.Controls.UserControl
    {
        public Bills()
        {
            InitializeComponent();
            initialLoad();
        }

        private void initialLoad()
        {
            //DataGridViewButtonColumn btns
            BillsGetDto bills = new BillsGetDto
            {
               TotalAmount=20,
               FLName="Adi Hodzic",
               IsPaid=false,
               NumberOfNights=10,
               Price=15,
               PriceOfServices=20
            };
            BillsGetDto bills1 = new BillsGetDto
            {
                TotalAmount = 20,
                FLName = "Adi Hodzic",
                IsPaid = false,
                NumberOfNights = 10,
                Price = 15,
                PriceOfServices = 20
            };
            List<BillsGetDto> list = new List<BillsGetDto>();
            list.Add(bills);
            list.Add(bills1);
            gridData.ItemsSource = list;
        }
    }
}
