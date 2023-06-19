using Vehicle_Mono_DAL.Entity;

namespace Vehicle_Mono_DAL.Interface
{
    public interface IModelV
    {

        int ModelId { get; set; }
        int MakeId { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        MakeEntity Make { get; set; }
    }
}