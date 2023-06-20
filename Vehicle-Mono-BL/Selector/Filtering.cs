using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Mono_BL.Selector
{
    public class Filtering
    {
        public int? MakeId { get; set; }
        public string Name { get; set; }

        public Filtering(int? makeId, string name)
        {
            MakeId = makeId;
            Name = name;
        }

    }
}