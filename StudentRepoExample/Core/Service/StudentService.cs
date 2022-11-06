using StudentRepoExample.Core.Interfaces;

namespace StudentRepoExample.Core.Service
{
    public class StudentService
    {
        private IStudentRepository _studentRepository;

        public StudentService(IStudentRepository repo)
        {
            _studentRepository = repo ?? throw new ArgumentNullException();
        }
    }
}
