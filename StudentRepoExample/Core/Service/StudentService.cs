using StudentRepoExample.Core.Domain;
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

        public void AddStudent(Student s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            ThrowIfInvalidStudent(s);

            _studentRepository.Add(s);
        }

        private void ThrowIfInvalidStudent(Student s)
        {
            if (s.Id <= 0)
                throw new ArgumentException("Invalid Id. Id must be greater than zero");
            if (s.Name == null)
                throw new ArgumentException("Invalid name. Name is missing");
            if (s.Name == "")
                throw new ArgumentException("Invalid name. Name is empty");
            if (s.Email == "")
                throw new ArgumentException("Invalid email. Email is empty");
        }

        public void UpdateStudent(Student s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            
            ThrowIfInvalidStudent(s);

            _studentRepository.Update(s);
        }

        #region DeleteStudent



        #endregion // DeleteStudent
    }
}
