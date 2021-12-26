using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using TinySync.Model;

namespace TinySync.Services
{
    public class ShaSvc
    {
        public static string GetSHA(string file)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                try
                {
                    FileStream stream = File.Open(file, FileMode.Open);
                    stream.Position = 0;
                    byte[] hash = hasher.ComputeHash(stream);
                    stream.Close();
                    string sha = "";
                    for (int i = 0; i < hash.Length; i++)
                    {
                        sha += $"{hash[i]:X2}";
                    }                    
                    return sha;
                }
                catch (IOException e)
                {
                    throw;
                }
                catch (UnauthorizedAccessException e)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Tests two files by checking their SHA values
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <returns></returns>
        public static bool TestSHA(string file1, string file2)
        {
            string file1SHA = GetSHA(file1);
            string file2SHA = GetSHA(file2);
            return file1SHA.Equals(file2SHA);
        }
        public static bool TestSHA(Metadata metadata)
        {
            return TestSHA(metadata.Origin, metadata.Remote);
        }
    }
}
