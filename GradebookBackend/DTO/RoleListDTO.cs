using System.Collections.Generic;

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
