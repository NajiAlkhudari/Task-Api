using TaskAlbayan.DB;

namespace TaskAlbayan.Common.Dtos
{
    public class CreateUpdateUserDto
    {
            public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoles Role { get; set; } 
    }
}