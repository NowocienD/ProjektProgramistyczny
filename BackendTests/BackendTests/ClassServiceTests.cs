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

        [Fact]
        public void GetAllClasses_()
        {
            //arrange
            var classRepositoryMock = new Mock<IRepository<ClassDAO>>();
            var lessonRepositoryMock = new Mock<IRepository<LessonDAO>>();

            //act
            var s = new ClassService(
                    classRepositoryMock.Object,
                    lessonRepositoryMock.Object);
            //assert
            Assert.True(true);
        }
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
                    }
                };

                yield return new object[] {
                    new ClassDTO(),
                    new ClassDAO()
                         };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        [Theory]
        [ClassData(typeof(ClassDAO_to_classDTO_testData))]
        public void ClassDAO_to_classDTO_whenCorrectData(ClassDTO expectedClassDTO, ClassDAO classDAO)
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
    }
}
