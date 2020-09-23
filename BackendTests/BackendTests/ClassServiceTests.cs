using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GradebookBackend;
using GradebookBackend.DTO;
using GradebookBackend.Logger.Service;
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

        private class Ids_testData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                for (int i = 0; i < 100; i++)
                {
                    yield return new object[] { i };
                }
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
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

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
            var loggerMock = new Mock<ILogerService>();
            Type type = typeof(ClassService);
            var className = Activator.CreateInstance(type, classRepositoryMock.Object, lessonRepositoryMock.Object, loggerMock.Object);
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
            var loggerMock = new Mock<ILogerService>();

            Type type = typeof(ClassService);
            var className = Activator.CreateInstance(type, classRepositoryMock.Object, lessonRepositoryMock.Object, loggerMock.Object);
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
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

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
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

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
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

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
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

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
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

            // act
            // assert
            Assert.Throws<GradebookServerException>(() => testService.AddClass(mockData));
            loggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(Ids_testData))]
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
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

            // act
            testService.DeleteClass(id);

            // assert
            Assert.True(true);
        }

        [Theory]
        [ClassData(typeof(Ids_testData))]
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
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

            // act
            // assert
            Assert.Throws<GradebookServerException>(() => testService.DeleteClass(id));
            loggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(Ids_testData))]
        public void DeleteClass_NoData_expectExceprionThrown(int id)
        {
            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.GetAll()).Returns(new List<ClassDAO>());
            classRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

            // act
            // assert
            Assert.Throws<GradebookServerException>(() => testService.DeleteClass(id));
            loggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(Ids_testData))]
        public void UpdateClass_CorrectData_expectPass(int id)
        {
            List<ClassDAO> mockClassDAOList = new List<ClassDAO>();
            for (int i = 0; i < 100; i++)
            {
                mockClassDAOList.Add(new ClassDAO()
                {
                    Id = i,
                    Name = "nameclass" + i.ToString(),
                });
            }

            ClassDTO mockData = new ClassDTO()
            {
                Id = id,
                Name = "className4",
            };

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.GetAll()).Returns(mockClassDAOList);
            classRepositoryMock.Setup(x => x.Update(It.IsAny<ClassDAO>()));
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

            // act
            testService.UpdateClass(mockData, mockData.Id);

            // assert
            Assert.True(true);
        }

        [Theory]
        [ClassData(typeof(Ids_testData))]
        public void UpdateClass_NoData_expectPass(int id)
        {
            List<ClassDAO> mockClassDAOList = new List<ClassDAO>();

            ClassDTO mockData = new ClassDTO()
            {
                Id = id,
                Name = "className4",
            };

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.GetAll()).Returns(mockClassDAOList);
            classRepositoryMock.Setup(x => x.Update(It.IsAny<ClassDAO>()));
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

            // act
            // assert
            Assert.Throws<GradebookServerException>(() => testService.UpdateClass(mockData, mockData.Id));
            loggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(Ids_testData))]
        public void UpdateClass_NoCorrectData_expectPass(int id)
        {
            List<ClassDAO> mockClassDAOList = new List<ClassDAO>();
            for (int i = 0; i < 100; i++)
            {
                if (i != id)
                {
                    mockClassDAOList.Add(new ClassDAO()
                    {
                        Id = i,
                        Name = "nameclass" + i.ToString(),
                    });
                }
            }

            ClassDTO mockData = new ClassDTO()
            {
                Id = id,
                Name = "className4",
            };

            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            classRepositoryMock.Setup(x => x.GetAll()).Returns(mockClassDAOList);
            classRepositoryMock.Setup(x => x.Update(It.IsAny<ClassDAO>()));
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();
            var loggerMock = new Mock<ILogerService>();

            IClassService testService = new ClassService(
                classRepositoryMock.Object,
                lessonRepositoryMock.Object,
                loggerMock.Object);

            // act
            // assert
            Assert.Throws<GradebookServerException>(() => testService.UpdateClass(mockData, mockData.Id));
            loggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.Once);
        }
    }
}
