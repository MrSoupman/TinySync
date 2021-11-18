using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySync.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace TinySync.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// data is used to store the directory/file to be synced, and its metadata. 
        /// </summary>
        public ObservableCollection<Metadata> data = new ObservableCollection<Metadata>();

        public void LoadData()
        {
            data = JsonSvc.LoadJson();
        }

        /// <summary>
        /// Initializes all data needed
        /// </summary>
        public void Init()
        {
            

        }

        

    }
}
