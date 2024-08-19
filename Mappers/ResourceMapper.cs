using AuthResourceEX.Data;
using AuthResourceEX.Dtos;
using AuthResourceEX.Models;

namespace AuthResourceEX.Mappers
{
    public static class ResourceMapper
    {
        public static Resource? MapToResource(this ResourceDto resourceDto, List<Resource> resourceList)
        {
            return new Resource
            {
                Id = resourceList.Count + 1,
                Name = resourceDto.Name,
                Description = resourceDto.Description,
            };
        }
    }
}
