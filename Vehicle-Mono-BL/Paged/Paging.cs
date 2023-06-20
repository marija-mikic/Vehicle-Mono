using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Mono_BL.Paged
{
    public class Paging
    {


        public int maxPageSize = 5;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 3;


        public Paging(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        //public int PageSize
        //{
        //    get
        //    {
        //        return _pageSize;
        //    }
        //    set
        //    {
        //        _pageSize = (value > maxPageSize) ? maxPageSize : value;
        //    }
    }

}

