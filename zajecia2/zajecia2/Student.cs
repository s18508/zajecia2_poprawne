namespace zajecia2
{
    public class Student
    {
        public string fname;
        public string lname;
        public string indexNumber;
        public string birthdate;
        public string email;
        public string motherName;
        public string fatherName;
        public Studies studies;

        public Student(string fname, string lname, string sname, string mode, string idx, string bd, string email, string mname, string faname)
        {
            this.fname = fname;
            this.lname = lname;
            this.indexNumber = idx;
            this.birthdate = bd;
            this.email = email;
            this.motherName = mname;
            this.fatherName = faname;
            studies = new Studies(sname, mode);

        }
    }
}