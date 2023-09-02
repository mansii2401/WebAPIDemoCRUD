using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPIDemo.Entities.Domain;
using WebAPIDemo.Entities.DTO;

namespace WebAPIDemo.Repositories
{
    public class UserRepository<T> : ISchoolRepository<T> where T : class
    {

        public List<T> ReadUsers(string path)
        {
            string ReadAllUsers = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(ReadAllUsers);

        }
        public void SaveUser(string path, List<T> ReadAllUsers)
        {
            string SaveUser = JsonSerializer.Serialize(ReadAllUsers);
            File.WriteAllText(path, SaveUser);
        }

        public ActionResult<Response<List<T>>> GetUserDetails()
        {

            string ReadAllUsers = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserEntry.json");
            var user = JsonSerializer.Deserialize<List<User>>(ReadAllUsers);
            try
            {
                if (user.Count == 0)
                {
                    return new Response<List<T>>
                    {

                        ErrorMessage = "No users present!."
                    };
                }
                else
                {
                    string ReadAllUserDetails = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserDetail.json");
                    var userdetails = JsonSerializer.Deserialize<List<UserDetails>>(ReadAllUserDetails);
                    if (userdetails.Count == 0)
                    {
                        return new Response<List<T>>
                        {
                            ErrorMessage = "No users data Found."
                        };
                    }
                    else
                    {


                        var result = (from item in user
                                      join itemdetails in userdetails on item.UserId equals itemdetails.UserId
                                      select new UserDetailsDTO()
                                      {
                                          UserId = item.UserId,
                                          UserName = item.UserName,
                                          FirstName = itemdetails.FirstName,
                                          LastName = itemdetails.LastName,
                                          UserEmail = itemdetails.UserEmail,
                                          IsStudent = itemdetails.IsStudent,
                                          Role = itemdetails.Role
                                      }).ToList();
                        if (result.Count > 0)
                        {

                            return new Response<List<T>>
                            {
                                //Result = result,
                                StatusMessage = "Ok"
                            };
                        }

                        else
                        {
                            return new Response<List<T>>
                            {
                                StatusMessage = "No Matching Records found"
                            };


                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public ActionResult<Response<AddUserDTO>> AddUserDetails(AddUserDTO userdto)
        {
            string ReadAllUser = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserEntry.json");
            var UserUpdated = JsonSerializer.Deserialize<List<User>>(ReadAllUser);

            var usercheck = (from e in UserUpdated where e.UserName.Equals(userdto.UserName) select e).Count();
            try
            {
                if (usercheck > 0)
                    return new Response<AddUserDTO>
                    {

                        StatusMessage = "User already exists"
                    };
                var maxIduser = (from e in UserUpdated orderby e.UserId descending select e.UserId).FirstOrDefault();
                var IdUser = maxIduser + 1;

                var adduser = new User()
                {
                    UserId = IdUser,
                    UserName = userdto.UserName,
                    Password = userdto.Password,
                };


                UserUpdated.Add(adduser);

                string json = JsonSerializer.Serialize(UserUpdated);
                File.WriteAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserEntry.json", json);

                string ReadAllUser1 = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserDetail.json");
                var UserUpdated1 = JsonSerializer.Deserialize<List<UserDetails>>(ReadAllUser1);

                var maxId = (from e in UserUpdated1 orderby e.Id descending select e.Id).FirstOrDefault();
                var IdEntry = maxId + 1;

                var adduser2 = new UserDetails()
                {
                    Id = IdEntry,
                    UserId = IdUser,

                    FirstName = userdto.FirstName,
                    LastName = userdto.LastName,
                    UserEmail = userdto.UserEmail,
                    IsStudent = userdto.IsStudent,
                    Role = userdto.Role,
                };


                UserUpdated1.Add(adduser2);
                string json1 = JsonSerializer.Serialize(UserUpdated1);
                File.WriteAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserDetail.json", json1);

                return new Response<AddUserDTO>
                {
                    Result = userdto,
                    StatusMessage = "Data has been added successfully!."
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult<Response<AddUserDTO>> GetUserDetailsById(int userid)
        {


            string userdetails = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserEntry.json");
            var users = JsonSerializer.Deserialize<List<User>>(userdetails);
            try
            {
                if (users.Count == 0)
                {
                    return new Response<AddUserDTO>
                    {
                        StatusMessage = "No Users present"
                    };
                }
                else
                {
                    string Details = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserDetail.json");
                    var userdetails1 = JsonSerializer.Deserialize<List<UserDetails>>(Details);
                    if (userdetails1 != null)
                    {

                        var result = (from item in users
                                      join itemDetail in userdetails1 on item.UserId equals itemDetail.UserId

                                      select new AddUserDTO()
                                      {
                                          UserName = item.UserName,
                                          Password = item.Password,
                                          FirstName = itemDetail.FirstName,
                                          LastName = itemDetail.LastName,
                                          UserEmail = itemDetail.UserEmail,
                                          Role = itemDetail.Role,
                                          IsStudent = itemDetail.IsStudent,
                                      }).FirstOrDefault();
                        if (result != null)
                        {
                            return new Response<AddUserDTO>
                            {
                                Result = result,
                                StatusMessage = "Ok"
                            };
                        }
                        else
                        {
                            return new Response<AddUserDTO>
                            {
                                StatusMessage = "No Data found"
                            };
                        }
                    }
                    else
                    {
                        return new Response<AddUserDTO>
                        {
                            StatusMessage = "No Data found"
                        };
                    }
                }
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }




        public ActionResult<Response<UserLogin>> LoginUser(UserLogin userlogin)
        {
            string ReadAllUser = File.ReadAllText(@"C:\Users\parom\source\repos\Abhishek\Abhishek\JsonData\UserEntry.json");
            var UserUpdated = JsonSerializer.Deserialize<List<User>>(ReadAllUser);

            var usercheck = (from e in UserUpdated where e.UserName.Equals(userlogin.UserName) select e).Count();
            if (usercheck > 0)
            {
                return new Response<UserLogin>
                {
                    StatusMessage = "User login successfull"
                };
            }
            else
            {

                return new Response<UserLogin>
                {
                    StatusMessage = "User credentials does not exist"
                };
            }
        }


    }
}