using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Entities.Domain;
using WebAPIDemo.Entities.DTO;
using WebAPIDemo.Repositories;

namespace WebAPIDemo.Services
{
    public class UserService
    {
        UserRepository<T> userdata = new UserRepository<T>();



        public ActionResult<Response<List<T>>> GetUserDetails()
        {
            return userdata.GetUserDetails();
        }

        public ActionResult<Response<AddUserDTO>> GetUserDetailsById(int userid)
        {
            try
            {
                if (userid < 0)
                {
                    return new Response<AddUserDTO>
                    {

                        ErrorMessage = "User ID cannot be Zero, Please enter valid user id"
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userdata.GetUserDetailsById(userid);
        }
        public ActionResult<Response<AddUserDTO>> AddUserDetails(AddUserDTO userdto)
        {
            if (userdto == null)
            {
                return new Response<AddUserDTO>
                {
                    ErrorMessage = "No Data Found"
                };
            }


            return userdata.AddUserDetails(userdto);
        }
        public ActionResult<Response<UserLogin>> LoginUser(UserLogin userlogin)
        {
            if (userlogin == null)
            {
                return new Response<UserLogin>
                {
                    ErrorMessage = "User credentials provided doesnot exist"
                };
            }

            return userdata.LoginUser(userlogin);


        }

    }
}