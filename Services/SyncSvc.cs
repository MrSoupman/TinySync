using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TinySync.Model;
using System.Runtime.InteropServices;

namespace TinySync.Services
{
    public class SyncSvc
    {
        /// <summary>
        /// Credit for code goes to pinvoke.net
        /// </summary>
        /// <param name="lpDirectoryName"></param>
        /// <param name="lpFreeBytesAvailable"></param>
        /// <param name="lpTotalNumberOfBytes"></param>
        /// <param name="lpTotalNumberOfFreeBytes"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
            out ulong lpFreeBytesAvailable,
            out ulong lpTotalNumberOfBytes,
            out ulong lpTotalNumberOfFreeBytes);

        private static ulong DriveFreeBytes(string folderName, out ulong freespace)
        {
            freespace = 0;
            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentNullException("folderName");
            }

            if (!folderName.EndsWith("\\"))
            {
                folderName += '\\';
            }

            ulong free = 0, dummy1 = 0, dummy2 = 0;

            if (GetDiskFreeSpaceEx(folderName, out free, out dummy1, out dummy2))
            {
                return free;
            }
            else
            {
                throw new System.ComponentModel.Win32Exception();
            }
        }

        public static void SyncFile(Metadata metadata)
        {
            try
            {
                SyncFile(metadata.Origin, metadata.Remote);
                metadata.Status = "Up to Date";
                metadata.LastSynced = DateTime.Now;
            }
            catch (Exception e)
            {
                metadata.Status = e.Message;
            }
            
        }

        private static void SyncFile(string file,string remote)
        {
            ulong DiskSpace = 0;
            try
            {
                DriveFreeBytes(Path.GetDirectoryName(file), out DiskSpace);
            }
            catch (Exception)
            {
                throw new Exception(message: "Error getting available disk space.");
            }

            FileInfo info = new FileInfo(file);
            try
            {
                if (DiskSpace - (ulong)info.Length > 2097152) //Magic number is just 2 Megabytes in bytes; 2MB was arbitrarily chosen
                {
                    try
                    {

                        File.Copy(file, remote, true);
                        
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
                else
                    throw new Exception(message: "Error - Not enough space on target disk.");
            }
            catch (Exception)
            {
                throw new Exception(message: "Error occurred getting file size.");
            }
        }

        public static void SyncDirectory(DirectoryMetadata metadata)
        {
            try
            {

                SyncDirectory(metadata.Origin, metadata.Remote);
                metadata.Status = "Up to Date";
                metadata.LastSynced = DateTime.Now;
            }
            catch(Exception e)
            {
                metadata.Status = e.Message;
            }
        }

        private static void SyncDirectory(string OriginDir, string RemoteDir)
        {
            var EFolders = Directory.EnumerateFileSystemEntries(OriginDir);
            int count = OriginDir.Length+1;
            foreach (string file in EFolders)
            {
                FileAttributes attr = File.GetAttributes(file);
                string sepFile = Path.DirectorySeparatorChar + file.Remove(0,count);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory) //check if it's a directory
                {
                    if (!Directory.Exists(RemoteDir + sepFile))
                    {
                        try
                        {
                            Directory.CreateDirectory(RemoteDir + sepFile);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                    SyncDirectory(OriginDir + sepFile, RemoteDir + sepFile);
                }
                else
                {
                    //need to check for exclusions here
                    try
                    {
                        SyncFile(OriginDir + sepFile, RemoteDir + sepFile);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
            }
        }
    }

    
}
