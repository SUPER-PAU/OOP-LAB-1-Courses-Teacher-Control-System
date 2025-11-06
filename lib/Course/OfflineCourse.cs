namespace LAB_1_OOP.lib.Course
{
    public class OfflineCourse : Course
    {
        public string Classroom { get; private set; }

        public OfflineCourse(string title, string classroom)
        {
            Title = title;
            Classroom = classroom;
        }

        public void ChangeClassroom(string classroom)
        {
            Classroom = classroom;
        }

        public override string GetCourseType()
        {
            return "Online";
        }
    }
}