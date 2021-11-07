using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TinySync.Model
{
    public class Metadata
    {

        public string OriginSHA { get; }

        public string RemoteFile { get; set; }

        public string RemoteSHA { get; }
    }

}
