using System.Data;
using WebAPIDemo.Entities.Domain;

namespace WebAPIDemo.Entities.DTO
{
    public class UserDetailsDTO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public Role Role { get; set; }
        public Boolean IsStudent { get; set; }

        public int UserId { get; set; }

    }
}