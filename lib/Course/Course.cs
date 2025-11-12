using System;
using System.Collections.Generic;
using LAB_1_OOP.lib.Person;

namespace LAB_1_OOP.lib.Course
{
    public abstract class Course
    {
        public string Title { get; protected set; }
        public Guid AssignedTeacher { get; set; }
        public List<Guid> Students { get; protected set; } = new List<Guid>();

        public abstract string GetCourseType();
        public abstract string ShowDetails();
        
        public void AssignTeacher(Teacher teacher)
        {
            AssignedTeacher = teacher.Id;
        }

        public void DeleteTeacher(Teacher teacher)
        {
            AssignedTeacher = Guid.Empty;
        }

        public void AddStudent(Student student)
        {
            if (!Students.Contains(student.Id))
                Students.Add(student.Id);
        }

        public void RemoveStudent(Student student)
        {
            if (Students.Contains(student.Id))
                Students.Remove(student.Id);
        }

       
    }
}