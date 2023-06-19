using Vehicle_Mono_DAL.Entity;
using Vehicle_Mono_DAL.Interface;

namespace Vehicle_Mono_DAL.MODELS
{
    public class Makes : IMake
    {
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public ICollection<ModelEntity> Models { get; set; }
    }
}
