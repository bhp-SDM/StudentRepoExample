﻿using Moq;
using StudentRepoExample.Core.Domain;
using StudentRepoExample.Core.Interfaces;
using StudentRepoExample.Core.Service;
using Xunit;

namespace XunitTestProject
{
    public class StudentServiceTest
    {
        #region CreateStudentService
        [Fact]
        public void CreateStudentService_ValidStudentRepository_Test()
        {
            // Arrange
            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();

            // Act
            var service = new StudentService(repoMock.Object);

            // Assert
            Assert.NotNull(service);
            Assert.True(service is StudentService);
        }

        [Fact]
        public void CreateStudentService_StudentRepositoryIsNull_ExpectArgumentNullException_Test()
        {
            // Arrange
            StudentService? service = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => service = new StudentService(null));
        }
        #endregion // CreateStudentService

        #region AddStudent

        [Theory]
        [InlineData(1, "name", "email")]
        [InlineData(1, "name", null)]
        public void AddStudent_ValidStudent_Test(int id, string name, string email)
        {
            // Arrange
            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();

            var service = new StudentService(repoMock.Object);

            var student = new Student(id, name, email);

            // Act
            service.AddStudent(student);

            // Assert
            repoMock.Verify(r => r.Add(student), Times.Once);
        }

        [Theory]
        [InlineData(0, "name", "email", "Invalid Id. Id must be greater than zero")]    // invalid id. Id <= 0
        [InlineData(1, null, "email", "Invalid name. Name is missing")]                 // invalid name. name == null
        [InlineData(1, "", "email", "Invalid name. Name is empty")]                     // invalid name. name == ""
        [InlineData(1, "name", "", "Invalid email. Email is empty")]                    // invalid email. email == ""
        public void AddStudent_InvalidStudent_ExpectArgumentException_Test(int id, string name, string email, string expected)
        {
            // Arrange
            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
            var service = new StudentService(repoMock.Object);

            var student = new Student(id, name, email);

            // Act and assert
            var ex = Assert.Throws<ArgumentException>(() => service.AddStudent(student));

            // Assert
            Assert.Equal(expected, ex.Message);
            repoMock.Verify(r => r.Add(student), Times.Never);
        }

        [Fact]
        public void AddStudent_StudentIsNull_ExpectArgumentNullException_Test()
        {
            // Arrange
            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
            var service = new StudentService(repoMock.Object);

            // Act and assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.AddStudent(null));

            // Assert
            repoMock.Verify(r => r.Add(null), Times.Never);
        }

        #endregion // AddStudent

        #region UpdateStudent

        [Theory]
        [InlineData(1, "newName", "email")]
        [InlineData(1, "name", "newEmail")]
        [InlineData(1, "name", null)]

        public void UpdateStudent_ValidUpdate_Test(int id, string name, string email)
        {
            // Arrange
            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
            var service = new StudentService(repoMock.Object);

            var student = new Student(id, name, email);

            // Act
            service.UpdateStudent(student);

            // Assert
            repoMock.Verify(r => r.Update(student), Times.Once);
        }

        [Theory]
        [InlineData(0, "name", "email", "Invalid Id. Id must be greater than zero")]    // invalid id. Id <= 0
        [InlineData(1, null, "email", "Invalid name. Name is missing")]                 // invalid name. name == null
        [InlineData(1, "", "email", "Invalid name. Name is empty")]                     // invalid name. name == ""
        [InlineData(1, "name", "", "Invalid email. Email is empty")]                    // invalid email. email == ""
        public void UpdateStudent_InvalidUpdate_ExpectArgumentException_Test(int id, string name, string email, string expected)
        {
            // Arrange
            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
            var service = new StudentService(repoMock.Object);

            var student = new Student(id, name, email);

            // Act and assert
            var ex = Assert.Throws<ArgumentException>(() => service.UpdateStudent(student));

            Assert.Equal(expected, ex.Message);
            repoMock.Verify(r => r.Update(student), Times.Never);
        }

        //[Fact]
        //public void UpdateStudent_StudentIsNull_ExpectArgumentNullException_Test()
        //{
        //    // Arrange
        //    Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
        //    var service = new StudentService(repoMock.Object);

        //    // Act and assert
        //    var ex = Assert.Throws<ArgumentNullException>(() => service.UpdateStudent(null));

        //    // Assert
        //    repoMock.Verify(r => r.Update(null), Times.Never);
        //}



        [Fact]
        public void UpdateStudent_StudentIsNull_ExpectArgumentNullException_Test()
        {
            // Arrange
            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
            var service = new StudentService(repoMock.Object);

            // Act and assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.UpdateStudent(null));

            // Assert
            repoMock.Verify(r => r.Update(null), Times.Never);
        }



        #endregion // UpdateStudent
    }
}
