using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SearchObjects
{
    public class SearchObject:BaseSearchObject
    {
        public DateTime DateFrom { get; set; } 
        public DateTime DateTo { get; set; } 
    }
}
