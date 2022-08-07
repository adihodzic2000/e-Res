using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF.Helper
{
    public static class Colors
    {

        public static Brush WhiteColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#ffffff");
            brush.Freeze();
            return brush;
        }
       
        
        public static Brush BlackColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#000000");
            brush.Freeze();
            return brush;
        }

        public static Brush GetColorFromHex(string color)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom(color);
            brush.Freeze();
            return brush;
        }
    }
}
