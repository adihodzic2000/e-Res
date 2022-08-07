using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF.Enums;

namespace WPF.Dtos
{
    public class ItemMenu
    {
        public Categories Category { get; set; }
        public string Header { get; set; }
        public PackIconKind Icon { get; set; }
        public UserControl Screen { get; set; }

        public ItemMenu(Categories category, string header, PackIconKind icon)
        {
            Header = header;
            Icon = icon;
            Category = category;
        }

        public ItemMenu(Categories category, string header, UserControl screen, PackIconKind icon)
        {
            Header = header;
            Screen = screen;
            Icon = icon;
            Category = category;
        }

    }
}
