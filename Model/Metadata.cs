using System;
using System.Text.Json.Serialization;


namespace TinySync.Model
{
    public class Metadata
    {
        //Maybe change this to be a list instead?
        [JsonInclude]
        public string Origin { get; set; }

        [JsonInclude]
        public string Remote { get; set; }

        [JsonInclude]
        public DateTime LastSynced { get; set; }

        [JsonIgnore]
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
            LastSynced = default;
        }

        public Metadata(string Origin, string Remote, DateTime LastSynced)
        {
            this.Origin = Origin;
            this.Remote = Remote;
            this.LastSynced = LastSynced;
        }

        public override bool Equals(object data)
        {
            Metadata temp = data as Metadata;
            if (data != null)
            {
                return temp.Origin == Origin && temp.Remote == Remote;
            }
            return false;
        }



        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

}
