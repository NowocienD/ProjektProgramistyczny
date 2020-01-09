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

        public void AddUser(NewUserDTO newUserDTO)
        {
            if (!CheckIfNewUserLoginIsUnique(newUserDTO.Login))
            {
                throw new GradebookServerException("New user login is not unique");
            }
            UserDAO newUserDAO = new UserDAO
            {
                Login = newUserDTO.Login,
                Email = newUserDTO.Email,
                Firstname = newUserDTO.Firstname,
                Surname = newUserDTO.Surname,
                RoleId = newUserDTO.RoleId,
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
                throw new GradebookServerException("Not valid roleId");
            }
        }

        public void UpdateUser(NewUserDTO updatedUserDTO, int userId)
        {
            if (!CheckIfUpdatedUserLoginIsUnique(updatedUserDTO.Login, userId))
            {
                throw new GradebookServerException("Updated user login is not unique");
            }
            UserDAO updatedUserDAO = new UserDAO
            {
                Id = userId,
                Login = updatedUserDTO.Login,
                Email = updatedUserDTO.Email,
                Firstname = updatedUserDTO.Firstname,
                Surname = updatedUserDTO.Surname,
                RoleId = updatedUserDTO.RoleId,
                IsActive = updatedUserDTO.IsActive
            };
            updatedUserDAO.Password = passwordHasher.HashPassword(updatedUserDTO.Password);
            usersRepository.Update(updatedUserDAO);
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
                throw new GradebookServerException("Old password is not valid");
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
            UserDataDTO userDataDTO = new UserDataDTO();
            userDataDTO.Firstname = usersRepository.Get(userId).Firstname;
            userDataDTO.Surname = usersRepository.Get(userId).Surname;
            userDataDTO.Role = rolesRepository.Get(usersRepository.Get(userId).RoleId).Name;

            return userDataDTO;
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
                            throw new GradebookServerException("Password should be rehash");
                        }
                        else
                        {
                            throw new GradebookServerException("Wrong password");
                        }
                    }
                    else
                    {
                        throw new GradebookServerException("User with this login isn't active");
                    }
                }
            }
            throw new GradebookServerException("User with this login doesn't exists");
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
            throw new GradebookServerException("Student with this userId doesn't exist");
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
            throw new GradebookServerException("Teacher with this userId doesn't exist");
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
    }
}