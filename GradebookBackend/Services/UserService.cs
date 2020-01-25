using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GradebookBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserDAO> usersRepository;
        private readonly IRepository<RoleDAO> rolesRepository;
        private readonly IRepository<StudentDAO> studentsRepository;
        private readonly IRepository<TeacherDAO> teachersRepository;
        private readonly IRepository<AdminDAO> adminsRepository;

        private readonly PasswordHasher passwordHasher;

        public UserService(IRepository<UserDAO> usersRepository, IRepository<RoleDAO> rolesRepository, 
            IRepository<StudentDAO> studentsRepository, IRepository<AdminDAO> adminsRepository,
            IRepository<TeacherDAO> teachersRepository)
        {
            this.usersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
            this.studentsRepository = studentsRepository;
            this.teachersRepository = teachersRepository;
            this.adminsRepository = adminsRepository;
            this.passwordHasher = new PasswordHasher();
        }

        public void AddUser(UserDTO newUserDTO)
        {
            if (!CheckIfNewUserLoginIsUnique(newUserDTO.Login))
            {
                throw new GradebookServerException("Nowy login uzytkownika nie jest unikalny");
            }
            UserDAO newUserDAO = new UserDAO
            {
                Login = newUserDTO.Login,
                Email = newUserDTO.Email,
                Firstname = newUserDTO.Firstname,
                Surname = newUserDTO.Surname,
                RoleId = newUserDTO.Role.Id,
                IsActive = newUserDTO.IsActive
            };
            newUserDAO.Password = passwordHasher.HashPassword(newUserDTO.Password);
            usersRepository.Add(newUserDAO);
            if (newUserDAO.RoleId == 1) studentsRepository.Add(new StudentDAO
            {
                UserId = GetUserIdByLoginAndPassword(newUserDAO.Login, newUserDTO.Password),
                ClassId = newUserDTO.ClassId
            });
            else if (newUserDAO.RoleId == 2)
            {
                teachersRepository.Add(new TeacherDAO
                {
                    UserId = GetUserIdByLoginAndPassword(newUserDAO.Login, newUserDTO.Password)
                });
            }
            else if (newUserDAO.RoleId == 3)
            {
                adminsRepository.Add(new AdminDAO
                {
                    UserId = GetUserIdByLoginAndPassword(newUserDAO.Login, newUserDTO.Password)
                });
            }
            else
            {
                throw new GradebookServerException("Nie poprawna roleId");
            }
        }

        public void UpdateUser(UserDTO updatedUserDTO, int userId)
        {
            if (!CheckIfUpdatedUserLoginIsUnique(updatedUserDTO.Login, userId))
            {
                throw new GradebookServerException("Zaktualizowany login uzytkownika nie jest unikalny");
            }
            UserDAO updatedUserDAO = new UserDAO
            {
                Id = userId,
                Login = updatedUserDTO.Login,
                Email = updatedUserDTO.Email,
                Firstname = updatedUserDTO.Firstname,
                Surname = updatedUserDTO.Surname,
                RoleId = updatedUserDTO.Role.Id,
                IsActive = updatedUserDTO.IsActive
            };
            if (updatedUserDTO.Password != null)
            {
                updatedUserDAO.Password = passwordHasher.HashPassword(updatedUserDTO.Password);
            }
            else
            {
                updatedUserDAO.Password = usersRepository.Get(userId).Password;
            }
            usersRepository.Update(updatedUserDAO);

            if(IsStudent(userId))
            {
                int studentId = GetStudentIdByUserId(userId);
                StudentDAO updatedStudentDAO = new StudentDAO
                {
                    Id = studentId,
                    UserId = userId,
                    ClassId = updatedUserDTO.ClassId
                };
                studentsRepository.Update(updatedStudentDAO);
            }
        }

        public void DeactivateUser(int userId)
        {
            UserDAO userDAO = usersRepository.Get(userId);
            if (userDAO.IsActive)
            {
                userDAO.IsActive = false;
                usersRepository.Update(userDAO);
            }
            else
            {
                throw new GradebookServerException("Niepowodzenie, uzytkownik jest juz deazaktywowany");
            }
        }

        public void UpdateUserPassword(UserPasswordChangeDTO userPasswordChangeDTO, int userId)
        {
            UserDAO userDAO = usersRepository.Get(userId);
            if(passwordHasher.VerifyHashedPassword(userDAO.Password, userPasswordChangeDTO.OldPassword) == Microsoft.AspNet.Identity.PasswordVerificationResult.Success)
            {
                userDAO.Password = passwordHasher.HashPassword(userPasswordChangeDTO.NewPassword);
            }
            else
            {
                throw new GradebookServerException("Poprzednie haslo nie jest poprawne");
            }
            usersRepository.Update(userDAO);
        }

        public bool CheckIfNewUserLoginIsUnique(string newUserLogin)
        {
            IEnumerable<UserDAO> users = usersRepository.GetAll();
            foreach (UserDAO user in users)
            {
                if (user.Login == newUserLogin)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckIfUpdatedUserLoginIsUnique(string updatedUserLogin, int userId)
        {
            if (usersRepository.Get(userId).Login == updatedUserLogin) return true;
            IEnumerable<UserDAO> users = usersRepository.GetAll();
            foreach (UserDAO user in users)
            {
                if (user.Login == updatedUserLogin)
                {
                    return false;
                }
            }
            return true;
        }

        public UserDataDTO GetUserDataByUserId(int userId)
        {
            UserDAO userDAO = usersRepository.Get(userId);
            UserDataDTO userDataDTO = new UserDataDTO
            {
                Firstname = userDAO.Firstname,
                Surname = userDAO.Surname,
                Role = rolesRepository.Get(userDAO.RoleId).Name
            };
            return userDataDTO;
        }

        public UserDTO GetUserByUserId(int userId)
        {
            UserDAO userDAO = usersRepository.Get(userId);
            UserDTO userDTO = new UserDTO
            {
                Id = userDAO.Id,
                Firstname = userDAO.Firstname,
                Surname = userDAO.Surname,
                Login = userDAO.Login,
                Email = userDAO.Email,
                IsActive = userDAO.IsActive,
                Role = new RoleDTO
                {
                    Id = rolesRepository.Get(userDAO.RoleId).Id,
                    Name = rolesRepository.Get(userDAO.RoleId).Name
                }
            };
            return userDTO;
        }

        public int GetUserIdByLoginAndPassword(string login, string password)
        {
            IEnumerable<UserDAO> users = usersRepository.GetAll();
            foreach(UserDAO user in users)
            {
                if(user.Login == login)
                {
                    if (user.IsActive == true)
                    {
                        if (passwordHasher.VerifyHashedPassword(user.Password, password) == Microsoft.AspNet.Identity.PasswordVerificationResult.Success)
                        {
                            return user.Id;
                        }
                        else if (passwordHasher.VerifyHashedPassword(user.Password, password) == Microsoft.AspNet.Identity.PasswordVerificationResult.SuccessRehashNeeded)
                        {
                            throw new GradebookServerException("Haslo powinno byc rehashowane");
                        }
                        else
                        {
                            throw new GradebookServerException("Niepoprawne haslo");
                        }
                    }
                    else
                    {
                        throw new GradebookServerException("Uzytkownik o tym loginie nie jest aktywny");
                    }
                }
            }
            throw new GradebookServerException("Niepoprawny login");
        }

        public int GetStudentIdByUserId(int userId)
        {
            IEnumerable<StudentDAO> students = studentsRepository.GetAll();
            foreach (StudentDAO student in students)
            {
                if (student.UserId == userId)
                {
                    return student.Id;
                }
            }
            throw new GradebookServerException("Student o tym userId nie istnieje");
        }

        public int GetTeacherIdByUserId(int userId)
        {
            IEnumerable<TeacherDAO> teachers = teachersRepository.GetAll();
            foreach (TeacherDAO teacher in teachers)
            {
                if (teacher.UserId == userId)
                {
                    return teacher.Id;
                }
            }
            throw new GradebookServerException("Nauczyciel o tym userId nie istnieje");
        }

        public bool IsStudent(int userId)
        {
            IEnumerable<StudentDAO> listOfStudents = studentsRepository.GetAll();
            foreach (StudentDAO student in listOfStudents)
            {
                if (student.UserId == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsAdmin(int userId)
        {
            IEnumerable<AdminDAO> listOfAdmins = adminsRepository.GetAll();
            foreach(AdminDAO admin in listOfAdmins)
            {
                if(admin.UserId == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsTeacher(int userId)
        {
            IEnumerable<TeacherDAO> teachers = teachersRepository.GetAll();
            foreach (TeacherDAO teacher in teachers)
            {
                if (teacher.UserId == userId)
                {
                    return true;
                }
            }
            return false;
        }
        public UserListDTO GetAllUsers()
        {
            UserListDTO userDataListDTO = new UserListDTO();
            IEnumerable<UserDAO> users = usersRepository.GetAll();
            foreach (UserDAO user in users)
            {
                userDataListDTO.Users.Add(new UserDTO
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Surname = user.Surname,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    Login = user.Login,
                    Role = new RoleDTO
                    {
                        Id = user.RoleId,
                        Name = rolesRepository.Get(user.RoleId).Name
                    }
                });
            }
            return userDataListDTO;
        }
    }
}