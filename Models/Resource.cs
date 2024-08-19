namespace AuthResourceEX.Models
{
    public class Resource
    {
        public int Id { get; set; }           // Unique identifier for the resource
        public string Name { get; set; }      // Name of the resource
        public string Description { get; set; } // Description of the resource
        public DateTime CreatedAt { get; set; } = DateTime.Now;// Creation timestamp
        public DateTime UpdatedAt { get; set; } = DateTime.Now;// Last update timestamp
    }
}
