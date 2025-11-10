using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LAB_1_OOP.lib.Person;
using Newtonsoft.Json;

namespace LAB_1_OOP.lib.Course
{
    public static class CourseManager
    {
        public static List<Course> Courses { get; private set; }
        public static List<Teacher> Teachers { get; private set; }
        public static List<Student> Students { get; private set; }
        
        private const string SavePath = "SaveData.json";

        public static void AddCourse(Course course)
        {
            Courses.Add(course);
        }


        public static void Save()
        {
            var wrapper = new SaveWrapper()
            {
                CoursesSave = Courses,
                TeachersSave = Teachers,
                StudentsSave = Students
            };
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(wrapper, settings);
            File.WriteAllText(SavePath, json);

        }

        public static void Load()
        {
            if (!File.Exists(SavePath))
            {
                Courses = new List<Course>
                {
                    new OnlineCourse("OOP", "ZoomMeetings"),
                    new OnlineCourse("UX/UI design", "Figma"),
                    new OfflineCourse("Probability Theory", "Chaykovskogo 12, 201"),
                    new OfflineCourse("Databases", "Birjevaya 14, 206"),
                    new OfflineCourse("English C1", "Lomonosova 9, 3412")
                };
                Teachers = new List<Teacher>
                {
                    new Teacher("Jeremy", "HZ"),
                    new Teacher("Sergey", "Popov"),
                    new Teacher("Inna", "Nikolayevna"),
                };
                Students = new List<Student>
                {
                    new Student("Vasya", "Kulkin"),
                    new Student("Petya", "Shkolkin"),
                    new Student("Kalina", "Grozova"),
                    new Student("Herald", "Prigojiv"),
                };
                
                Courses[0].AssignTeacher(Teachers[1]);
                Courses[1].AssignTeacher(Teachers[1]);
                Courses[2].AssignTeacher(Teachers[2]);
                Courses[3].AssignTeacher(Teachers[1]);
                Courses[4].AssignTeacher(Teachers[0]);
                
                Courses[0].AddStudent(Students[0]);
                Courses[0].AddStudent(Students[1]);
                Courses[0].AddStudent(Students[2]);
                Courses[1].AddStudent(Students[2]);
                Courses[1].AddStudent(Students[3]);
                Courses[3].AddStudent(Students[2]);
                Courses[3].AddStudent(Students[3]);
                Courses[4].AddStudent(Students[0]);
                
                Save();
                return;
            }
            var json = File.ReadAllText(SavePath);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            
            var wrapper = JsonConvert.DeserializeObject<SaveWrapper>(json, settings);
            
            Teachers = wrapper.TeachersSave;
            Students = wrapper.StudentsSave;
            Courses = wrapper.CoursesSave;
        }
    }
    
    [System.Serializable]
    class SaveWrapper
    {
        public List<Course> CoursesSave { get; set; }
        public List<Teacher> TeachersSave { get; set; }
        public List<Student> StudentsSave { get; set; }
    }
}