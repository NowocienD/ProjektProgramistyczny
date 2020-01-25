using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class RoleListDTO
    {
        public List<RoleDTO> Roles { get; set; }
        public RoleListDTO()
        {
            Roles = new List<RoleDTO>();
        }
    }
}
