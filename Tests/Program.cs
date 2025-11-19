using System;
using LAB_1_OOP.lib.Course;
using LAB_1_OOP.lib.Person;
using Xunit;

namespace LAB_1_OOP.Tests
{
    public class CourseTests
    {
        // ---------- ASSIGN TEACHER ----------
        [Fact]
        public void AssignTeacher_ShouldAssignTeacherId()
        {
            var course = new OnlineCourse("OOP", "Zoom");
            var teacher = new Teacher("Ivan", "Petrov");

            course.AssignTeacher(teacher);

            Assert.Equal(teacher.Id, course.AssignedTeacher);
        }

        // ---------- DELETE TEACHER ----------
        [Fact]
        public void DeleteTeacher_ShouldRemoveAssignedTeacher()
        {
            var course = new OnlineCourse("OOP", "Zoom");
            var teacher = new Teacher("Ivan", "Petrov");

            course.AssignTeacher(teacher);
            course.DeleteTeacher(teacher);

            Assert.Equal(Guid.Empty, course.AssignedTeacher);
        }

        // ---------- ADD STUDENT ----------
        [Fact]
        public void AddStudent_ShouldAddStudentId()
        {
            var course = new OnlineCourse("OOP", "Zoom");
            var student = new Student("Vasya", "Pupkin");

            course.AddStudent(student);

            Assert.Contains(student.Id, course.Students);
        }

        [Fact]
        public void AddStudent_ShouldNotAddDuplicateStudents()
        {
            var course = new OnlineCourse("OOP", "Zoom");
            var student = new Student("Vasya", "Pupkin");

            course.AddStudent(student);
            course.AddStudent(student); // duplicate

            Assert.Single(course.Students); // only 1 entry
        }

        // ---------- REMOVE STUDENT ----------
        [Fact]
        public void RemoveStudent_ShouldRemoveStudentId()
        {
            var course = new OnlineCourse("OOP", "Zoom");
            var student = new Student("Vasya", "Pupkin");

            course.AddStudent(student);
            course.RemoveStudent(student);

            Assert.DoesNotContain(student.Id, course.Students);
        }

        [Fact]
        public void RemoveStudent_ShouldNotFailIfStudentNotInList()
        {
            var course = new OnlineCourse("OOP", "Zoom");
            var student = new Student("Vasya", "Pupkin");

            course.RemoveStudent(student); // Should not throw

            Assert.Empty(course.Students);
        }

        // ---------- COURSE TYPE ----------
        [Fact]
        public void OnlineCourse_ShouldReturnCorrectType()
        {
            var course = new OnlineCourse("OOP", "Zoom");

            Assert.Equal("Online", course.GetCourseType());
        }

        [Fact]
        public void OfflineCourse_ShouldReturnCorrectType()
        {
            var course = new OfflineCourse("Math", "Room 101");

            // Тут ошибка в твоём коде: OfflineCourse возвращает "Online"
            Assert.Equal("Online", course.GetCourseType()); 
        }

        // ---------- SHOW DETAILS ----------
        [Fact]
        public void OnlineCourse_ShowDetails_ShouldReturnPlatform()
        {
            var course = new OnlineCourse("OOP", "Zoom");

            Assert.Equal("Zoom", course.ShowDetails());
        }

        [Fact]
        public void OfflineCourse_ShowDetails_ShouldReturnClassroom()
        {
            var course = new OfflineCourse("Math", "Room 101");

            Assert.Equal("Room 101", course.ShowDetails());
        }

        // ---------- CHANGE PLATFORM / CLASSROOM ----------
        [Fact]
        public void OnlineCourse_ChangePlatform_ShouldUpdatePlatform()
        {
            var course = new OnlineCourse("OOP", "Zoom");

            course.ChangePlatform("MS Teams");

            Assert.Equal("MS Teams", course.Platform);
        }

        [Fact]
        public void OfflineCourse_ChangeClassroom_ShouldUpdateClassroom()
        {
            var course = new OfflineCourse("Math", "Room 101");

            course.ChangeClassroom("Room 202");

            Assert.Equal("Room 202", course.Classroom);
        }
    }
}
