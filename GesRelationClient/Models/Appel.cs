namespace GesRelationClient.Models
{
    public class Appel
    {
        public int? AppelId { get; set; }
        public int? ClientId { get; set; }
        public DateTime DateAppel { get; set; }
        public string? Objet { get; set; }
        public string? Description { get; set; }
    }
}
