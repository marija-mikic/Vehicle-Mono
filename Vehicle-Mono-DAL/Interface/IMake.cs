using Vehicle_Mono_DAL.Entity;

namespace Vehicle_Mono_DAL.Interface
{
    public interface IMake
    {
        int MakeId { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }

        ICollection<ModelEntity> Models { get; set; }
    }
}