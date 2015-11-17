using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace signbluetooth
{
    class Program
    {
        static List<string> DirSearch(string sDir)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        files.Add(f);
                    }
                    foreach (string f in DirSearch(d))
                    {
                        files.Add(f);
                    }
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            return files;
        }

        static void Main(string[] args)
        {
            //fuck .bat
            List<string> files = DirSearch(".\\Windows");
            foreach(string f in files)
            {
                try
                {
                    string filetocopy = f.Replace(".\\Windows", "U:\\Windows");
                    string destination = f.Replace(".\\Windows", ".\\MyWindows");
                    Directory.CreateDirectory(Path.GetDirectoryName(destination));
                    File.Copy(filetocopy, destination);
                    Console.WriteLine("Done: " + filetocopy);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
