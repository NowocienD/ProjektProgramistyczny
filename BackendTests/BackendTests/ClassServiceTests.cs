using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GradebookBackend;
using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.Services;
using GradebookBackend.ServicesCore;
using Moq;
using Xunit;

namespace BackendTests
{
    public class ClassServiceTests
    {
        public class DTOandDAO_tuple_testData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new ClassDTO()
                        {
                            Id = 1,
                            Name = "nameclass1",
                        },
                    new ClassDAO()
                        {
                            Id = 1,
                            Name = "nameclass1",
                            ClassSubjects = null,
                            Lessons = new List<LessonDAO>()
                            {
                                new LessonDAO(),
                            },
                        },
                };
                yield return new object[]
                {
                    new ClassDTO()
                        {
                            Id = 2,
                            Name = null,
                        },
                    new ClassDAO()
                        {
                            Id = 2,
                            Name = null,
                            ClassSubjects = null,
                            Lessons = new List<LessonDAO>()
                            {
                                new LessonDAO(),
                            },
                        },
                };

                yield return new object[]
                {
                    new ClassDTO(),
                    new ClassDAO(),
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Fact]
        public void GetAllClasses_CorrectData_expectEquals()
        {
            // arrange
            ClassListDTO expected = new ClassListDTO();
            List<ClassDAO> classDAO = new List<ClassDAO>();

            DTOandDAO_tuple_testData testData = new DTOandDAO_tuple_testData();
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
                lessonRepositoryMock.Object);

            // act
            var result = testService.GetAllClasses();

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ClassDAOList_to_classDTOList_CorrectData_expectEquals()
        {
            // arrange
            ClassListDTO expected = new ClassListDTO();
            List<ClassDAO> classDAOList = new List<ClassDAO>();

            DTOandDAO_tuple_testData testData = new DTOandDAO_tuple_testData();
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

            // act
            var result = (ClassListDTO)methodName.Invoke(className, new object[] { classDAOList });

            // assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(DTOandDAO_tuple_testData))]
        public void ClassDAO_to_classDTO_CorrectData_expectEquals(ClassDTO expectedClassDTO, ClassDAO classDAO)
        {
            // arrange
            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();

            Type type = typeof(ClassService);
            var className = Activator.CreateInstance(type, classRepositoryMock.Object, lessonRepositoryMock.Object);
            MethodInfo methodName = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.Name == "ClassDAO_to_classDTO" && x.IsPrivate)
            .First();

            // Act
            var result = (ClassDTO)methodName.Invoke(className, new object[] { classDAO });

            // assert
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
                Name = "someClass",
            });
            expected.Add(new ClassDTO()
            {
                Id = 5,
                Name = "someClass",
            });
            expected.Add(new ClassDTO()
            {
                Id = 2,
                Name = "someClass",
            });
            expected.Add(new ClassDTO()
            {
                Id = 1,
                Name = "someClass",
            });

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(new ClassDAO() { Name = "someClass" });
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();
            lessonRepositoryMock.Setup(x => x.GetAll()).Returns(lissonsDAOList);

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object);

            // act
            ClassListDTO classListDTO = testService.GetAllClassesOfTeacher(teacherID);

            // assert
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
                Name = "someClass",
            });
            expected.Add(new ClassDTO()
            {
                Id = 1,
                Name = "someClass",
            });

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(new ClassDAO() { Name = "someClass" });
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();
            lessonRepositoryMock.Setup(x => x.GetAll()).Returns(lissonsDAOList);

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object);

            // act
            ClassListDTO classListDTO = testService.GetAllClassesOfTeacher(teacherID);

            // assert
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
                lessonRepositoryMock.Object);

            // act
            ClassListDTO classListDTO = testService.GetAllClassesOfTeacher(teacherID);

            // assert
            Assert.Equal(expected, classListDTO.ClassList);
        }

        [Fact]
        public void AddClass_correctData_expectPass()
        {
            List<ClassDAO> mockClassDAO = new List<ClassDAO>();

            mockClassDAO.Add(new ClassDAO()
            {
                Id = 1,
                Name = "nameclass1",
                ClassSubjects = null,
                Lessons = new List<LessonDAO>()
                        {
                            new LessonDAO(),
                        },
            });

            for (int i = 0; i < 100; i++)
            {
                mockClassDAO.Add(new ClassDAO()
                {
                    Id = i,
                    Name = "nameclass" + i.ToString(),
                });
            }

            ClassDTO mockData = new ClassDTO()
            {
                Name = "class1",
                Id = 2,
            };

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.GetAll()).Returns(mockClassDAO);
            classRepositoryMock.Setup(x => x.Add(It.IsAny<ClassDAO>()));
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object);

            // act
            testService.AddClass(mockData);

            // assert
            Assert.True(true);
        }

        [Fact]
        public void AddClass_redundantData_expectExceprionThrown()
        {
            List<ClassDAO> mockClassDAO = new List<ClassDAO>();

            mockClassDAO.Add(new ClassDAO()
            {
                Id = 1,
                Name = "nameclass1",
                ClassSubjects = null,
                Lessons = new List<LessonDAO>()
                        {
                            new LessonDAO(),
                        },
            });

            for (int i = 0; i < 100; i++)
            {
                mockClassDAO.Add(new ClassDAO()
                {
                    Id = i,
                    Name = "nameclass" + i.ToString(),
                });
            }

            ClassDTO mockData = new ClassDTO()
            {
                Name = "nameclass44",
                Id = 2,
            };

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.GetAll()).Returns(mockClassDAO);
            classRepositoryMock.Setup(x => x.Add(It.IsAny<ClassDAO>()));
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object);

            // act
            // assert
            Assert.Throws<GradebookServerException>(() => testService.AddClass(mockData));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(35)]
        [InlineData(8)]
        [InlineData(28)]
        [InlineData(96)]
        [InlineData(64)]
        [InlineData(4)]
        public void DeleteClass_correctData_expectPass(int id)
        {
            List<ClassDAO> mockClassDAO = new List<ClassDAO>();
            for (int i = 0; i < 100; i++)
            {
                mockClassDAO.Add(new ClassDAO()
                { Id = i });
            }

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.GetAll()).Returns(mockClassDAO);
            classRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object);

            // act
            testService.DeleteClass(id);

            // assert
            Assert.True(true);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(9)]
        [InlineData(16)]
        [InlineData(35)]
        [InlineData(39)]
        [InlineData(76)]
        [InlineData(99)]
        [InlineData(66)]
        public void DeleteClass_NoCorrectData_expectExceprionThrown(int id)
        {
            List<ClassDAO> mockClassDAO = new List<ClassDAO>();

            for (int i = 0; i < 100; i++)
            {
                if (i != id)
                {
                    mockClassDAO.Add(new ClassDAO()
                    {
                        Id = i,
                        Name = "nameclass" + i.ToString(),
                    });
                }
            }

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.GetAll()).Returns(mockClassDAO);
            classRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object);

            // act
            // assert
            Assert.Throws<GradebookServerException>(() => testService.DeleteClass(id));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(9)]
        [InlineData(16)]
        [InlineData(35)]
        [InlineData(39)]
        [InlineData(76)]
        [InlineData(99)]
        [InlineData(66)]
        public void DeleteClass_NoData_expectExceprionThrown(int id)
        {
            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.GetAll()).Returns(new List<ClassDAO>());
            classRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object);

            // act
            // assert
            Assert.Throws<GradebookServerException>(() => testService.DeleteClass(id));
        }
    }
}
