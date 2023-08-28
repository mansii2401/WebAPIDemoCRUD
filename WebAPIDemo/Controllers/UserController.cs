using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Entities.Domain;
using WebAPIDemo.Entities.DTO;
using WebAPIDemo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIDemo.Controllers
{
    
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserService userService = new UserService();


        [HttpGet]
        public List<User> GetAllUsers()
        {
            return userService.GetAllUsers();
        }
        [HttpGet("UserDetails/{id}")]
        public ActionResult<Response<AddUserDTO>> GetUserDetailsById(int userid)
        {
            return userService.GetUserDetailsById(userid);
        }
        [HttpPost]
        public ActionResult<Response<AddUserDTO>> AddUser(AddUserDTO userdto)
        {
            return userService.AddUser(userdto);
        }

        [HttpGet("UserDetails")]
        public ActionResult<Response<List<UserDetailDTO>>> GetUserDetails()
        {
            return userService.GetUserDetails();
        }

    }

}