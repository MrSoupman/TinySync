using System;
using System.Text.Json.Serialization;


namespace TinySync.Model
{
    public class Metadata
    {
        //Maybe change this to be a list instead?
        [JsonInclude]
        public string Origin { get; set; }

        //To remove?
        public string OriginSHA { get; set; }

        [JsonInclude]
        public string Remote { get; set; }

        //To remove?
        public string RemoteSHA { get; set; }

        public DateTime LastSynced { get; set; }

        
        public string Status { get; set; }

        
        public Metadata() { }

        public Metadata(string Origin)
        {
            this.Origin = Origin;
        }

        public Metadata(string Origin, string Remote)
        {
            this.Origin = Origin;
            this.Remote = Remote;
        }

        /// <summary>
        /// For debugging purposes only, need to remove later.
        /// </summary>
        /// <param name="Origin"></param>
        /// <param name="OriginSHA"></param>
        /// <param name="Remote"></param>
        /// <param name="RemoteSHA"></param>
        public Metadata(string Origin, string OriginSHA, string Remote, string RemoteSHA)
        {
            this.Origin = Origin;
            this.OriginSHA = OriginSHA;
            this.Remote = Remote;
            this.RemoteSHA = RemoteSHA;
            
        }

        public override bool Equals(object data)
        {
            Metadata temp = data as Metadata;
            if (data != null)
            {
                return temp.OriginSHA == OriginSHA && temp.Remote == Remote;
            }
            return false;
        }



        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

}
