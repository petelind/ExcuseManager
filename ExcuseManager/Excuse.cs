using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExcuseManager
{
    public class Excuse
    // TODO: Comments needed
    {
        public string Description;
        public string Results;
        public DateTime LastUsed;

        public string Save (string path, DateTime date)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(Description);
                sw.WriteLine(Results);
                sw.WriteLine(date);
                return Convert.ToString(File.GetLastAccessTime(path));
            }
        }
        public void Load(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                this.Description = sr.ReadLine();
                this.Results = sr.ReadLine();
                this.LastUsed = Convert.ToDateTime(sr.ReadLine());
            }
        }

    }
}
