namespace GesRelationClient.Models
{
    public class Client
    {
        public int? ClientId { get; set; }
        public string? NomClient { get; set; }
        public string? PrenomClient { get; set; }
        public string? Adresse { get; set; }
        public string? CodePostal { get; set; }
        public string? Pays { get; set; }
        public DateTime DateNaissance { get; set; }
    }
}
