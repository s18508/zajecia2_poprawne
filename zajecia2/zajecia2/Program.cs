using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace zajecia2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string path;
            string pathTo;
            string format;

            // obsluga podanych argumentow i wlascie przypisanie 
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
                throw new ArgumentException("Brak prawidlowej liczby argumentow, brak mozliwosci wykonania programu");
            }

            //w tym stringu znajda sie wszystkie bledy ktore od tej pory wystapily
            string trescBledow = "";

            // proba wczytania
            try
            {
                var lines = File.ReadLines(path);
                var studenci = new HashSet<Student>(new OwnComparer());
                foreach (var line in lines)
                {
                    try
                    {
                        var parts = line.Split(",");
                        if (parts.Length != 9)
                        {
                            throw new ArgumentNot9Exception("Studenta z podanej lini nie mozna zapisac, za malo argumentow, linia: " + line.ToString(), trescBledow);
                        }
                        for (int i = 0; i < 9; i++)
                        {
                            if (parts[i] == " " || parts[i] == "")
                            {
                                throw new EmptyArgumentException("Studenta z podanej lini nie mozna zapisac, pewien argument jest pusty, linia: " + line.ToString(), trescBledow);
                            }
                        }
                        var student = new Student(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8]);
                        if (studenci.Contains(student))
                        {
                            throw new DulpicateException("Powtorzono dane studenta", trescBledow);
                        }
                        else
                        {
                            studenci.Add(student);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
            
                }

                if (format == "xml")
                {
                    FileStream writer = new FileStream(pathTo, FileMode.Create);
                    XmlSerializer serializer = new XmlSerializer(typeof(HashSet<Student>),
                                               new XmlRootAttribute("uczelnia"));
                    serializer.Serialize(writer, studenci);
                    serializer.Serialize(writer, studenci);
                }
                    
            }





            catch(FileNotFoundException e)
            {
                var parts = path.Split("\\");
                trescBledow += "FileNotFoundException: niestety plik o nazwie " + parts[parts.Length - 1] + " nie istnieje.";
            }



            //zapis bledow
            var log = new FileStream("log.txt", FileMode.Create);
            var str_out = new StreamWriter(log);
            str_out.Write(trescBledow);
            str_out.Close();
            log.Close();





        }

    }
}
