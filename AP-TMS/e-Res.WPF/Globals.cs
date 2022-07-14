using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Res.WPF
{
    public static class Globals
    {
        public static string GetMonthName(int monthNumber)
        {
            switch (monthNumber)
            {
                case 1: return "Januar";
                case 2: return "Februar";
                case 3: return "Mart";
                case 4: return "April";
                case 5: return "Maj";
                case 6: return "Juni";
                case 7: return "Juli";
                case 8: return "August";
                case 9: return "Septembar";
                case 10: return "Oktobar";
                case 11: return "Novembar";
                case 12: return "Decembar";
                default: return "Nothing";
            }
        }
    }
}
