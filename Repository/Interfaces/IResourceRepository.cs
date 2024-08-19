using AuthResourceEX.Dtos;
using AuthResourceEX.Models;

namespace AuthResourceEX.Repository.Interfaces
{
    public interface IResourceRepository
    {
        (Resource? resource, string Message) Create(User user, ResourceDto resource);
        (bool IsSuccess, string Message) Delete(User user, int id);
        List<Resource> GetAll();
        Resource? GetById(int id);
        (Resource? Resource, string Message) Update(User user, int id, ResourceDto resource);
    }
}