using System;
using System.Collections.Generic;
using System.Text;
using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Xunit;
using Moq;
using GradebookBackend.Services;
using System.Reflection;

namespace BackendTests
{
    public class ClassServiceTests
    {
        public class ClassDAO_to_classDTO_testData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new ClassDTO()
                        {
                            Id = 1,
                            Name = "nameclass1"
                        },
                    new ClassDAO()
                        {
                            Id = 1,
                            Name = "nameclass1",
                            ClassSubjects = null,
                            Lessons = new List<LessonDAO>()
                            {
                                new LessonDAO()
                            }
                        } };
                yield return new object[] {
                    new ClassDTO()
                        {
                            Id = 2,
                            Name = null
                        },
                    new ClassDAO()
                        {
                            Id = 2,
                            Name = null,
                            ClassSubjects = null,
                            Lessons = new List<LessonDAO>()
                            {
                                new LessonDAO()
                            }
                    } };

                yield return new object[] {
                    new ClassDTO(),
                    new ClassDAO()
                         };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        [Fact]
        public void GetAllClasses_CorrectData_expectEquals()
        {
            //arrange
            ClassListDTO expected = new ClassListDTO();
            List<ClassDAO> classDAO = new List<ClassDAO>();

            ClassDAO_to_classDTO_testData testData = new ClassDAO_to_classDTO_testData();
            foreach (var item in testData)
            {
                expected.ClassList.Add((ClassDTO)item.ToArray()[0]);
                classDAO.Add((ClassDAO)item.ToArray()[1]);
            }


            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.GetAll()).Returns(classDAO);
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object
                );

            //act
            var result = testService.GetAllClasses();

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ClassDAOList_to_classDTOList_CorrectData_expectEquals()
        {
            //arrange
            ClassListDTO expected = new ClassListDTO();
            List<ClassDAO> classDAOList = new List<ClassDAO>();

            ClassDAO_to_classDTO_testData testData = new ClassDAO_to_classDTO_testData();
            foreach (var item in testData)
            {
                expected.ClassList.Add((ClassDTO)item.ToArray()[0]);
                classDAOList.Add((ClassDAO)item.ToArray()[1]);
            }

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();
            Type type = typeof(ClassService);
            var className = Activator.CreateInstance(type, classRepositoryMock.Object, lessonRepositoryMock.Object);
            MethodInfo methodName = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "ClassDAOList_to_classDTOList" && x.IsPrivate)
            .First();

            //act
            var result = (ClassListDTO)methodName.Invoke(className, new object[] { classDAOList });

            //assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(ClassDAO_to_classDTO_testData))]
        public void ClassDAO_to_classDTO_CorrectData_expectEquals(ClassDTO expectedClassDTO, ClassDAO classDAO)
        {
            //arrange
            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();

            Type type = typeof(ClassService);
            var className = Activator.CreateInstance(type, classRepositoryMock.Object, lessonRepositoryMock.Object);
            MethodInfo methodName = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "ClassDAO_to_classDTO" && x.IsPrivate)
            .First();

            //Act
            var result = (ClassDTO)methodName.Invoke(className, new object[] { classDAO });

            //assert
            Assert.Equal(expectedClassDTO, result);
        }


        [Fact]
        public void GetAllClassesOfTeacher_CorrectData_expectEquals()
        {
            int teacherID = 4;

            List<LessonDAO> lissonsDAOList = new List<LessonDAO>();
            lissonsDAOList.Add(new LessonDAO
            {
                Id = 2,
                TeacherId = 4,
                ClassId = 3,
            });
            lissonsDAOList.Add(new LessonDAO
            {
                Id = 5,
                TeacherId = 4,
                ClassId = 5,
            });
            lissonsDAOList.Add(new LessonDAO
            {
                Id = 4,
                TeacherId = 4,
                ClassId = 2,
            });
            lissonsDAOList.Add(new LessonDAO
            {
                Id = 1,
                TeacherId = 4,
                ClassId = 1,
            });
            lissonsDAOList.Add(new LessonDAO
            {
                Id = 8,
                TeacherId = 3,
                ClassId = 8,
            });

            List<ClassDTO> expected = new List<ClassDTO>();
            expected.Add(new ClassDTO()
            {
                Id = 3,
                Name = "someClass"
            });
            expected.Add(new ClassDTO()
            {
                Id = 5,
                Name = "someClass"
            });
            expected.Add(new ClassDTO()
            {
                Id = 2,
                Name = "someClass"
            });
            expected.Add(new ClassDTO()
            {
                Id = 1,
                Name = "someClass"
            });



            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(new ClassDAO() { Name = "someClass" });
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();
            lessonRepositoryMock.Setup(x => x.GetAll()).Returns(lissonsDAOList);

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object
                );

            //act
            ClassListDTO classListDTO = testService.GetAllClassesOfTeacher(teacherID);

            //assert
            Assert.Equal(expected, classListDTO.ClassList);

        }
        [Fact]
        public void GetAllClassesOfTeacher_RecursiveData_expectEquals()
        {
            int teacherID = 4;

            List<LessonDAO> lissonsDAOList = new List<LessonDAO>();
            lissonsDAOList.Add(new LessonDAO
            {
                Id = 2,
                TeacherId = 8,
                ClassId = 3,
            });
            lissonsDAOList.Add(new LessonDAO
            {
                Id = 5,
                TeacherId = 4,
                ClassId = 2,
            }); 
            lissonsDAOList.Add(new LessonDAO
            {
                Id = 5,
                TeacherId = 4,
                ClassId = 2,
            });
            lissonsDAOList.Add(new LessonDAO
            {
                Id = 5,
                TeacherId = 4,
                ClassId = 2,
            });
            lissonsDAOList.Add(new LessonDAO
            {
                Id = 1,
                TeacherId = 4,
                ClassId = 1,
            });

            List<ClassDTO> expected = new List<ClassDTO>();
            expected.Add(new ClassDTO()
            {
                Id = 2,
                Name = "someClass"
            });
            expected.Add(new ClassDTO()
            {
                Id = 1,
                Name = "someClass"
            });

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(new ClassDAO() { Name = "someClass" });
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();
            lessonRepositoryMock.Setup(x => x.GetAll()).Returns(lissonsDAOList);

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object
                );

            //act
            ClassListDTO classListDTO = testService.GetAllClassesOfTeacher(teacherID);

            //assert
            Assert.Equal(expected, classListDTO.ClassList);
        }

        [Fact]
        public void GetAllClassesOfTeacher_EmptyData_expectEquals()
        {
            int teacherID = 4;

            List<LessonDAO> lissonsDAOList = new List<LessonDAO>();
            List<ClassDTO> expected = new List<ClassDTO>();

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(new ClassDAO() { Name = "someClass" });
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();
            lessonRepositoryMock.Setup(x => x.GetAll()).Returns(lissonsDAOList);

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object
                );

            //act
            ClassListDTO classListDTO = testService.GetAllClassesOfTeacher(teacherID);

            //assert
            Assert.Equal(expected, classListDTO.ClassList);
        }
    }
}
