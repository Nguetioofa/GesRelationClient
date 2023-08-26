namespace GesRelationClient.Models
{
    public class ClientListPagedViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

}
