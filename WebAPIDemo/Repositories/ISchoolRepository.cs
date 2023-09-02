using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Entities.DTO;

namespace WebAPIDemo.Repositories
{
    public interface ISchoolRepository<T> where T : class
    {
        ActionResult<Response<List<T>>> GetUserDetails();
    }
}