using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TinySync.Model;

namespace TinySync.Services
{
    public class MetadataConverter : JsonConverter<Metadata>
    {
        enum TypeDiscriminator
        { 
            Metadata = 1,
            DirectoryMetadata = 2
        }
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(Metadata).IsAssignableFrom(typeToConvert);
        }

        public override Metadata Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            reader.Read();
            string propertyName = reader.GetString();
            if (propertyName != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            Metadata meta = typeDiscriminator switch
            {
                TypeDiscriminator.Metadata => new Metadata(),
                TypeDiscriminator.DirectoryMetadata => new DirectoryMetadata(),
                _ => throw new JsonException()
            };

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return meta;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "Origin":
                            meta.Origin = reader.GetString();
                            break;
                        case "Remote":
                            meta.Remote = reader.GetString();
                            break;
                        case "LastSynced":
                            meta.LastSynced = reader.GetDateTime();
                            break;
                        case "Exclusions":
                            reader.Read(); //Get past OpenArray token
                            List<string> Exclusions = new List<string>();
                            while (reader.TokenType != JsonTokenType.EndArray)
                            {
                                Exclusions.Add(reader.GetString());
                                reader.Read();
                            }
                            ((DirectoryMetadata)meta).Exclusions = Exclusions;
                            break;

                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Metadata value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value is DirectoryMetadata directory)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.DirectoryMetadata);
                writer.WriteStartArray("Exclusions");
                foreach (string path in ((DirectoryMetadata)value).Exclusions)
                {
                    writer.WriteStringValue(path);
                }
                writer.WriteEndArray();
            }
            else
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.Metadata);
            writer.WriteString("Origin", value.Origin);
            writer.WriteString("Remote", value.Remote);
            writer.WriteString("LastSynced", value.LastSynced);
            writer.WriteEndObject();
        }
    }
}
