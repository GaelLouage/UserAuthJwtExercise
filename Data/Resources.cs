using System.Reflection.Metadata.Ecma335;
using AuthResourceEX.Models;

namespace AuthResourceEX.Data
{
    public static class Resources
    {
        public static List<Resource>  ResourcesList = new List<Resource>
        {
            new Resource
            {
                Id = 1,
                Name = "Resource 1",
                Description = "This is the first resource.",
                CreatedAt = DateTime.UtcNow.AddDays(-10), // Example creation date
                UpdatedAt = DateTime.UtcNow.AddDays(-5)  // Example update date
            },
            new Resource
            {
                Id = 2,
                Name = "Resource 2",
                Description = "This is the second resource.",
                CreatedAt = DateTime.UtcNow.AddDays(-7),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new Resource
            {
                Id = 3,
                Name = "Resource 3",
                Description = "This is the third resource.",
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            }
        };
    }
}
