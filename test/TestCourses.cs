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
            // Проверяем, что правильно происходит присоединение преподавателя на курс
            var course = new OnlineCourse("OOP", "Zoom");
            var teacher = new Teacher("Ivan", "Petrov");

            course.AssignTeacher(teacher);

            Assert.Equal(teacher.Id, course.AssignedTeacher);
        }

        // ---------- DELETE TEACHER ----------
        [Fact]
        public void DeleteTeacher_ShouldRemoveAssignedTeacher()
        {
            // Проверяем удаление преподавателя с курса (Что id пуст)
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
            // Проверяем добавление студента в список курса
            var course = new OnlineCourse("OOP", "Zoom");
            var student = new Student("Vasya", "Pupkin");

            course.AddStudent(student);

            Assert.Contains(student.Id, course.Students);
        }

        [Fact]
        public void AddStudent_ShouldNotAddDuplicateStudents()
        {
            // Проверяем, что студента нельзя зачислить несколько раз на 1 курс
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
            // Проверяем удаление студента с курса
            var course = new OnlineCourse("OOP", "Zoom");
            var student = new Student("Vasya", "Pupkin");

            course.AddStudent(student);
            course.RemoveStudent(student);

            Assert.DoesNotContain(student.Id, course.Students);
        }

        [Fact]
        public void RemoveStudent_ShouldNotFailIfStudentNotInList()
        {
            // Проверяем, что удаление студента, которого нет в курсе не ломает программу
            var course = new OnlineCourse("OOP", "Zoom");
            var student = new Student("Vasya", "Pupkin");

            course.RemoveStudent(student);

            Assert.Empty(course.Students);
        }

        // ---------- COURSE TYPE ----------
        [Fact]
        public void OnlineCourse_ShouldReturnCorrectType()
        {
            // Проверяем, что тип курса совпадает с классом
            var course = new OnlineCourse("OOP", "Zoom");

            Assert.Equal("Online", course.GetCourseType());
        }

        [Fact]
        public void OfflineCourse_ShouldReturnCorrectType()
        {
            // Проверяем, что тип курса совпадает с классом
            var course = new OfflineCourse("Math", "Room 101");

            Assert.Equal("Offline", course.GetCourseType()); 
        }

        // ---------- SHOW DETAILS ----------
        [Fact]
        public void OnlineCourse_ShowDetails_ShouldReturnPlatform()
        {
            // Проверяем метод получения сведений о платформе
            var course = new OnlineCourse("OOP", "Zoom");

            Assert.Equal("Zoom", course.ShowDetails());
        }

        [Fact]
        public void OfflineCourse_ShowDetails_ShouldReturnClassroom()
        {
            // Проверяем метод получения места нахождения
            var course = new OfflineCourse("Math", "Room 101");

            Assert.Equal("Room 101", course.ShowDetails());
        }

        // ---------- CHANGE PLATFORM / CLASSROOM ----------
        [Fact]
        public void OnlineCourse_ChangePlatform_ShouldUpdatePlatform()
        {
            // Проверяем метод смены платформы
            var course = new OnlineCourse("OOP", "Zoom");

            course.ChangePlatform("MS Teams");

            Assert.Equal("MS Teams", course.Platform);
        }

        [Fact]
        public void OfflineCourse_ChangeClassroom_ShouldUpdateClassroom()
        {
            // Проверяем метод смены аудитории
            var course = new OfflineCourse("Math", "Room 101");

            course.ChangeClassroom("Room 202");

            Assert.Equal("Room 202", course.Classroom);
        }
    }
}
