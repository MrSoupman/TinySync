using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TinySync.Model;

namespace TinySync.ViewModel
{
    class SyncSvc
    {
        /// <summary>
        /// Refreshes a metadata, resetting SHA values
        /// </summary>
        /// <param name="data"></param>
        public static void RefreshMetadata(Metadata data)
        {
            if (File.Exists(data.Origin) || Directory.Exists(data.Origin))
            {
                //First check if we're working with a directory or file

                FileAttributes attr = File.GetAttributes(data.Origin);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    //may have to do this somewhere else?
                }
                else 
                {
                    data.OriginSHA = ShaSvc.GetSHA(data.Origin);
                    data.RemoteSHA = ShaSvc.GetSHA(data.Remote);
                }
            }
            else
                throw new FileNotFoundException("Error, couldn't find the specified origin file or directory.", data.Origin);

            
        }

    }
}
