using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinySync.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TinySync.Services;
namespace TinySync.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {

        /// <summary>
        /// data is used to store the directory/file to be synced, and its metadata. 
        /// </summary>
        public ObservableCollection<MetadataViewModel> data;

        public IEnumerable<MetadataViewModel> MetaList => data;


        public ICommand AddFile { get; }
        public ICommand AddFolder { get; }
        public ICommand Remove { get; }
        public ICommand Modify { get; }
        public ICommand Sync { get; }

        public HomeViewModel()
        {
            data = new ObservableCollection<MetadataViewModel>();
            ObservableCollection<Metadata> metadatas = JsonSvc.LoadJson();
            foreach (Metadata meta in metadatas)
            {
                data.Add(new MetadataViewModel(meta));
            }
        }



    }
}
