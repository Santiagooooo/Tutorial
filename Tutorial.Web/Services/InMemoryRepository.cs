using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Tutorial.Web.Model;

namespace Tutorial.Web.Services
{
    public class InMemoryRepository : IRepository<Student>
    {
        public readonly List<Student> students;
        public InMemoryRepository()
        {
            students = new List<Student>
        {
            new Student
            {
                Id = 1,
                FirstName = "zhang",
                LastName = "san",
                BirthDate = new DateTime(1980,1,4)
            },
            new Student
            {
                Id = 2,
                FirstName = "li",
                LastName = "si",
                BirthDate = new DateTime(1967,6,13)
            },
            new Student
            {
                Id = 3,
                FirstName = "wang",
                LastName = "wu",
                BirthDate = new DateTime(2001,1,1)
            }
        };
        }

        public Student Add(Student studentModel)
        {
            var maxId = students.Max(x => x.Id);
            studentModel.Id = maxId + 1;
            students.Add(studentModel);
            return studentModel;
        }

        public IEnumerable<Student> GetAll()
        {
            return students;
        }

        public Student GetById(int id)
        {
            //return new Student { x => x.Id == id };
            return students.FirstOrDefault(x => x.Id == id);
        }
    }
}
