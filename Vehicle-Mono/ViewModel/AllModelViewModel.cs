using Vehicle_Mono_BL.Paged;
using Vehicle_Mono_DAL.Interface;

namespace Vehicle_Mono.ViewModel
{
    public class AllModelViewModel
    {
        public int SelectedIdMake { get; set; }
        public List<IMake>? Makes { get; set; }
        public PaginatedList<IModelV>? Models { get; set; }

        public AllModelViewModel(PaginatedList<IModelV>? models, List<IMake>? makes)
        {
            Models = models;
            Makes = makes;
            SelectedIdMake = 0;
        }
    }
}
