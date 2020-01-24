using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class UserDataListDTO
    {
        public List<UserDataDTO> Users { get; set; }
        public UserDataListDTO()
        {
            Users = new List<UserDataDTO>();
        }
    }
}
