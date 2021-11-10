using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace TinySync.ViewModel
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
    }
}
