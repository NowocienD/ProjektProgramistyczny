using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
