namespace Neat.Infrastructure.Session.Model.Request
{
    public class SessionDurationRequest
    {
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public override string ToString()
        {
            return string.Format("Days: {0}, Hours: {1}, Minutes: {2}, Seconds: {3}", Days, Hours, Minutes, Seconds);
        }
    }
}