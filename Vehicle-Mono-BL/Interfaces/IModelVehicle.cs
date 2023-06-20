using Vehicle_Mono_BL.Paged;
using Vehicle_Mono_BL.Selector;
using Vehicle_Mono_DAL.Interface;

namespace Vehicle_Mono_BL.Interface
{
    public interface IModelVehicle
    {

        Task<bool> Add(IModelV model);
        Task<bool> Delete(int id);
        Task<PaginatedList<IModelV>> GetAllModels(Paging paging, Sorting sorting, Filtering filtering);
        Task<IModelV> GetModelById(int id);
        Task<bool> Edit(IModelV make);
    }
}
