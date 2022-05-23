using System.ComponentModel.DataAnnotations;

namespace CRUD.WebAPI.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
