using Moq;
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

        #region DeleteStudent

        [Fact]
        public void DeleteStudent_ExistingStudent_Test()
        {
            // Arrange
            var student = new Student(1, "name", "email");

            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
            // Student must exist before deletion
            repoMock.Setup(r => r.Get(1)).Returns(student);

            var service = new StudentService(repoMock.Object);

            // Act
            service.DeleteStudent(student);

            // Assert
            repoMock.Verify(r => r.Delete(student), Times.Once);
        }

        [Fact]
        public void DeleteStudent_StudentDoesNotExist_ExpectArgumentException_Test()
        {
            // Arrange
            var student = new Student(1, "name", "email");

            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
            // Student must not exist before deletion
            repoMock.Setup(r => r.Get(1)).Returns(() => null);

            var service = new StudentService(repoMock.Object);

            // Act and assert
            var ex = Assert.Throws<ArgumentException>(() => service.DeleteStudent(student));

            Assert.Equal("Student does not exist", ex.Message);

            repoMock.Verify(r => r.Delete(student), Times.Never);
        }

        [Fact]
        public void DeleteStudent_StudentIsNull_ExpectArgumentNullException_Test()
        {
            // Arrange
            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
            var service = new StudentService(repoMock.Object);

            // Act and assert
            var ex = Assert.Throws<ArgumentNullException>(() => service.DeleteStudent(null));

            // Assert
            repoMock.Verify(r => r.Delete(null), Times.Never);
        }

        #endregion // DeleteStudent

        #region GetStudent

        [Fact]
        public void GetStudent_ExistingStudent_Test()
        {
            // Arrange
            var student = new Student(1, "name", "email");

            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
            repoMock.Setup(r => r.Get(1)).Returns(student);

            var service = new StudentService(repoMock.Object);

            // Act
            Student? result = service.GetStudent(1);

            // Assert
            Assert.Equal(result, student);
            repoMock.Verify(r => r.Get(1), Times.Once);

        }

        [Fact]
        public void GetStudent_NonExistingStudent_Test()
        {
            // Arrange

            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
            repoMock.Setup(r => r.Get(1)).Returns(() => null);

            var service = new StudentService(repoMock.Object);

            // Act
            Student? result = service.GetStudent(1);

            // Assert
            Assert.Null(result);
            repoMock.Verify(r => r.Get(1), Times.Once);
        }

        #endregion // GetStudent

        #region GetAllStudents

        [Fact]
        public void GetAllStudents_Test()
        {
            //Arrange
            // Existing data
            var s1 = new Student(1, "name1", "email1");
            var s2 = new Student(2, "name2", "email2");
            var students = new List<Student>() { s1, s2 };

            Mock<IStudentRepository> repoMock = new Mock<IStudentRepository>();
            repoMock.Setup(r => r.GetAll()).Returns(students);
            
            var service = new StudentService(repoMock.Object);

            // Act
            var result = service.GetAllStudents();

            // Assert
            Assert.Equal(result.ToList().Count, students.Count);
            Assert.Contains(s1, result);
            Assert.Contains(s2, result);
            repoMock.Verify(r => r.GetAll(), Times.Once);
        }

        #endregion // GetAllStudents
    }
}
