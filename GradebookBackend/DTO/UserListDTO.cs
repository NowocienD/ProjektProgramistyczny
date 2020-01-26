using System.Collections.Generic;

namespace GradebookBackend.DTO
{
    public class UserListDTO
    {
        public List<UserDTO> Users { get; set; }
        public UserListDTO()
        {
            Users = new List<UserDTO>();
        }
    }
}
