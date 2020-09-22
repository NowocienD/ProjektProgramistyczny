using System;
using System.Collections.Generic;
using System.Text;
using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using GradebookBackend.Services;

namespace BackendTests
{
    public class ClassServiceTests
    {

        [Fact]
        public void GetAllClasses_test()
        {
            ClassListDTO expected = new ClassListDTO();

            expected.ClassList.Add(
                new ClassDTO()
                {
                    Id = 1,
                    Name = "nameclass1"
                });

            expected.ClassList.Add(
                new ClassDTO()
                {
                    Id = 2,
                    Name = "nameclass2"
                });

            expected.ClassList.Add(
                new ClassDTO()
                {
                    Id = 3,
                    Name = "nameclass3"
                });


            List<ClassDAO> classDTO = new List<ClassDAO>();
            classDTO.Add(new ClassDAO()
            {
                Id = 1,
                Name = "nameclass1",
                ClassSubjects = null,
                Lessons = new List<LessonDAO>()
                {
                    new LessonDAO()
                }
            }); 
            
            classDTO.Add(new ClassDAO()
            {
                Id = 2,
                Name = "nameclass2",
                ClassSubjects = null,
                Lessons = new List<LessonDAO>()
                {
                    new LessonDAO()
                }
            }); 
            
            classDTO.Add(new ClassDAO()
            {
                Id = 3,
                Name = "nameclass3",
                ClassSubjects = null,
                Lessons = null
            });

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.GetAll()).Returns(classDTO);

            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();


            var testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object
                );

            var result = testService.GetAllClasses();

            Assert.Equal(expected.ClassList[0].Id, result.ClassList[0].Id);
            Assert.Equal(expected.ClassList[1].Id, result.ClassList[1].Id);
            Assert.Equal(expected.ClassList[2].Id, result.ClassList[2].Id);

            Assert.Equal(expected.ClassList[0].Name, result.ClassList[0].Name);
            Assert.Equal(expected.ClassList[1].Name, result.ClassList[1].Name);
            Assert.Equal(expected.ClassList[2].Name, result.ClassList[2].Name);
        }
    }
}
