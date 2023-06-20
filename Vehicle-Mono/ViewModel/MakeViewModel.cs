namespace Vehicle_Mono.ViewModel
{
    public class MakeViewModel
    {
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public MakeViewModel()
        {
            MakeId = 0;
            Name = string.Empty;
            Abrv = string.Empty;

        }
    }
}
