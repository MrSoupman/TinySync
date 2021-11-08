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
        public string Origin { get; }

        public string OriginSHA { get; }

        public string Remote { get; }

        public string RemoteSHA { get; }

        /// <summary>
        /// Exclusions is a list of files/dirs to be excluded from the sync. As such, should only be used for directories that were added.
        /// </summary>
        public IList<string> Exclusions { get; set; }

        public Metadata() { }

        public Metadata(string Origin)
        {
            this.Origin = Origin;
            Exclusions = new List<string>();
        }

        public Metadata(string Origin, string OriginSHA, string Remote, string RemoteSHA)
        {
            this.Origin = Origin;
            this.OriginSHA = OriginSHA;
            this.Remote = Remote;
            this.RemoteSHA = RemoteSHA;
            Exclusions = new List<string>();
        }

    }

}
