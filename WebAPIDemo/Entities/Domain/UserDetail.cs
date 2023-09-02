using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebAPIDemo.Entities.Domain
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]

        public string UserEmail { get; set; }

        [Required]

        public Role Role { get; set; }
        public bool IsStudent { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }



    }

}