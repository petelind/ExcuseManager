using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExcuseManager
{
    public class Excuse
    {
        public string Description;
        public string Results;
        public DateTime LastUsed;

        public void Save (string path, DateTime date)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(Description);
                sw.WriteLine(Results);
                sw.WriteLine(date);
            }
        }
    }
}
