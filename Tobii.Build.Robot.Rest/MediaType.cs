namespace Tobii.Build.Robot.Rest
{
    public class MediaType
    {
        public static readonly MediaType Json = new MediaType("application/json");

        internal string Value;

        private MediaType(string mediaType)
        {
            Value = mediaType;
        }
    }
}