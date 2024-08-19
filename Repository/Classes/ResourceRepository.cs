using System.Reflection;
using AuthResourceEX.Data;
using AuthResourceEX.Dtos;
using AuthResourceEX.Enums;
using AuthResourceEX.Mappers;
using AuthResourceEX.Models;
using AuthResourceEX.Repository.Interfaces;

namespace AuthResourceEX.Repository.Classes
{
    public class ResourceRepository : IResourceRepository
    {
        public (Resource? resource, string Message) Create(User user, ResourceDto resourceDto)
        {
            var resource = new Resource();
            if (user.Role == Role.ADMIN ||
                user.Role == Role.SUPERADMIN)
            {
                resource = resourceDto.MapToResource(Resources.ResourcesList);
                Resources.ResourcesList.Add(resource);
                return (resource, "Resource Created!");
            }
            return (null, "Failed to create resource!");
        }

        public List<Resource> GetAll()
        {
            return Resources.ResourcesList;
        }

        public Resource? GetById(int id)
        {
            var resource = Resources.ResourcesList.FirstOrDefault(x => x.Id == id);
            if (resource == null)
            {
                return null;
            }
            return resource;
        }

        public (Resource? Resource, string Message) Update(User user, int id, ResourceDto resource)
        {
            if (user.Role != Role.ADMIN && user.Role != Role.SUPERADMIN)
            {
                return (null, "Insufficient permissions!");
            }
            var resourceWithId = Resources.ResourcesList.FirstOrDefault(x => x.Id == id);
            if (resourceWithId is null)
            {
                return (null, "Resource not founed!");
            }
            if(string.IsNullOrEmpty(resource.Name) && string.IsNullOrEmpty(resource.Description))
            {
                return (null, "Cannot be empty!");
            }
            resourceWithId.Name = resource.Name;
            resourceWithId.Description = resource.Description;
            resourceWithId.UpdatedAt = DateTime.Now;
            return (resourceWithId, "Updated Resource!");
        }

        public (bool IsSuccess, string Message) Delete(User user, int id)
        {
            var currentResourceCount = Resources.ResourcesList.Count;
            if (user.Role != Role.ADMIN && user.Role != Role.SUPERADMIN)
            {
                return (false, "Insufficient permissions!");
            }
            var resourceWithId = Resources.ResourcesList.FirstOrDefault(x => x.Id == id);
            if (resourceWithId == null)
            {
                return (false, "Resource does not exist!");
            }
            Resources.ResourcesList.Remove(resourceWithId);
            var success = currentResourceCount > Resources.ResourcesList.Count;
            if (success is false)
            {
                return (false, "Failed to delete Resource!");
            }
            return (false, "Deleted Resource!");
        }
    }
}
