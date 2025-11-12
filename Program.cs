using System;
using System.Linq;
using LAB_1_OOP.lib.Course;
using LAB_1_OOP.lib.Person;

namespace Application
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello! To see all available commands, type 'h' or 'help'.");
            CourseManager.Load();

            while (true)
            {
                var cmd = Console.ReadLine();
                if (string.IsNullOrEmpty(cmd)) continue;
                
                switch (cmd.ToLower())
                {
                    case "list":
                        Console.WriteLine("To show all Courses of specific person, please type: \n" +
                                          "'teacher/person' 'first name' 'last name'\nTo show all courses, press 'Enter'");
                        var line = Console.ReadLine();
                        if (string.IsNullOrEmpty(line))
                        {
                            foreach (var course in CourseManager.Courses)
                            {
                                Console.WriteLine($"– {course.Title}, Type: {course.GetCourseType()}" +
                                                  $" specifics: {course.ShowDetails()}.");
                                string teachers = "\t Assigned Teacher: ";
                                teachers += CourseManager.Teachers.FirstOrDefault(
                                    t => t.Id == course.AssignedTeacher)?.GetFullName();
                                string students = "\t Assigned Students: ";
                                foreach (var stId in course.Students)
                                {
                                    students += CourseManager.Students.FirstOrDefault(
                                                    s => s.Id == stId)?.GetFullName() + ", ";
                                }
                                    
                                Console.WriteLine(teachers + "\n" + students);
                            }
                            break;
                        };
                        var parts = line.Trim().Split();
                        if (parts.Length != 3)
                        {
                            Console.WriteLine("Incorrect naming, please try again");
                            break;
                        }
                        var selectedPersonName = (parts[1] + " " + parts[2]).ToLower().Trim();
                        if (parts[0] == "teacher")
                        {
                            var selectedTeacher = CourseManager.Teachers.FirstOrDefault(
                                t => t.GetFullName() == selectedPersonName);
                            if (selectedTeacher == null)
                            {
                                Console.WriteLine("Teacher not found");
                                break;
                            }
                            foreach (var course in CourseManager.Courses.Where(
                                         c=> c.AssignedTeacher == selectedTeacher.Id))
                            {
                                Console.WriteLine($"– {course.Title}, Type: {course.GetCourseType()}" +
                                                  $" specifics: {course.ShowDetails()}.");
                            }
                        }
                        else if (parts[0] == "student")
                        {
                            var selectedStudent = CourseManager.Students.FirstOrDefault(
                                s => s.GetFullName() == selectedPersonName);
                            if (selectedStudent == null)
                            {
                                Console.WriteLine("Student not found.");
                                break;
                            }
                            foreach (var course in CourseManager.Courses.Where(c 
                                         => c.Students.Contains(selectedStudent.Id)))
                            {
                                Console.WriteLine($"– {course.Title}, Type: {course.GetCourseType()}" +
                                          $" specifics: {course.ShowDetails()}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Unknown person type, please try again");
                        }
                        break;
                    
                    case "list_students":
                        foreach (var student in CourseManager.Students)
                        {
                            Console.WriteLine($"– {student.GetFullName()}, uid: {student.Id}");
                        };
                        break;
                    
                    case "list_teachers":
                        foreach (var teacher in CourseManager.Teachers)
                        {
                            Console.WriteLine($"– {teacher.GetFullName()}, uid: {teacher.Id}");
                        };
                        break;
                    
                    case "add":
                        Console.WriteLine("Please specify: 'student' or 'teacher'");
                        var personType  = Console.ReadLine()?.ToLower();
                        if (personType == "student" || personType == "teacher")
                        {
                            Console.WriteLine("Please specify person name: 'FirstName LastName'");
                            var personFullName = Console.ReadLine()?.ToLower().Split();
                            if (personFullName.Length != 2)
                            {
                                Console.WriteLine("Incorrect person name, please enter first and last name with space");
                                break;
                            }

                            if (personType == "student")
                            {
                                var newStudent = new Student(personFullName[0], personFullName[1]);
                                CourseManager.Students.Add(newStudent); 
                                CourseManager.Save();
                                Console.WriteLine($"Successfully created student {newStudent.FirstName} {newStudent.LastName}");
                            }
                            else
                            {
                                var newTeacher = new Teacher(personFullName[0], personFullName[1]);
                                CourseManager.Teachers.Add(newTeacher);
                                CourseManager.Save();
                                Console.WriteLine($"Successfully createed teacher " +
                                                  $"{newTeacher.FirstName} {newTeacher.LastName}");
                            }
                            break;
                        }
                        Console.WriteLine("Incorrect person type, please try again"); break;
                        
                    case "enroll":
                        Console.WriteLine("Please specify student name: 'firstName' 'lastName'");
                        var studentFullName = Console.ReadLine()?.ToLower().Trim();
                        var enrollingStudent = CourseManager.Students.FirstOrDefault(
                            s => s.GetFullName() == studentFullName);
                        if (enrollingStudent == null)
                        {
                            Console.WriteLine("Student not found.");
                            break;
                        }
                        Console.WriteLine("Please specify course name:");
                        var courseToEnrollName = Console.ReadLine()?.ToLower().Trim();
                        Course courseToEnroll = CourseManager.Courses.FirstOrDefault(
                            c => c.Title.ToLower() == courseToEnrollName);
                        if (courseToEnroll == null)
                        {
                            Console.WriteLine("Course not found.");
                            break;
                        }
                        courseToEnroll.AddStudent(enrollingStudent);
                        CourseManager.Save();
                        Console.WriteLine($"Student {enrollingStudent.GetFullName()} Enrolled" +
                                          $" on {courseToEnroll.GetCourseType()} course {courseToEnroll.Title}");
                        break;
                    
                    case "assign_teacher":
                        Console.WriteLine("Please specify teacher's full name: 'firstName' 'lastName'");
                        var teacherFullName = Console.ReadLine()?.ToLower().Trim();
                        if (teacherFullName == "")
                        { 
                            Console.WriteLine("Incorrect teacher name, please enter first and last name with space"); 
                            break;
                            
                        }
                        var teacherForChange = CourseManager.Teachers.FirstOrDefault(
                            t => t.GetFullName() == teacherFullName);
                        if (teacherForChange == null)
                        {
                            Console.WriteLine("Teacher not found.");
                            break;
                        }
                        Console.WriteLine("Please specify course name:");
                        var courseTeacherChangeName = Console.ReadLine()?.Trim().ToLower();
                        var courseTeacherChange =
                            CourseManager.Courses.FirstOrDefault(c => c.Title.ToLower() == courseTeacherChangeName);
                        if (courseTeacherChange == null)
                        {
                            Console.WriteLine("Course not found.");
                            break;
                        }
                        courseTeacherChange.AssignTeacher(teacherForChange);
                        CourseManager.Save();
                        Console.WriteLine($"Successfuly assigned teacher to {courseTeacherChangeName} " +
                                          $"in course {courseTeacherChangeName}.");
                        break;
                    
                    case "create_course":
                        Console.WriteLine("Please specify course name:");
                        var newCourseName = Console.ReadLine();
                        if (newCourseName == null)
                        {
                            Console.WriteLine("Incorrect course name, please try again.");
                            break;
                        }
                        Console.WriteLine("Please specify Course Type 'online/offline'");
                        var newCourseType = Console.ReadLine()?.ToLower().Trim();
                        if (newCourseType != "online" && newCourseType != "offline")
                        {
                            Console.WriteLine("Incorrect course type, please try again.");
                            break;
                        }
                        Console.WriteLine("Please specify course teacher: 'First Name' 'Last Name'");
                        var newCourseTeacherName = Console.ReadLine()?.ToLower();
                        
                        var newCourseTeacher = CourseManager.Teachers.FirstOrDefault(
                            t => t.GetFullName() == newCourseTeacherName);
                        if (newCourseTeacher == null)
                        {
                            Console.WriteLine("Teacher not found. Please try again.");
                            break;
                        }

                        Course newCourse;
                        if (newCourseType == "online")
                        {
                            Console.WriteLine("Please specify course platform");
                            var newCoursePlatformName = Console.ReadLine()?.ToLower().Trim();
                            newCourse = new OnlineCourse(newCourseName, newCoursePlatformName);
                        }
                        else
                        {
                            Console.WriteLine("Please specify course location");
                            var newCourseLocationName = Console.ReadLine()?.ToLower().Trim();
                            newCourse = new OfflineCourse(newCourseName, newCourseLocationName);
                        }

                        if (newCourse == null)
                        {
                            Console.WriteLine("Cannot create course. Please try again.");
                            break;
                        }
                        newCourse.AssignTeacher(newCourseTeacher);
                        CourseManager.Courses.Add(newCourse);
                        CourseManager.Save();
                        Console.WriteLine($"Successfully created {newCourse.GetType()} course: {newCourse.Title}" +
                                          $"\n specifics: {newCourse.ShowDetails()}.");
                        break;
                    
                    case "delete":
                        Console.WriteLine("Please specify who you want to delete: 'course/student/teacher'");
                        var deletingTypeName = Console.ReadLine()?.ToLower().Trim();
                        
                        if (deletingTypeName == "course")
                        {
                            Console.WriteLine("Please specify title of course you want to delete: ");
                            var deleteCourseTitle = Console.ReadLine()?.ToLower().Trim();
                            var courseToDelete = CourseManager.Courses.FirstOrDefault(c => c.Title.ToLower() == deleteCourseTitle);
                            if (courseToDelete == null) { Console.WriteLine("Course not found."); break; }
                            CourseManager.Courses.Remove(courseToDelete);
                            CourseManager.Save();
                            Console.WriteLine("Course deleted.");
                            break;
                        }
                        if (deletingTypeName == "student" || deletingTypeName == "teacher")
                        {
                            Console.WriteLine("Please specify person name you want to delete:");
                            var deletePersonName = Console.ReadLine()?.ToLower().Trim();
                            if (deletingTypeName == "student")
                            {
                                var personToDelete = CourseManager.Students.FirstOrDefault(
                                    s => s.GetFullName() == deletePersonName);
                                if (personToDelete == null) { Console.WriteLine("Person not found."); break; }
                                CourseManager.Students.Remove(personToDelete);

                                foreach (var course in CourseManager.Courses.Where(c =>
                                             c.Students.Contains(personToDelete.Id)))
                                {
                                    course.RemoveStudent(personToDelete);
                                }
                                
                                CourseManager.Save();
                                Console.WriteLine("Student deleted.");
                                break;
                            }
                            else
                            {
                                var personToDelete = CourseManager.Teachers.FirstOrDefault(
                                    t => t.GetFullName() == deletePersonName);
                                if (personToDelete == null) { Console.WriteLine("Person not found."); break; }
                                CourseManager.Teachers.Remove(personToDelete);
                                
                                foreach (var course in CourseManager.Courses.Where(c =>
                                             c.AssignedTeacher == personToDelete.Id))
                                {
                                    course.DeleteTeacher(personToDelete);
                                }

                                CourseManager.Save();
                                Console.WriteLine("Teacher deleted.");
                                break;
                            }
                        }
                        Console.WriteLine("Incorrect type, please try again.");
                        break;
                    
                    case "exit":
                        Console.WriteLine("Terminating program...");
                        return;
                        
                    case "h":
                    case "help":
                        Console.WriteLine("  Available commands:");
                        Console.WriteLine("  h/help \t \t help");
                        Console.WriteLine("  list \t \t \t return list of all courses or specific courses");
                        Console.WriteLine("  list_students \t return list of all sudents");
                        Console.WriteLine("  list_teachers \t \t return list of all teachers");
                        Console.WriteLine("  add \t \t \t add new student or teacher");
                        Console.WriteLine("  enroll \t \t enroll student on a course");
                        Console.WriteLine("  assign_teacher \t assign teacher on a course");
                        Console.WriteLine("  create_course \t create new course");
                        Console.WriteLine("  delete \t \t delete person or course");
                        Console.WriteLine("  exit \t \t \t exit program");
                        
                        break;
                    default:
                        Console.WriteLine("Invalid command, use 'help' for a list of available commands.");
                        break;
                }
                
            }
        }
    }
}