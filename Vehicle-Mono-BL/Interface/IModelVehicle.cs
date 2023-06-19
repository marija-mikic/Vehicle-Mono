using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Mono_DAL.Interface;

namespace Vehicle_Mono_BL.Interface
{
    internal interface IModelVehicle
    {

        Task<bool> Add(IModelV model);
        Task<bool> Delete(int id);
        Task<PaginatedList<IModelV>> GetAllModels(Paging paging, Sorting sorting, Filtering filtering);
        Task<IModelV> GetModelById(int id);
        Task<bool> Edit(IModelV make);
    }
}
