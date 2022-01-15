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

        public static void Sync(Metadata metadata)
        {
            ulong DiskSpace = 0;
            try
            {
                DriveFreeBytes(Path.GetDirectoryName(metadata.Remote), out DiskSpace);
            }
            catch (Exception)
            {
                metadata.Status = "Error getting available disk space.";
            }

            FileInfo info = new FileInfo(metadata.Origin);
            try
            {
                if (DiskSpace - (ulong)info.Length > 2097152) //Magic number is just 2 Megabytes in bytes; 2MB was arbitrarily chosen
                {
                    try
                    {
                        File.Copy(metadata.Origin, metadata.Remote, true);
                        metadata.Status = "Up to Date";
                        metadata.LastSynced = DateTime.Now;
                    }
                    catch (Exception e)
                    {
                        metadata.Status = e.Message;
                    }

                }
                else
                    metadata.Status = "Error - Not enough space on target disk.";
            }
            catch (Exception)
            {
                metadata.Status = "Error occurred getting file size.";
            }
            
        }
    }

    
}
