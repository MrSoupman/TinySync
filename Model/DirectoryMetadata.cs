using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TinySync.Model
{
    public class DirectoryMetadata : Metadata
    {
        /// <summary>
        /// Exclusions is a list of files/dirs to be excluded from the sync. As such, should only be used for directories that were added.
        /// </summary>
        [JsonInclude]
        public IList<string> Exclusions { get; set; }

        public DirectoryMetadata(string origin, string remote)
        {
            Origin = origin;
            Remote = remote;
            Exclusions = new List<string>();
        }

        public override bool Equals(object data)
        {
            DirectoryMetadata temp = data as DirectoryMetadata;
            if (data != null)
            {
                if (temp.Origin == Origin &&
                    temp.Remote == Remote)
                {
                    for (int i = 0; i < temp.Exclusions.Count; i++)
                    {
                        if (!temp.Exclusions.Contains(Exclusions[i]))
                            return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
