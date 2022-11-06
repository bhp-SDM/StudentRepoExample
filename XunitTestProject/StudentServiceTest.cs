using Moq;
using StudentRepoExample.Core.Interfaces;
using StudentRepoExample.Core.Service;
using Xunit;

namespace XunitTestProject
{
    public class StudentServiceTest
    {
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
    }
}
