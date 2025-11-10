using System;
using System.Collections.Generic;
using LAB_1_OOP.lib.Person;

namespace LAB_1_OOP.lib.Course
{
    public abstract class Course
    {
        public string Title { get; protected set; }
        public List<Teacher> AssignedTeachers { get; private set; } = new List<Teacher>();
        public List<Student> Students { get; protected set; } = new List<Student>();

        public abstract string GetCourseType();
        public abstract string ShowDetails();
        
        public void AssignTeacher(Teacher teacher)
        {
            AssignedTeachers.Add(teacher);
        }

        public void DeleteTeacher(Teacher teacher)
        {
            AssignedTeachers.Remove(teacher);
        }

        public void AddStudent(Student student)
        {
            if (!Students.Contains(student))
                Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }

       
    }
}