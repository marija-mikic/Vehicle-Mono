using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Mono_BL.Paged;
using Vehicle_Mono_BL.Selector;
using Vehicle_Mono_DAL.Interface;

namespace Vehicle_Mono_BL.Interface
{
    public interface IMakeVehicle
    {
        Task<PaginatedList<IMake>> GetAll(Paging paging, Sorting sorting, string searchTerm);


        public Task<bool> Add(IMake make);
        Task<IMake> GetMakeById(int id);

        Task<bool> Edit(IMake make);
        Task<List<IMake>> GetAll();
         Task<bool> Remove(int id);
    }
}

