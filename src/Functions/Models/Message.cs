namespace Functions.Models
{
    public class Message
    {
        public int Userid { get; }
        public int TimezoneOffset { get; }

        public Message(int userid, int timezoneOffset = -120)
        {
            Userid = userid;
            TimezoneOffset = timezoneOffset;
        }
    }
}