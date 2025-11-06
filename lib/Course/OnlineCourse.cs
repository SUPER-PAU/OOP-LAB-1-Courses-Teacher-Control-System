namespace LAB_1_OOP.lib.Course
{
    public class OnlineCourse : Course
    {
        public string Platform { get; private set; }
    
        public OnlineCourse(string title, string platform)
        {
            Title = title;
            Platform = platform;
        }

        public void ChangePlatform(string platform)
        {
            Platform = platform;
        }
    
        public override string GetCourseType()
        {
            return "Online";
        }
    }
}
