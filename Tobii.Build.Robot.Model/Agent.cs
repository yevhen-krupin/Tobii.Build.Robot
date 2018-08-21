namespace Tobii.Build.Robot.Model
{
    public class Agent : Entity
    {
        public string TypeId { get; set; }

        public string Connected { get; set; }

        public string Enabled { get; set; }

        public string Autorized { get; set; }

        public string Ip { get; set; }

        public Info EnabledInfo { get; set; }

        public Info AuthorizedInfo { get; set; }

        public Properties Properties { get; set; }
    }
}