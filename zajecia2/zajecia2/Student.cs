namespace zajecia2
{
    public class Student
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string indexNumber { get; set; }
        public string birthdate { get; set; }
        public string email { get; set; }
        public string motherName { get; set; }
        public string fatherName { get; set; }
        public Studies studies { get; set; }

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