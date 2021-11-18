using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace TinySync.Model
{
    public class Metadata
    {
        //Need to add datetime object

        [JsonInclude]
        public string Origin { get; set; }

        [JsonInclude]
        public string OriginSHA { get; set; }

        [JsonInclude]
        public string Remote { get; set; }

        [JsonInclude]
        public string RemoteSHA { get; set; }

        /// <summary>
        /// Exclusions is a list of files/dirs to be excluded from the sync. As such, should only be used for directories that were added.
        /// </summary>
        [JsonInclude]
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

        public override bool Equals(object data)
        {
            Metadata temp = data as Metadata;
            if (data != null)
            {
                if (temp.Origin == Origin &&
                    temp.OriginSHA == OriginSHA &&
                    temp.Remote == Remote &&
                    temp.RemoteSHA == RemoteSHA)
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
