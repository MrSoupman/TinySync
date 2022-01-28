using System;
using System.Text.Json.Serialization;


namespace TinySync.Model
{
    public class Metadata
    {
        public string Origin { get; set; }
        public string Remote { get; set; }
        public DateTime LastSynced { get; set; }

        [JsonIgnore]
        public string Status { get; set; } //might change to enum


        
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
