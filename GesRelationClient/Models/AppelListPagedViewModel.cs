namespace GesRelationClient.Models
{
    public class AppelListPagedViewModel
    {
        public IEnumerable<Appel> apples { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
