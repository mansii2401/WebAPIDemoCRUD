using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Entities.Domain;
using WebAPIDemo.Entities.DTO;
using WebAPIDemo.Repositories;

namespace WebAPIDemo.Services
{
    public class UserService
    {
        UserData userdata = new UserData();

        public List<User> GetAllUsers()
        {
            return userdata.GetAllUsers();
        }

        public ActionResult<Response<List<UserDetailDTO>>> GetUserDetails()
        {
            return userdata.GetUserDetails();
        }

        public ActionResult<Response<AddUserDTO>> GetUserDetailsById(int userid)
        {
            if (userid < 0)
            {
                return new Response<AddUserDTO>
                {

                    ErrorMessage = "User ID cannot be Zero, Please enter valid user id"
                };
            }

            return userdata.GetUserDetailsById(userid);
        }
        public ActionResult<Response<AddUserDTO>> AddUser(AddUserDTO userdto)
        {
            if (userdto == null)
            {
                return new Response<AddUserDTO>
                {
                    ErrorMessage = "No Data Found"
                };
            }


            return userdata.AddUser(userdto);
        }




    }
}
