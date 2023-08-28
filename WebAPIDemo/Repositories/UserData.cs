using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPIDemo.Entities.Domain;
using WebAPIDemo.Entities.DTO;

namespace WebAPIDemo.Repositories
{
        public class UserData
        {


            public List<User> GetAllUsers()
            {
                string ReadAllUsers = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserEntry.json");
                return JsonSerializer.Deserialize<List<User>>(ReadAllUsers);
            }

            public ActionResult<Response<List<UserDetailDTO>>> GetUserDetails()
            {
                string ReadAllUsers = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserEntry.json");
                var user = JsonSerializer.Deserialize<List<User>>(ReadAllUsers);
                if (user.Count == 0)
                {
                    return new Response<List<UserDetailDTO>>
                    {
                        StatusMessage = "No users present!."
                    };
                }
                else
                {
                    string ReadAllUserDetails = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserDetail.json");
                    var userdetails = JsonSerializer.Deserialize<List<UserDetail>>(ReadAllUserDetails);
                    if (userdetails.Count == 0)
                    {
                        return new Response<List<UserDetailDTO>>
                        {
                            StatusMessage = "No users data Found."
                        };
                    }
                    else
                    {


                        var result = (from item in user
                                      join itemdetails in userdetails on item.UserId equals itemdetails.UserId
                                      select new UserDetailDTO()
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
                            return new Response<List<UserDetailDTO>>
                            {
                                Result = result,
                                StatusMessage = "Ok"
                            };
                        }

                        else
                        {
                            return new Response<List<UserDetailDTO>>
                            {
                                StatusMessage = "No Matching Records found"
                            };


                        }
                    }
                }
            }



            public ActionResult<Response<AddUserDTO>> AddUser(AddUserDTO userdto)
            {
                string ReadAllUser = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserEntry.json");
                var UserUpdated = JsonSerializer.Deserialize<List<User>>(ReadAllUser);

                var usercheck = (from e in UserUpdated where e.UserName.Equals(userdto.UserName) select e).Count();
                if (usercheck > 0)
                    return new Response<AddUserDTO>
                    {

                        StatusMessage = "User already exists"
                    };
                var maxIduser = (from e in UserUpdated orderby e.UserId descending select e.UserId).FirstOrDefault();
                userdto.UserId = maxIduser + 1;

                var adduser = new User()
                {
                    UserId = userdto.UserId,
                    UserName = userdto.UserName,
                    Password = userdto.Password,
                };


                UserUpdated.Add(adduser);

                string json = JsonSerializer.Serialize(UserUpdated);
                File.WriteAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserEntry.json", json);

                string ReadAllUser1 = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserDetail.json");
                var UserUpdated1 = JsonSerializer.Deserialize<List<UserDetail>>(ReadAllUser1);

                var maxId = (from e in UserUpdated1 orderby e.Id descending select e.Id).FirstOrDefault();
                userdto.Id = maxId + 1;

                var adduser2 = new UserDetail()
                {
                    Id = userdto.Id,
                    UserId = userdto.UserId,

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
                ;
            }



            public ActionResult<Response<AddUserDTO>> GetUserDetailsById(int userid)
            {


                string userdetails = File.ReadAllText(@"C:\Users\mansi\source\repos\WebAPIDemo\WebAPIDemo\JsonData\UserEntry.json");
                var users = JsonSerializer.Deserialize<List<User>>(userdetails);
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
                    var userdetails1 = JsonSerializer.Deserialize<List<UserDetail>>(Details);
                    if (userdetails1 != null)
                    {

                        var result = (from item in users
                                      join itemDetail in userdetails1 on item.UserId equals itemDetail.UserId

                                      select new AddUserDTO()
                                      {
                                          UserName = item.UserName,
                                          Password = item.Password,
                                          UserId = item.UserId,
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

        }
    }

