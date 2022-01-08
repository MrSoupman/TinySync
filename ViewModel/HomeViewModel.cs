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
using TinySync.Commands;

namespace TinySync.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {

        /// <summary>
        /// data is used to store the directory/file to be synced, and its metadata. 
        /// </summary>
        public ObservableCollection<MetadataViewModel> data;
        private List<Metadata> dataList;

        private int _SelectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                _SelectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        private string _Status;
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public IEnumerable<MetadataViewModel> MetaList => data;


        public ICommand AddFileMenu { get; }
        public ICommand AddFolderMenu { get; }
        public ICommand Remove { get; }
        public ICommand ModifyMenu { get; }
        public ICommand Sync { get; }

        public HomeViewModel(List<Metadata> dataList, NavigationSvc FileChooseViewNavSvc)
        {
            this.dataList = dataList;
            data = new ObservableCollection<MetadataViewModel>();
            UpdateMetalist();
            AddFileMenu = new NavigateCommand(FileChooseViewNavSvc);
            _SelectedIndex = -1;
            Remove = new RemoveFileCommand(this, dataList);
            Status = "Waiting...";
            ModifyMenu = new NavigateModifyCommand(this, dataList,FileChooseViewNavSvc.GetNavStore());
            
        }

        public void UpdateMetalist()
        {
            data.Clear();
            foreach (Metadata meta in dataList)
                data.Add(new MetadataViewModel(meta));
            JsonSvc.SaveJson(dataList);
        }



    }
}
