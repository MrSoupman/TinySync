using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySync.Model;
using System.ComponentModel;

namespace TinySync.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// data is used to store the directory/file to be synced, and its metadata. 
        /// </summary>
        public IList<Metadata> data;
        

        public void LoadData()
        { 
            
        }

    }
}
