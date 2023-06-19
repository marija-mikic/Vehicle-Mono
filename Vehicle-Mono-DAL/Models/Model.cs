using Vehicle_Mono_DAL.Entity;
using Vehicle_Mono_DAL.Interface;

namespace Vehicle_Mono_DAL.Models
{
    public class Model : IModelV
    {


        public int ModelId { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public MakeEntity Make { get; set; }
    }
}
