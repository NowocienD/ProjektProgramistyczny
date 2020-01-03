using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    //sluzy do dodawania nowego uzytkownika lub aktualizacji juz istniejacego
    public class NewUserDTO
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        //wypelnic tylko w przypadku studenta
        public int ClassId { get; set; }
    }
}
