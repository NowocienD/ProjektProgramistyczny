using GradebookBackend.DTO;
using GradebookBackend.ServicesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradebookBackend.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public RoleController(IUserProviderService userProviderService, IUserService userService,
            IRoleService roleService)
        {
            this.userProviderService = userProviderService;
            this.userService = userService;
            this.roleService = roleService;
        }

        [Authorize]
        [HttpGet("admin/allroles")]
        public IActionResult GetRoles()
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                RoleListDTO roleListDTO = roleService.GetAllRoles();
                return Ok(roleListDTO);
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }
    }
}
