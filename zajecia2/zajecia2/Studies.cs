namespace zajecia2
{
    public class Studies
    {
        public string name { get; set; }
        public string mode { get; set; }
        public int numberOfStudents;
        public Studies(string name, string mode)
        {
            this.name = name;
            this.mode = mode;
            numberOfStudents = 0;
        }
        public void addStud()
        {
            numberOfStudents++;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (typeof(Studies).IsInstanceOfType(obj)) {
                Studies nStud = (Studies)obj;
                return (nStud.name == this.name);
            }
            return false;
        }
    }
    
}