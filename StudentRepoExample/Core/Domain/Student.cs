namespace StudentRepoExample.Core.Domain
{
    public class Student
    {
 
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        
        public Student(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public Student(int id, string name) : this(id, name, null) { }
    }
}
