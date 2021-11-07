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
        private Dictionary<string, Metadata> data;
        public event PropertyChangedEventHandler PropertyChanged;


    }
}
