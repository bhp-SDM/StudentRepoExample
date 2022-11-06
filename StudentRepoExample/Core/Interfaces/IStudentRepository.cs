using StudentRepoExample.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRepoExample.Core.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student s);
        void Update(Student s);
        void Delete(Student s);
        Student Get(int id);
        IEnumerable<Student> GetAll();
    }
}
