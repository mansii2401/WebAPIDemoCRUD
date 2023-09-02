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



        [HttpGet("UserDetails/id")]
        public ActionResult<Response<AddUserDTO>> GetUserDetailsById(int userid)
        {
            return userService.GetUserDetailsById(userid);
        }
        [HttpPost("UserDetails")]
        public ActionResult<Response<AddUserDTO>> AddUserDetails(AddUserDTO userdto)
        {
            return userService.AddUserDetails(userdto);


        }

        [HttpGet("UserDetails")]
        public ActionResult<Response<List<T>>> GetUserDetails()
        {
            return userService.GetUserDetails();
        }
        [HttpPost("UserLogin")]
        public ActionResult<Response<UserLogin>> LoginUser(UserLogin userlogin)
        {
            return userService.LoginUser(userlogin);

        }

    }
}

