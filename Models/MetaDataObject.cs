namespace ContosoPizza.Models
{
    public class MetaDataObject
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
