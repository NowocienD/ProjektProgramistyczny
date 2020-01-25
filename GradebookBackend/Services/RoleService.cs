using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<RoleDAO> rolesRepository;

        public RoleService(IRepository<RoleDAO> rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public RoleListDTO GetAllRoles()
        {
            RoleListDTO roleListDTO = new RoleListDTO();
            IEnumerable<RoleDAO> roles = rolesRepository.GetAll();
            foreach(RoleDAO role in roles)
            {
                roleListDTO.Roles.Add(new RoleDTO
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }
            return roleListDTO;
        }

    }
}
