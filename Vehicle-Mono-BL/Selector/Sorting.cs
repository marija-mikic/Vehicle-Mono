using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Mono_BL.Selector
{
    public class Sorting
    {
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }

        public Sorting(string sortBy, bool sortAscending)
        {
            SortBy = sortBy;
            SortAscending = sortAscending;
        }
    }
}
