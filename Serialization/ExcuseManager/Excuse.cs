using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; // Здесь живет бинарный форматтер

namespace ExcuseManager
{
    [Serializable] // этот ключ разрешает сериализовывать объекты этого типа
    public class Excuse
    {
        public string Description;
        public string Results;
        public DateTime LastUsed;

        public string Save (string path, DateTime date)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream output = File.Create(path))
            {
                formatter.Serialize(output, this);
                return Convert.ToString(File.GetLastAccessTime(path));
            }
            
        }
        public void Load(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream input = File.Open(path, FileMode.Open))
            {
                Excuse temp = (Excuse)formatter.Deserialize(input);
                this.Description = temp.Description;
                this.Results = temp.Results;
                this.LastUsed = temp.LastUsed;             
            }
        }

    }
}
