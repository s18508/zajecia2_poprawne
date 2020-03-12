using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text.Json;

namespace zajecia2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string path;
            string pathTo;
            string format;

            // obsluga podanych argumentow i wlasciwe przypisanie 
            if (args.Length ==2 || args.Length==3)
            {
                if (args.Length == 3)
                {
                    path=args[0];
                    pathTo = args[1];
                    format = args[2];
                }
                else
                {
                    if(args[1]!="xml" && args[1] != "json")
                    {
                        path = args[0];
                        pathTo = args[1];
                        format = "xml";
                    }
                    else
                    {
                        var parts = args[0].Split(".");
                        if (parts[parts.Length - 1] == "csv")
                        {
                            path = args[0];
                            pathTo = "result." + args[1];
                            format = args[1];
                        }
                        else
                        {
                            path = "data.csv";
                            pathTo = args[0];
                            format = args[1];
                        }
                    }
                }
            }
            else
            {
                path = @"D:\dane.csv";
                pathTo = @"D:\dokument.xml";
                format = "xml"; //parametry testowe
                throw new ArgumentException("Brak prawidlowej liczby argumentow, brak mozliwosci wykonania programu");
            }



            //w tym stringu znajda sie wszystkie bledy ktore od tej pory wystapily
            string trescBledow = "";

            // proba wczytania
            try
            {
                var lines = File.ReadLines(path);
                var studenci = new HashSet<Student>(new OwnComparer());
                var studies = new List<Studies>();
                foreach (var line in lines)
                {
                    try
                    {
                        var parts = line.Split(",");
                        if (parts.Length != 9)
                        {
                            throw new ArgumentNot9Exception("Studenta z podanej lini nie mozna zapisac, za malo argumentow, linia: " + line.ToString());
                        }
                        for (int i = 0; i < 9; i++)
                        {
                            if (parts[i] == " " || parts[i] == "")
                            {
                                throw new EmptyArgumentException("Studenta z podanej lini nie mozna zapisac, pewien argument jest pusty, linia: " + line.ToString());
                            }
                        }
                        var elStudies = new Studies(parts[2], parts[3]);
                        if (!studies.Contains(elStudies)){
                            studies.Add(elStudies);
                        }
                        
                        var student = new Student(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8]);
                        if (studenci.Contains(student))
                        {
                            throw new DulpicateException("Powtorzono dane studenta");
                        }
                        else
                        {
                            studies.ElementAt(studies.IndexOf(elStudies)).addStud();
                            studenci.Add(student);
                        }
                    }
                    catch (Exception e)
                    {
                        trescBledow += e.Message + "\n";
                    }

                }
                Console.WriteLine(trescBledow);


                //tworzenie xml
                if (format == "xml")
                {

                    var xDoc = new XDocument();
                    var today = DateTime.Today;
                    var trescXML = "<uczelnia \ncreatedAt=\" " + today + "\"\nauthor=\"Dominika Wojtanowska\">\n\t<studenci>\n";
                     
                    
                    xDoc.Add(new XElement("uczelnia",
                        new XAttribute("createdAt", today),
                        new XAttribute("author", "Dominika Wojtanowska"),
                        new XElement("studenci",
                        from stud in studenci
                        select new XElement("student",
                            new XAttribute("indexNumber", stud.indexNumber),
                            new XElement("fname", stud.fname),
                            new XElement("lname", stud.lname),
                            new XElement("birthdate", stud.birthdate),
                            new XElement("email", stud.email),
                            new XElement("mohersName", stud.motherName),
                            new XElement("fathersName", stud.fatherName),
                            new XElement("studies", new XElement("name", stud.studies.name), new XElement("mode", stud.studies.mode))

                        )),
                        new XElement( "activeStudies",
                        from studie in studies
                        select new XElement("studies", 
                            new XAttribute("name", studie.name),
                            new XAttribute("numberOfStudents", studie.numberOfStudents))
                            
                        )));
                    //Console.Write(xDoc.ToString());
                    xDoc.Save(pathTo);
                    
                    // serializer.Serialize(writer, studenci); wolalam sie pobawic w tworzenie xdocumenta
                }

                //tworzenie jsona
                if (format == "json")
                {
                    var today = DateTime.Today;
                    var jsonString = "{\n\tuczelnia: {\n\t\tcreatedAt:\"" + today + "\"\n"
                        + "\t\tauthor: \"Dominika Wojtanowska\" " + "\n\t\tstudenci:";
                    jsonString += JsonSerializer.Serialize(studenci);
                    jsonString += JsonSerializer.Serialize(studies);
                    jsonString += "} \n}";
                    //Console.Write(jsonString);
                    File.WriteAllText(pathTo, jsonString);

                }

            }
            catch (FileNotFoundException e)
            {
                var parts = path.Split("\\");
                trescBledow += "FileNotFoundException: niestety plik o nazwie " + parts[parts.Length - 1] + " nie istnieje.";
            }



            //zapis bledow
            var log = new FileStream("D:\\log.txt", FileMode.Create);
            var str_out = new StreamWriter(log);
            Console.Write(trescBledow);
            str_out.Write(trescBledow);
            str_out.Close();
            log.Close();

        }

    }
}
