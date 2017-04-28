using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gepard.Configuration.VirtualHost
{
    [Serializable]
    [XmlRoot("FileName")]
    public class IndexFile
    {
        [XmlElement("FileName")]
        public string FileName { get; set; }

 //       public IndexFile(string fileName, int filePriority)
 //       {
 //           FileName = fileName;
 //           FilePriority = filePriority;
 //       }
    }
}
