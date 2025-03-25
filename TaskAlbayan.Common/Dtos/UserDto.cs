using TaskAlbayan.DB;

namespace TaskAlbayan.Common.Dtos
{
    public class UserDto
    {
             public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRoles Role { get; set; } 
    }
}